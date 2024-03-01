using MISA.WebFresher.MF1773.Demo.Domain;
using MISA.WebFresher.MF1773.Demo.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }

        IEmployeeRepository Employees { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
