using System.Text.Json;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    public class BaseException
    {
        // Mã lỗi, sẽ chứa giá trị số để xác định loại lỗi
        public int ErrorCode { get; set; }

        // Thông điệp dành cho Dev
        public string? DevMessage { get; set; }

        // Thông điệp dành cho người dùng
        public string? UserMessage { get; set; }

        // Mã theo dõi (Trace ID) để theo dõi lỗi trong hệ thống
        public string? TraceId { get; set; }

        // Thông tin bổ sung, chẳng hạn như URL hoặc tài liệu hướng dẫn
        public string? MoreInfo { get; set; }

        // Đối tượng lỗi hoặc thông tin cụ thể về lỗi
        public object? Error { get; set; }

        // Phương thức này chuyển đối tượng Exception thành một chuỗi JSON
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
