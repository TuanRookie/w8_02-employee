using Microsoft.Extensions.Configuration;
using MISA.WebFresher.MF1773.Demo.Infractructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain.UnitTests
{
    [TestFixture]
    public class EmployeeValidateUnitTests
    {
        [Test]
        public async Task CheckEmployeeExistByCodeAsync_NotExistEmployee_Success()
        {
            //Arrange
            var employee = new Employee();

            var employeeRepository = new EmployeeRepositoryFake();

            var employeeValidate = new EmployeeValidate(employeeRepository);

            //Act
            await employeeValidate.CheckEmployeeExistByCodeAsync(employee);

            //Assert
            Assert.That(employeeRepository.TotalCall, Is.EqualTo(1));
        }

        [Test]
        public void CheckEmployeeExistByCodeAsync_NotExistEmployee_ThrowException()
        {
            //Arrange
            var employee = new Employee();

            var employeeRepository = new EmployeeRepositoryFake1();

            var employeeValidate = new EmployeeValidate(employeeRepository);

            //Act & Assert
            var exception = Assert.ThrowsAsync<ConflictException>(async () => await employeeValidate.CheckEmployeeExistByCodeAsync(employee));

            Assert.That(exception.ErrorCode, Is.EqualTo(409));

            Assert.That(employeeRepository.TotalCall, Is.EqualTo(1));
        }
    }
}
