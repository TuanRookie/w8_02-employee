using AutoMapper;
using MISA.WebFresher.MF1773.Demo.Domain;
using MISA.WebFresher.MF1773.Demo.Domain.Repository;
using MISA.WebFresher.MF1773.Demo.Infractructure;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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
        /// Test ham InsertAsync entity.Employee not empty
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
        /// Test ham InsertAsync không có CreatedBy
        /// </summary>
        /// <returns></returns>
        ///  CreatedBy: DCTuan 23/02/2024
        [Test]
        public async Task InsertAsync_EmployeeAuditNull_EmployeeAuditNotNull()
        {
            //Arrange
            var employeeCreateDto = new EmployeeCreateDto();

            var employee = new Employee()
            {
                EmployeeId = Guid.NewGuid()
            };

            EmployeeService.MapCreateDtoToEntity(employeeCreateDto).Returns(employee);

            //Act
            var rerult = await EmployeeService.InsertAsync(employeeCreateDto);

            //Assert
            Assert.That(employee.CreatedBy, Is.EqualTo("Đinh Công Tuấn"));
        }

        /// <summary>
        /// Test ham InsertAsync thêm thành công
        /// </summary>
        /// <returns></returns>
        ///  CreatedBy: DCTuan 23/02/2024
        [Test]
        public async Task InsertAsync_ValidInput_Success()
        {
            //Arrange
            var employeeCreateDto = new EmployeeCreateDto();
            var employee = new Employee();
            employee.EmployeeId = Guid.NewGuid();

            EmployeeService.MapCreateDtoToEntity(employeeCreateDto).Returns(employee);

            //Act
            var rerult = await EmployeeService.InsertAsync(employeeCreateDto);

            //Assert
            await EmployeeService.Received(1).ValidateCreateBusiness(employee);

            await EmployeeRepository.Received(1).InsertAsync(employee);
        }

        /// <summary>
        /// Test ham UpdateAsync không có ModifiedBy
        /// </summary>
        /// <returns></returns>
        ///  CreatedBy: DCTuan 23/02/2024
        [Test]
        public async Task UpdateAsync_ValidInput_Success()
        {
            //Arrange
            var employeeUpdateDto = new EmployeeUpdateDto();
            var employee = new Employee();
            var id = new Guid();
            var newEmployee = new Employee();

            EmployeeRepository.GetAsync(id).Returns(employee);
            EmployeeService.MapUpdateDtoToEntity(employeeUpdateDto,employee).Returns(newEmployee);

            //Act
            var rerult = await EmployeeService.UpdateAsync(id, employeeUpdateDto);

            //Assert
            await EmployeeService.Received(1).ValidateUpdateBusiness(newEmployee);

            await EmployeeRepository.Received(1).UpdateAsync(id, newEmployee);
        }
        
        /// <summary>
        /// Test ham UpdateAsyn false return null
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: DCTuan 27/02/2024
        [Test]
        public async Task UpdateAsync_EmployeeAuditNull_EmployeeAuditNotNull()
        {
            //Arrange
            var employeeUpdateDto = new EmployeeUpdateDto();
            var employee = new Employee();
            var id = new Guid();
            var newEmployee = new Employee();

            EmployeeRepository.GetAsync(id).Returns(employee);
            EmployeeService.MapUpdateDtoToEntity(employeeUpdateDto, employee).Returns(newEmployee);

            //Act
            var rerult = await EmployeeService.UpdateAsync(id, employeeUpdateDto);

            //Assert
            Assert.That(employee.ModifiedBy, Is.EqualTo("Đinh Công Tuấn"));
        }
       
        /// <summary>
        /// Test ham DeleteAsync  trả về true
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: DCTuan 27/02/2024
        [Test]
        public async Task DeleteAsync_Success_ReturnTrue()
        {
            //Arrange
            var employee = new Employee();
            EmployeeRepository.GetAsync(employee.EmployeeId).Returns(employee);
            EmployeeRepository.DeleteAsync(employee.EmployeeId).Returns(true);

            //Act
            var reslut = await EmployeeService.DeleteAsync(employee.EmployeeId);

            //Assert
            Assert.That(reslut, Is.EqualTo(true));
            await EmployeeRepository.Received(1).GetAsync(employee.EmployeeId);
            await EmployeeRepository.Received(1).DeleteAsync(employee.EmployeeId);
        }
        /// <summary>
        /// Test hamf DeleteManyAsync trả về true
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: DCTuan 27/02/2024
        [Test]
        public async Task DeleteManyAsync_Success_ReturnTrue()
        {
            //Arrange
            List<Guid> ids = new List<Guid>();
            var listEmployee = new List<Employee>();
            EmployeeRepository.GetByListIdAsync(ids).Returns(listEmployee);
            EmployeeRepository.DeleteManyAsync(ids).Returns(true);

            //Act
            var reslut = await EmployeeService.DeleteManyAsync(ids);

            //Assert
            Assert.That(reslut, Is.EqualTo(true));
            await EmployeeRepository.Received(1).GetByListIdAsync(ids);
            await EmployeeRepository.Received(1).DeleteManyAsync(ids);
        }
        /// <summary>
        /// Test hàm ExportAllAsync trả về kết quả file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: DCTuan 27/02/2024
        [Test]
        public async Task ExportAllAsync_Success_ReturnFile()
        {
            //Arrange
            List<Employee> listEmployee = new List<Employee>();
            EmployeeRepository.GetAllAsync().Returns(listEmployee);
            EmployeeService.ExportExcelAsync(listEmployee);

            //Act
            var reslut = await EmployeeService.ExportAllAsync();

            //Assert
            await EmployeeRepository.Received(1).GetAllAsync();
           
        }
        /// <summary>
        /// Test hàm GetEmployeePagingAsync trả về kết quả EmployeePaging(tổng số trang, tổng số bản ghi, Trang hiện tại, dữ liệu phân trang)
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: DCTuan 27/02/2024
        [Test]
        public async Task GetEmployeePagingAsync_Success_ReturnEmployeePaging()
        {
            //Arrange
            var pageSize = new int();
            var pageNumber = new int();
            string employeeFilter = "";
            EntitisPaging<Employee> entitisPaging = new EntitisPaging<Employee>();

            EmployeeRepository.GetEntityPagingAsync(pageSize, pageNumber, employeeFilter).Returns(entitisPaging);
            Mapper.Map<List<EmployeeDto>>(entitisPaging.Data);

            //Act
            var result = await EmployeeService.GetEmployeePagingAsync(pageSize, pageNumber, employeeFilter);

            //Assert
            await EmployeeRepository.Received(1).GetEntityPagingAsync(pageSize,pageNumber,employeeFilter);
        }

        /// <summary>
        /// Test ham GetNewEmployeeCode trả về EmployeeeCode mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: DCTuan 27/02/2024
        [Test]
        public async Task GetNewEmployeeCode_Success_ReturnNewEmployeeCode()
        {
            //Arrange
            EmployeeRepository.GetNewEmployeeCode();

            //Act
            var result = await EmployeeService.GetNewEmployeeCode();

            //Assert
            await EmployeeRepository.Received(2).GetNewEmployeeCode();
        }

        
    }
}
