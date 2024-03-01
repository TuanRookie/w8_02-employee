using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Kiểm tra sự tồn tại của một entity dựa trên định danh bản ghi
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns>true nếu entity tồn tại, false nếu không tồn tại</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        Task<bool> IsExistEmployeeAsync(string entityCode);

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(29/12/2023)</CreatedDate> 
        Task<string> GetNewEmployeeCode();
    }
}
