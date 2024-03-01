using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    public class ValidateException :Exception
    {
        public int ErrorCode { get; set; }
        public ValidateException() { }
        public ValidateException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        //overloading base exception
        public ValidateException(string message) : base(message) { }
        public ValidateException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
