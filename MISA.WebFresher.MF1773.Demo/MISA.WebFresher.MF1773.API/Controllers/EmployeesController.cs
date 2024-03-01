using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher.MF1773.Demo.Application;
using MISA.WebFresher.MF1773.Demo.Domain;
using MISA.WebFresher.MF1773.Demo.Domain.Resource;
using MISA.WebFresher.MF1773.Demo.Infractructure;
using System.Data;
using System.Security.Authentication;

namespace MISA.WebFresher.MF1773.Demo.API.Controllers
{

    public class EmployeesController : BaseController<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("filter")]
        [Authorize(Roles = "Admin")]
        public async Task<EntitisPaging<EmployeeDto>> Get(int? pageSize, int? pageNumber,
            string? employeeFilter)
        {
            try
            {
                var res = await _employeeService.GetEmployeePagingAsync(pageSize, pageNumber, employeeFilter);
                return res;
            }
            catch(AuthenticationException)
            {
                throw new ConnectDbException(Resource.UnauthorizedAccess, 500);
            }
            

        }

        [HttpGet("NewEmployeeCode")]
        public async Task<IActionResult> GetNewEmployeeCode()
        {
            var newEmployeeCode = await _employeeService.GetNewEmployeeCode();
            if (newEmployeeCode != null)
                return Ok(newEmployeeCode);
            else
                return NoContent();
        }

        [HttpGet("Export")]
        public async Task<IActionResult> ExportToExcel()
        {
            var contenType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"Danh sách nhân viên.xlsx";
            var res = await _employeeService.ExportAllAsync();

            return File(res, contenType, fileName);
        }

        [HttpPost("Import")]
        public async Task<IActionResult> Import(IFormFile formFile)
        {
            var res = await _employeeService.ImportEmployeeAsync(formFile);
            return Ok(res);
        }
    }
}
