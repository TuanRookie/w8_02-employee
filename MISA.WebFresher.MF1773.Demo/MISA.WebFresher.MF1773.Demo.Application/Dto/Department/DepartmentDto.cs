using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public class DepartmentDto : BaseDto
    {
        //ID mã phòng ban
        public Guid DepartmentId { get; set; }
        //Mã phòng ban
        public string? DepartmentCode { get; set; }
        //Tên phòng ban
        public string? DepartmentName { get; set; }
        //Mô tả
        public string? Description { get; set; }
    }
}
