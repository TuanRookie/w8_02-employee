using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    /// <summary>
    /// Interface tương tác với repository của customer
    /// </summary>
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// Kiểm tra sự tồn tại của một entity dựa trên định danh bản ghi
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns>true nếu entity tồn tại, false nếu không tồn tại</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        Task<bool> IsExistCustomerAsync(string entityCode);
    }
}
