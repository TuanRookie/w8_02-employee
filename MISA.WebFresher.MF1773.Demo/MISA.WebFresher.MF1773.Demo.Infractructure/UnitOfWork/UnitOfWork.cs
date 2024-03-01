
using Microsoft.Extensions.Configuration;
using MISA.WebFresher.MF1773.Demo.Application;
using MISA.WebFresher.MF1773.Demo.Domain;
using MISA.WebFresher.MF1773.Demo.Domain.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Infractructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        IDbContext _dbContext;

        public UnitOfWork(IDbContext dbContext, ICustomerRepository customerRepository, IEmployeeRepository employeeRepository)
        {
            _dbContext = dbContext;
            Customers = customerRepository;
            Employees = employeeRepository;
        }

        public ICustomerRepository Customers { get; }

        public IEmployeeRepository Employees { get; }

        public void BeginTransaction()
        {
            _dbContext.Connection.Open();
            _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.Transaction.Commit();
        }

        public void Dispose()
        {
            if (_dbContext.Connection.State == ConnectionState.Open)
            {
                _dbContext.Connection.Close();
            }
            _dbContext.Connection?.Dispose();
        }

        public void Rollback()
        {
            _dbContext.Transaction.Rollback();
        }
    }
}
