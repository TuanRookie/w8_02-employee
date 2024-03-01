using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    public class EntitisPaging<T>
    {
        // Tổng số bản ghi
        public int TotalPage { get; set; }
        // Số trang sau khi phân trang
        public int TotalRecord { get; set; }
        // 
        public int? CurrentPage { get; set; }
        public int? CurrentPageRecords { get; set; }
        public List<T> Data { get; set; }
    }
}
