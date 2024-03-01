using Microsoft.Extensions.Configuration;
using MISA.WebFresher.MF1773.Demo.Domain;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher.MF1773.Demo.Domain.Resource;
using Dapper;
using MISA.WebFresher.MF1773.Demo.Application;

namespace MISA.WebFresher.MF1773.Demo.Infractructure
{
    public class DbContext : IDbContext
    {
        public IDbConnection Connection { get; }

        public IDbTransaction Transaction { get; set; }

        public DbContext(IConfiguration configuration)
        {
            Connection = new MySqlConnection(configuration["ConnectionString"]);
        }

        public async Task<bool> DeleteAsync<TEntity>(Guid id)
        {
            var TableName = typeof(TEntity).Name;
            var proc = $"Proc_Delete{TableName}ById";
            var param = new DynamicParameters();
            param.Add(@$"{TableName}Id", id);

            await Connection.ExecuteAsync(
               proc,
               param,
               commandType: CommandType.StoredProcedure,
               transaction: Transaction
            );

            return true;
        }

        public async Task<bool> DeleteManyAsync<TEntity>(List<Guid> ids)
        {
            var TableName = typeof(TEntity).Name;
            var stringIds = ids.Select(id => id.ToString()).ToArray();
            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id IN @Ids";
            await Connection.ExecuteAsync(sql, new { Ids = stringIds });
            return true;
        }

        public async Task<TEntity?> FindAsync<TEntity>(Guid id)
        {
            var TableName = typeof(TEntity).Name;
            var proc = $"Proc_Get{TableName}ById";
            var param = new DynamicParameters();
            param.Add(@$"{TableName}Id", id);

            var entity = await Connection.QueryFirstOrDefaultAsync<TEntity>(
                    proc,
                    param,
                    commandType: CommandType.StoredProcedure,
                    transaction: Transaction
                    );

            return entity;
        }

        public async Task<List<TEntity>> GetAllAsync<TEntity>()
        {
            var TableName = typeof(TEntity).Name;
            var proc = $"Proc_GetAll{TableName}";
            var entitys = await Connection.QueryAsync<TEntity>(
                proc,
                commandType: CommandType.StoredProcedure,
                transaction: Transaction
                );
            return entitys.ToList();
        }

        public async Task<EntitisPaging<TEntity>> GetEntityPagingAsync<TEntity>(int? pageSize, int? pageNumber, string? entityFilter)
        {
            var TableName = typeof(TEntity).Name;
            var emtitiesPaging = new EntitisPaging<TEntity>();
            var proc = $"Proc_Get{TableName}FilterPaging";
            DynamicParameters param = new DynamicParameters();
            param.Add("@m_PageSize", pageSize);
            param.Add("@m_PageIndex", pageNumber);
            param.Add($"@m_{TableName}Filter", entityFilter);
            param.Add("@m_TotalRecord", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            param.Add("@m_TotalPage", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            param.Add("@m_CurrentPageRecords", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);


            var entities = await Connection.QueryAsync<TEntity>(proc, param, commandType: CommandType.StoredProcedure);

            emtitiesPaging.Data = entities.ToList();
            emtitiesPaging.TotalPage = param.Get<int>("@m_TotalPage"); //Số trang sau khi phân
            emtitiesPaging.TotalRecord = param.Get<int>("@m_TotalRecord");//Tổng số trang
            emtitiesPaging.CurrentPageRecords = param.Get<int>("@m_CurrentPageRecords"); //Bản ghi trên trang hiện tại
            emtitiesPaging.CurrentPage = pageNumber; //Trang hiện tại

            return emtitiesPaging;
        }

        public async Task<bool> InsertAsync<TEntity>(TEntity entity)
        {
            var TableName = typeof(TEntity).Name;
            var proc = $"Proc_Insert{TableName}";
            var id = Guid.NewGuid();

            await Connection.ExecuteAsync(
                   proc,
                   entity,
                   commandType: CommandType.StoredProcedure,
                   transaction: Transaction
            );

            return true;
        }

        public async Task<bool> UpdateAsync<TEntity>(Guid id, TEntity entity)
        {
            var TableName = typeof(TEntity).Name;
            var proc = $"Proc_Update{TableName}";

            var res = await Connection.ExecuteAsync(
               proc,
               entity,
               commandType: CommandType.StoredProcedure,
               transaction: Transaction
            );

            if (res > 0)
            {
                return true;
            }
            else
            {
                throw new NotFoundException("Không có dữ liệu nào được cập nhật");
            }
        }
    }
}
