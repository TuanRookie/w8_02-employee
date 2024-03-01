using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public class CustomerImportDto : CustomerDto
    {
        public CustomerImportDto()
        {
            ImportInvalidErrors = new List<string>();
        }
        public List<string> ImportInvalidErrors {  get; set; }
        public bool IsImport { get; set; }
    }
}
