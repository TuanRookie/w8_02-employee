using Dapper;
using MISA.WebFresher.MF1773.Demo.Application;
using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Infractructure
{
    public class CustomerGroupRepository : BaseRepository<CustomerGroup>, ICustomerGroupRepository
    {
        public CustomerGroupRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
