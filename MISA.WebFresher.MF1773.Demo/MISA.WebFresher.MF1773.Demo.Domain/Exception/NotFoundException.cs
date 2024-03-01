namespace MISA.WebFresher.MF1773.Demo.Domain
{
    public class NotFoundException : Exception
    {
        public int ErrorCode { get; set; }
        public NotFoundException() { }
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        //overloading base exception
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
