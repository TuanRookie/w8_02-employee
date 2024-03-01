using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public static class MISAConst
    {
        public const string ERROR_CUSTOMER_EMPTY_CODE = "Mã khách hàng không được phép để trống.";
        public const string ERROR_CUSTOMER_EMPTY_CODE_LENGTH = "Mã khách hàng không được vượt quá 100 ký tự.";
        public const string ERROR_CUSTOMER_EMPTY_NAME = "Tên khách hàng không được phép để trống.";
        public const string ERROR_CUSTOMER_EMPTY_NAME_LENGTH = "Tên khách hàng không được vượt quá 255 ký tự.";

        public const string ERROR_EMPLOYEE_EMPTY_CODE = "Mã nhân viên không được phép để trống.";
        public const string ERROR_EMPLOYEE_EMPTY_CODE_LENGTH = "Mã nhân viên không được vượt quá 100 ký tự.";
        public const string ERROR_EMPLOYEE_EMPTY_NAME = "Tên nhân viên không được phép để trống.";
        public const string ERROR_EMPLOYEE_EMPTY_NAME_LENGTH = "Tên nhân viên không được vượt quá 255 ký tự.";
    }
}
