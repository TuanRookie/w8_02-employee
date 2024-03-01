using MISA.WebFresher.MF1773.Demo.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    public class EmployeeValidate : IEmployeeValidate
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeValidate(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task CheckEmployeeExistByCodeAsync(Employee employee)
        {
            var isExistEmployee = await _employeeRepository.IsExistEmployeeAsync(employee.EmployeeCode);
            if (isExistEmployee)
            {
                throw new ConflictException(String.Format( Resource.Resource.Exception_Duplicate_Employee, employee.EmployeeCode), 409);
            }
        }
    }
}
