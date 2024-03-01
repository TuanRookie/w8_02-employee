using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy thông tin một bản ghi
        /// </summary>
        /// <param name="TEntityId">Định danh bản ghi</param>
        /// <exception cref="NotFoundException">Khi không tìm thấy bản ghi</exception>
        /// <returns> Một bản ghi</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate> 
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Tìm kiếm bản ghi dựa trên mã định danh
        /// </summary>
        /// <param name="id">Chuỗi tìm kiếm</param>
        /// <returns>Bản ghi hoặc null nếu không tìm thấy</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        Task<TEntity?> FindAsync(Guid id);

        /// <summary>
        /// Tìm kiếm các entity dựa trên danh sách ID
        /// </summary>
        /// <param name="ids"> Danh sách ID tìm kiếm</param>
        /// <returns>Danh sách entity hoặc exception nếu không tìm thấy entity phù hợp với các id</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate>
        Task<List<TEntity>> GetByListIdAsync(List<Guid> ids);

        /// <summary>
        /// Thêm một bản ghi vào hệ thống
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true nếu thêm thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        Task<bool> InsertAsync(TEntity entity);

        /// <summary>
        /// Cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="id">Định danh</param>
        /// <param name="entity">Thông tin cần cập nhật</param>
        /// <returns>true nếu thêm thành công, false nếu không thành côn</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        Task<bool> UpdateAsync(Guid id, TEntity entity);

        /// <summary>
        /// Xoá một bản ghi khỏi hệ thống
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true nếu xóa thành công,false nếu xoá không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Xoá nhiều bản ghi khỏi hệ thống
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true nếu xóa thành công,false nếu xoá không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        Task<bool> DeleteManyAsync(List<Guid> ids);

        /// <summary>
        /// Phân trang và tìm kiếm dựa trên 1 chuỗi và số lượng bản ghi trên 1 trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="entityFilter">Chuỗi tìm kiếm(số điện thoại,tên, mã nhân viên)</param>
        /// <returns>Số bản ghi phù hợp và phân trang</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(29/12/2023)</CreatedDate>
        Task<EntitisPaging<TEntity>> GetEntityPagingAsync(int? pageSize, int? pageNumber, string? entityFilter);

    }
}
