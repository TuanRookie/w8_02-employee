
using Newtonsoft.Json;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    /// <summary>
    /// Thông tin khách hàng
    /// CreatedBy: DCTuan
    /// </summary>
    public class Customer : BaseEntity, IEntity
    {
        // ID khách hàng
        [JsonProperty("Name")]
        public Guid CustomerId { get; set; }
        // Mã nhóm khách hàng
        public Guid CustomerGroupId { get; set; }
        // Mã khách hàng
        public string? CustomerCode { get; set; }
        // Họ và tên khách hàng
        public string? FullName { get; set; }
        // Ngày sinh
        public DateTime? DateOfBirth { get; set; }
        // Giới tính
        public GenderEnum? Gender { get; set; }
        // Số điện thoại
        public string? PhoneNumber { get; set; }
        // Địa chỉ email
        public string? Email { get; set; }
        // Địa chỉ
        public string? Address { get; set; }
        // Số căn cước công dân
        public string? IdentityNumber { get; set; }
        // Ngày cấp
        public DateTime? IdentityDate { get; set; }
        // Nơi cấp
        public string? IdentityPlace { get; set; }
        // Số tiền dư nợ
        public Decimal? DebitAmount { get; set; }
        // Công ty
        public string? CompanyName { get; set; }

        public Guid GetId()
        {
            return CustomerId;
        }

        public void SetId(Guid id)
        {
            CustomerId = id;
        }
    }
}
