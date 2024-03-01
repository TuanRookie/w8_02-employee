using MISA.WebFresher.MF1773.Demo.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain.UnitTests
{
    public class EmployeeRepositoryFake1 : IEmployeeRepository
    {
        public int TotalCall { get; set; }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteManyAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetByListIdAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<EntitisPaging<Employee>> GetEntityPagingAsync(int? pageSize, int? pageNumber, string? entityFilter)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNewEmployeeCode()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistEmployeeAsync(string entityCode)
        {
            TotalCall++;
            return Task.FromResult(true);
        }

        public Task<bool> UpdateAsync(Guid id, Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
