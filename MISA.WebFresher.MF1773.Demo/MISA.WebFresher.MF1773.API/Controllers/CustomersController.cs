
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher.MF1773.Demo.Application;


namespace MISA.WebFresher.MF1773.Demo.API.Controllers
{
    public class CustomersController : BaseController<CustomerDto, CustomerCreateDto, CustomerUpdateDto>
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService) : base(customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Get(int? pageSize, int? pageNumber,
            string? customerFilter)
        {
            var res = await _customerService.GetCustomerPagingAsync(pageSize, pageNumber, customerFilter);
            if (res.Data.Count() > 0)
                return Ok(res);
            else
                return NoContent();

        }

        [HttpGet("Export")]
        public async Task<IActionResult> ExportToExcel()
        {
            var contenType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"Danh sách khách hàng.xlsx";
            var res = await _customerService.ExportAllAsync();

            return File(res, contenType, fileName);
        }

        [HttpPost("Import")]
        public async Task<IActionResult> Import(IFormFile formFile)
        {
            var res = await _customerService.ImportCustomerAsync(formFile);
            return Ok(res);
        }
    }
}
