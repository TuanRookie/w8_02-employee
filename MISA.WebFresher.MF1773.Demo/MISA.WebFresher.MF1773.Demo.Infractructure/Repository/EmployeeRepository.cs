using Dapper;
using MISA.WebFresher.MF1773.Demo.Application;
using MISA.WebFresher.MF1773.Demo.Domain;
using MISA.WebFresher.MF1773.Demo.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher.MF1773.Demo.Infractructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> GetNewEmployeeCode()
        {
            var proc = "Proc_GetNewEmployeeCode";

            var newEmployeeCode = await
               _dbContext.Connection.ExecuteScalarAsync<string>(
                   proc,
                   commandType: CommandType.StoredProcedure
                   );

            return newEmployeeCode;
        }

        public async Task<bool> IsExistEmployeeAsync(string entityCode)
        {
            var proc = $"Proc_CheckEmployeeCodeExits";

            var param = new DynamicParameters();
            param.Add($"EmployeeCode", entityCode);

            var result = await
              _dbContext.Connection.ExecuteScalarAsync<bool>(
                   proc,
                    param,
                   commandType: CommandType.StoredProcedure,
                   transaction: _dbContext.Transaction
                   );

            return result;
        }
    }
}
