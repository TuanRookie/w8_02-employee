using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public class EmployeeUpdateDto
    {
        //ID nhân viên
        [Required]
        public Guid EmployeeId { get; set; }

        // Mã phòng ban
        [Required]
        public Guid DepartmentId { get; set; }

        //Mã nhân viên
        [Required(ErrorMessage = MISAConst.ERROR_EMPLOYEE_EMPTY_CODE)]
        [MaxLength(100, ErrorMessage = MISAConst.ERROR_EMPLOYEE_EMPTY_CODE_LENGTH)]
        public string? EmployeeCode { get; set; }

        // Họ và tên nhân viên
        [Required(ErrorMessage = MISAConst.ERROR_EMPLOYEE_EMPTY_NAME)]
        [MaxLength(255, ErrorMessage = MISAConst.ERROR_EMPLOYEE_EMPTY_NAME_LENGTH)]
        public string? FullName { get; set; }

        // Ngày sinh
        public DateTime? DateOfBirth { get; set; }

        // Giới tính
        public GenderEnum? Gender { get; set; }

        // Số điện thoại
        public string? PhoneNumber { get; set; }

        // Số điện thoại cố định
        public string? LandlineNumber { get; set; }

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

        // Số tài khoản ngân hàng
        public string? BankAccount { get; set; }
        // Chi nhánh ngân hàng
        public string? BankBranch { get; set; }
        //Tên ngân hàng
        public string? BankName { get; set; }
        //Chức danh
        public string? Positions { get; set; }
        //Tên phòng ban
        public string? DepartmentName { get; set; }
        //Mã phòng ban
        public string? DepartmentCode { get; set; }
    }
}
