using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public interface IBaseService<TDto, TCreateDto, TUpdateDto> : IBaseReadOnlyService<TDto>
    {
        /// <summary>
        /// Thêm một entity mới vào hệ thống.
        /// </summary>
        /// <param name="entity">entity cần thêm</param>
        /// <returns>true nếu thêm thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        Task<TDto> InsertAsync(TCreateDto createDto);

        /// <summary>
        /// Cập nhật thông tin của một entity.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        /// <returns>true nếu cập nhật thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate> 
        Task<TDto> UpdateAsync(Guid id, TUpdateDto updateDto);

        /// <summary>
        /// Xóa một entity khỏi hệ thống.
        /// </summary>
        /// <param name="id">Mã định danh của entity cần xoá </param>
        /// <returns>true nếu xóa thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>  
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhieu entity khỏi hệ thống.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>true nếu xóa thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>  
        Task<bool> DeleteManyAsync(List<Guid> ids);

        /// <summary>
        /// Xuất tất cả các bản ghi vào file excel
        /// </summary>
        /// <returns>Mảng byte của file excel</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(17/01/2024)</CreatedDate> 
        Task<byte[]> ExportAllAsync();
    }
}
