using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    public interface ICustomerValidate 
    {
        /// <summary>
        /// Kiểm tra trùng mã khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <exception cref="ConflictException">Nếu bị trùng mã</exception>
        /// <returns></returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(26/12/2023)</CreatedDate>
        Task CheckCustomerExistByCodeAsync(Customer customer);
    }
}
