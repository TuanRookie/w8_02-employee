using AutoMapper;
using MISA.WebFresher.MF1773.Demo.Domain;
using MISA.WebFresher.MF1773.Demo.Domain.Repository;
using MISA.WebFresher.MF1773.Demo.Infractructure;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application.UnitTests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        public IEmployeeRepository? EmployeeRepository { get; set; }
        public IEmployeeValidate? EmployeeValidate { get; set; }
        public IMapper? Mapper { get; set; }
        public EmployeeService? EmployeeService { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public IDepartmentRepository? DepartmentRepository { get; set; }
        [SetUp]
        public void SetUp()
        {
            EmployeeRepository = Substitute.For<IEmployeeRepository>();
            EmployeeValidate = Substitute.For<IEmployeeValidate>();
            Mapper = Substitute.For<IMapper>();
            UnitOfWork = Substitute.For<IUnitOfWork>();
            EmployeeService = Substitute.For<EmployeeService>(EmployeeRepository, EmployeeValidate, Mapper, DepartmentRepository, UnitOfWork);
        }
        /// <summary>
        /// Test ham InsertAsync entity.FixedAsset not empty
        /// </summary>
        /// <returns></returns>
        ///  CreatedBy: DCTuan 23/02/2024
        [Test]
        public async Task InsertAsync_EmptyEmployeeId_ReturnIdNotEmpty()
        {
            //Arrange
            var employeeCreateDto = new EmployeeCreateDto();
            var employee = new Employee();

            EmployeeService.MapCreateDtoToEntity(employeeCreateDto).Returns(employee);

            //Act
            var rerult = await EmployeeService.InsertAsync(employeeCreateDto);

            //Assert
            Assert.That(employee.EmployeeId, Is.Not.EqualTo(Guid.Empty));
        }

        /// <summary>
        /// Test ham InsertAsync thêm thành công
        /// </summary>
        /// <returns></returns>
        ///  CreatedBy: DCTuan 23/02/2024
        [Test]
        public async Task InsertAsync_Success_ReturnEmployeeDto()
        {
            //Arrange
            var employeeCreateDto = new EmployeeCreateDto();
            var employee = new Employee();
            employee.EmployeeId = Guid.NewGuid();
            var employeeDto = new EmployeeDto();

            EmployeeService.MapCreateDtoToEntity(employeeCreateDto).Returns(employee);
            EmployeeService.MapEntityToDto(employee).Returns(employeeDto);

            //Act
            var rerult = await EmployeeService.InsertAsync(employeeCreateDto);
            //Assert
            Assert.That(rerult, Is.EqualTo(employeeDto));
        }

        /// <summary>
        /// Test ham InsertAsync th truyen null
        /// </summary>
        /// <returns></returns>
        ///  CreatedBy: DCTuan 23/02/2024
        [Test]
        public async Task InsertAsync_ParamNull()
        {
            //Arrange
            var employeeCreateDto = new EmployeeCreateDto();

            //Act &&  Assert

            var exception = Assert.ThrowsAsync<NullReferenceException>(async () => await EmployeeService.InsertAsync(employeeCreateDto));
            Assert.That(exception.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }

        /// <summary>
        /// Test ham UpdateAsyn success return not null
        /// </summary>
        /// <returns></returns>
        ///  CreatedBy: DCTuan 23/02/2024
        [Test]
        public async Task UpdateAsync_Success_ReturnNotNull()
        {
            //Arrange
            var id = Guid.NewGuid();
            var employeeUpdateDto = new EmployeeUpdateDto();
            var employee = new Employee()
            {
                EmployeeId = Guid.NewGuid()
            }; var employeeDto = new EmployeeDto()
            {
                EmployeeId = Guid.NewGuid()
            };
            EmployeeRepository.GetAsync(id).Returns(employee);
            EmployeeService.MapEntityToDto(null).Returns(employeeDto);

            //Act
            var rerult = await EmployeeService.UpdateAsync(id, employeeUpdateDto);

            //Assert
            Assert.That(rerult, Is.Not.EqualTo(null));
        }
        
        /// <summary>
        /// Test ham UpdateAsyn false return null
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: DCTuan 27/02/2024
        [Test]
        public async Task UpdateAsync_False_ReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();
            var employeeUpdateDto = new EmployeeUpdateDto();
            var employee = new Employee()
            {
                EmployeeId = Guid.NewGuid()
            }; var employeeDto = new EmployeeDto()
            {
                EmployeeId = Guid.NewGuid()
            };
            /* EmployeeRepository.GetAsync(id).Returns(employee);
             EmployeeService.MapEntityToDto(null).Returns(employeeDto);*/

            //Act
            var rerult = await EmployeeService.UpdateAsync(id, employeeUpdateDto);

            //Assert
            Assert.That(rerult, Is.EqualTo(null));
        }
        /// <summary>
        /// Test ham DeleteAsyn success return true
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: DCTuan 27/02/2024
        [Test]
        public async Task DeleteAsync_Success_ReturnTrue()
        {
            //Arrange
            var id = Guid.NewGuid();
            Employee employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
            };
            //Act
            EmployeeRepository.DeleteAsync(id).Returns(true);
            var rerult = await EmployeeService.DeleteAsync(employee.EmployeeId);

            //Assert
            Assert.That(rerult, Is.EqualTo(true));
            await EmployeeRepository.Received(1).GetAsync(id);
            await EmployeeRepository.Received(1).DeleteAsync(employee.EmployeeId);
        }
        /// <summary>
        /// Test ham DeleteAsyn False return false
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: DCTuan 27/02/2024
        [Test]
        public async Task DeleteAsync_False_ReturnFalse()
        {
            //Arrange
            var id = Guid.NewGuid();
            Employee employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
            };
            //Act
            EmployeeRepository.DeleteAsync(null).Returns(true);
            var rerult = await EmployeeService.DeleteAsync(id);
            //Assert
            Assert.That(rerult, Is.EqualTo(false));
            await EmployeeRepository.Received(1).GetAsync(id);
            await EmployeeRepository.Received(1).DeleteAsync(null);
        }

        /// <summary>
        /// Test ham DeleteAsyn false return Empty
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: DCTuan 27/02/2024
        [Test]
        public async Task DeleteManyAsync_False_ReturnEmpty()
        {
            //Arrange
            var ids = new List<Guid>();
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            ids.Add(id1);
            ids.Add(id2);
            Employee employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
            };
            //Act
            var rerult = await EmployeeService.DeleteManyAsync(ids);

            //Assert
            Assert.That(rerult, Is.Empty);
            await EmployeeRepository.Received(1).GetByListIdAsync(ids);
            await EmployeeRepository.Received(0).DeleteManyAsync(null);
        }
        /// <summary>
        /// Test ham DeleteAsyn success return Empty
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: DCTuan 27/02/2024
        [Test]
        public async Task DeleteManyAsync_Success_ReturnEmpty()
        {
            //Arrange
            var ids = new List<Guid>();
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            ids.Add(id1);
            ids.Add(id2);
            var entitys = new List<Employee>();
            Employee employee1 = new Employee();
            Employee employee2 = new Employee();
            entitys.Add(employee1);
            entitys.Add(employee2);
            EmployeeRepository.GetByListIdAsync(ids).Returns(entitys);
            //Act
            var rerult = await EmployeeService.DeleteManyAsync(ids);

            //Assert
            Assert.That(rerult.Count, Is.EqualTo(2));
            await EmployeeRepository.Received(1).GetByListIdAsync(ids);
            await EmployeeRepository.Received(1).DeleteManyAsync(entitys);
        }
    }
}
