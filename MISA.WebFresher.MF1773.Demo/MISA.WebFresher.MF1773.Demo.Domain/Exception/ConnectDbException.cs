using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    public class ConnectDbException : Exception
    {
        public int ErrorCode { get; set; }
        public ConnectDbException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public ConnectDbException(string message) : base(message) { }
        public ConnectDbException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

    }
}
