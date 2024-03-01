using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public class CustomerGroupDto : BaseDto
    {
        //ID mã nhóm khách hàng
        public Guid CustomerGroupId { get; set; }
        //Mã nhóm khách hàng
        public string? CustomerGroupCode { get; set; }
        //Tên nhóm khách hàng
        public string? CustomerGroupName { get; set; }
        //Mô tả
        public string? Description { get; set; }
    }
}
