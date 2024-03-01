using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public class CustomerUpdateDto : BaseDto
    {
        //ID khách hàng
        [Required]
        public Guid CustomerId { get; set; }

        // Mã nhóm khách hàng
        public Guid CustomerGroupId { get; set; }

        //Mã khách hàng
        [Required(ErrorMessage = MISAConst.ERROR_CUSTOMER_EMPTY_CODE)]
        [MaxLength(100, ErrorMessage = MISAConst.ERROR_CUSTOMER_EMPTY_CODE_LENGTH)]
        public string? CustomerCode { get; set; }

        // Họ và tên khách hàng
        [Required(ErrorMessage = MISAConst.ERROR_CUSTOMER_EMPTY_NAME)]
        [MaxLength(255, ErrorMessage = MISAConst.ERROR_CUSTOMER_EMPTY_NAME_LENGTH)]
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
    }
}
