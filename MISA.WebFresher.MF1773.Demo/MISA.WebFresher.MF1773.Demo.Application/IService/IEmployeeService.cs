using Microsoft.AspNetCore.Http;
using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public interface IEmployeeService : IBaseService<EmployeeDto,EmployeeCreateDto,EmployeeUpdateDto>
    {
        /// <summary>
        /// Timf kiếm và phân trang dựa trên 1 chuỗi
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// <returns>Số bản ghi phù hợp và phân trang</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(29/12/2023)</CreatedDate>
        Task<EntitisPaging<EmployeeDto>> GetEmployeePagingAsync(int? pageSize, int? pageNumber, string? employeeFilter);

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Trả về mã nhân viên mới</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(29/12/2023)</CreatedDate>
        Task<string> GetNewEmployeeCode();

        /// <summary>
        /// Thêm dữ liệu bằng file excel
        /// </summary>
        /// <param name="fileImport"></param>
        /// <exception cref="ValidateException">Khi bản ghi nào bị lỗi</exception>
        /// <returns>Trả về danh sách đã được thêm và lỗi</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/01/2024)</CreatedDate> 
        Task<IEnumerable<EmployeeDto>> ImportEmployeeAsync(IFormFile fileImport);

    }
}
