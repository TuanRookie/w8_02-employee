using Microsoft.AspNetCore.Http;
using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public interface ICustomerService : IBaseService<CustomerDto, CustomerCreateDto,  CustomerUpdateDto>
    {
        /// <summary>
        /// Timf kiếm và phân trang dựa trên 1 chuỗi
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="customerFilter"></param>
        /// <returns>Số bản ghi phù hợp và phân trang</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(29/12/2023)</CreatedDate>
        Task<EntitisPaging<CustomerDto>> GetCustomerPagingAsync(int? pageSize, int? pageNumber, string? customerFilter);
        /// <summary>
        /// Thêm dữ liệu bằng file excel
        /// </summary>
        /// <param name="fileImport"></param>
        /// <exception cref="ValidateException">Khi bản ghi nào bị lỗi</exception>
        /// <returns>Trả về danh sách đã thêm vào database</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(17/01/2024)</CreatedDate> 
        Task<IEnumerable<CustomerDto>> ImportCustomerAsync(IFormFile fileImport);
    }
}
