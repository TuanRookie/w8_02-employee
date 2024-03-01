using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    public class Employee : BaseEntity,IEntity
    {
        // ID khách hàng
        [JsonProperty("Name")]
        public Guid EmployeeId { get; set; }
        // Mã nhóm khách hàng
        public Guid DepartmentId { get; set; }
        // Mã nhân viên
        [DisplayName(ConstDomain.EMPLOYEE_CODE)]
        public string? EmployeeCode { get; set; }
        // Họ và tên nhân viên
        [DisplayName(ConstDomain.FULLNAME)]
        public string? FullName { get; set; }
        // Ngày sinh
        [DisplayName(ConstDomain.DATEOFBIRTH)]
        public DateTime? DateOfBirth { get; set; }
        // Giới tính
        [DisplayName(ConstDomain.GENDER)]
        public GenderEnum? Gender { get; set; }
        // Số điện thoại
        [DisplayName(ConstDomain.PHONENUMBER)]
        public string? PhoneNumber { get; set; }
        // Số điện thoại cố định
        [DisplayName(ConstDomain.LANDLINENUMBER)]
        public string? LandlineNumber { get; set; }
        // Địa chỉ email
        [DisplayName(ConstDomain.EMAIL)]
        public string? Email { get; set; }
        // Địa chỉ
        [DisplayName(ConstDomain.ADDRESS)]
        public string? Address { get; set; }
        // Số căn cước công dân
        [DisplayName(ConstDomain.IDENTITYNUMBER)]
        public string? IdentityNumber { get; set; }
        // Ngày cấp
        [DisplayName(ConstDomain.IDENTITYDATE)]
        public DateTime? IdentityDate { get; set; }
        // Nơi cấp
        [DisplayName(ConstDomain.IDENTITYPLACE)]
        public string? IdentityPlace { get; set; }
        // Số tài khoản ngân hàng
        [DisplayName(ConstDomain.BANKACCOUNT)]
        public string? BankAccount { get; set; }
        // Chi nhánh ngân hàng
        [DisplayName(ConstDomain.BANKBRANCH)]
        public string? BankBranch { get; set; }
        //Tên ngân hàng
        [DisplayName(ConstDomain.BANKNAME)]
        public string? BankName { get; set; }
        //Chức danh
        [DisplayName(ConstDomain.POSITION)]
        public string? Positions { get; set; }
        //Tên phòng ban
        [DisplayName(ConstDomain.DEPARTMENT_NAME)]
        public string? DepartmentName { get; set; }
        //Mã phòng ban
        public string? DepartmentCode { get; set; }

        public Guid GetId()
        {
            return EmployeeId;
        }

        public void SetId(Guid id)
        {
            EmployeeId = id;
        }
    }
}
