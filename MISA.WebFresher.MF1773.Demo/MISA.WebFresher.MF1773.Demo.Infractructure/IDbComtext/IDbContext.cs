using Dapper;
using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher.MF1773.Demo.Infractructure
{
     public interface IDbContext 
     {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; set; }

        #region FindAsync
        /// <summary>
        /// Tìm bản ghi theo ID
        /// </summary>
        /// <param name="id">ID cần tìm</param>
        /// <returns>Trả về một bản ghi nếu có ,Không có trả về null</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate> 
        Task<TEntity?> FindAsync<TEntity>(Guid id);

        #endregion

        #region GetAll
        /// <summary>
        /// Lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>>Danh sách tất cả bản ghi</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate>
        Task<List<TEntity>> GetAllAsync<TEntity>();

        #endregion

        #region Insert
        /// <summary>
        /// Thêm một bản ghi mới vào hệ thống.
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm</param>
        /// <returns>true nếu thêm thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate>
        Task<bool> InsertAsync<TEntity>(TEntity entity);

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
        Task<bool> UpdateAsync<TEntity>(Guid id, TEntity entity);
        #endregion

        #region DeleteSimple
        /// <summary>
        /// Xoá 1 bản ghi khỏi hệ thống
        /// </summary>
        /// <param name="entity">Bản ghi cần xoá</param>
        /// <returns>true nếu xóa thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate>
        Task<bool> DeleteAsync<TEntity>(Guid id);
        #endregion

        #region DeleteMany
        /// <summary>
        /// Xoá nhiều bản ghi khỏi hệ thống
        /// </summary>
        /// <param name="entities">Danh sách bản ghi cần xoá</param>
        /// <returns>true nếu xóa thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate>
        Task<bool> DeleteManyAsync<TEntity>(List<Guid> ids);
        #endregion

        #region Paging
        /// <summary>
        /// Phân trang và tìm kiếm dựa trên 1 chuỗi
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="entityFilter"></param>
        /// <returns>Trả về kết quả phân trang theo kết quả đầu vào</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate>
        #endregion
        Task<EntitisPaging<TEntity>> GetEntityPagingAsync<TEntity>(int? pageSize, int? pageNumber, string? entityFilter);

     }
}
