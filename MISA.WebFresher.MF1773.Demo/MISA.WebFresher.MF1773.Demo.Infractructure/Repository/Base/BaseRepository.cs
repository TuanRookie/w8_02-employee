using Dapper;
using MISA.WebFresher.MF1773.Demo.Application;
using MISA.WebFresher.MF1773.Demo.Domain;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Infractructure
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IEntity
    {
        protected IDbContext _dbContext;
        public BaseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region FindAsync
        /// <summary>
        /// Tìm bản ghi theo ID
        /// </summary>
        /// <param name="id">ID cần tìm</param>
        /// <returns>Trả về một bản ghi nếu có ,Không có trả về null</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate> 
        public async Task<TEntity?> FindAsync(Guid id)
        {
            var entity = await _dbContext.FindAsync<TEntity>(id);
            return entity;
        }
        #endregion

        #region GetAll
        /// <summary>
        /// Lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>>Danh sách tất cả bản ghi</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate>
        public async Task<List<TEntity>> GetAllAsync()
        {

            var entitys = await _dbContext.GetAllAsync<TEntity>();
            return entitys.ToList();
        }
        #endregion

        #region GetSimple
        /// <summary>
        /// Lấy dữ liệu 1 bản ghi
        /// </summary>
        /// <param name="id">Mã ID của bản ghi</param>
        /// <returns>Trả về 1 bản ghi</returns>
        /// <exception cref="NotFoundException"></exception>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate> 
        public async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await _dbContext.FindAsync<TEntity>(id);

            if (entity == null)
            {
                throw new NotFoundException("Không tìm thấy tài nguyên!", 404);
            }

            return entity;
        }
        #endregion

        #region GetByListId
        /// <summary>
        /// Lấy danh sách các bản ghi theo id .
        /// </summary>
        /// <param name="ids">Danh sách ID cần tìm kiếm</param>
        /// <returns>Danh sách bản ghi theo ID</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate> 
        public async Task<List<TEntity>> GetByListIdAsync(List<Guid> ids)
        {
            var listEntity = new List<TEntity>();
            foreach (var id in ids)
            {
                var entity = await _dbContext.FindAsync<TEntity>(id);
                if (entity != null)
                {
                    listEntity.Add(entity);
                }
            }
            return listEntity;
        }

        #endregion

        #region Insert
        /// <summary>
        /// Thêm một bản ghi mới vào hệ thống.
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm</param>
        /// <returns>true nếu thêm thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate>
        public async Task<bool> InsertAsync(TEntity entity)
        {

            var result = await _dbContext.InsertAsync<TEntity>(entity);

            return result;


        }
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate>
        public async Task<bool> UpdateAsync(Guid id, TEntity entity)
        {
            var res = await _dbContext.UpdateAsync<TEntity>(id, entity);
            return res;
        }
        #endregion

        #region DeleteSimple
        /// <summary>
        /// Xoá 1 bản ghi khỏi hệ thống
        /// </summary>
        /// <param name="entity">Bản ghi cần xoá</param>
        /// <returns>true nếu xóa thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _dbContext.DeleteAsync<TEntity>(id);

            return result;

        }
        #endregion

        #region DeleteMany
        /// <summary>
        /// Xoá nhiều bản ghi khỏi hệ thống
        /// </summary>
        /// <param name="entities">Danh sách bản ghi cần xoá</param>
        /// <returns>true nếu xóa thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate>
        public async Task<bool> DeleteManyAsync(List<Guid> ids)
        {
            var result = await _dbContext.DeleteManyAsync<TEntity>(ids);

            return result;
        }
        #endregion

        public async Task<EntitisPaging<TEntity>> GetEntityPagingAsync(int? pageSize, int? pageNumber, string? entityFilter)
        {
            var emtitiesPaging = await _dbContext.GetEntityPagingAsync<TEntity>(pageSize, pageNumber, entityFilter);

            return emtitiesPaging;
        }

    }
}
