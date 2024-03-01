using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    public abstract class BaseEntity
    {
        // Người thêm
        public string? CreatedBy { get; set; }
        // Ngày thêm
        public DateTime? CreatedDate { get; set; }
        // Người sửa đổi
        public string? ModifiedBy { get; set; }
        // Ngày sửa đổi
        public DateTime? ModifiedDate { get; set; }
    }
}
