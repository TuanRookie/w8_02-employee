using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    public interface IEmployeeValidate
    {
        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="ConflictException">Nếu bị trùng mã</exception>
        /// <returns></returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(08/01/2024)</CreatedDate>
        Task CheckEmployeeExistByCodeAsync(Employee employee);

    }
}
