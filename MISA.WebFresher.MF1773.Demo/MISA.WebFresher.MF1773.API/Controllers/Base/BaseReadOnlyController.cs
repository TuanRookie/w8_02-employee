using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher.MF1773.Demo.Application;

namespace MISA.WebFresher.MF1773.Demo.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseReadOnlyController<TDto> : ControllerBase
    {
        protected readonly IBaseReadOnlyService<TDto> BaseReadOnlyService;

        public BaseReadOnlyController(IBaseReadOnlyService<TDto> baseReadOnlyService)
        {
            BaseReadOnlyService = baseReadOnlyService;
        }
        // <summary>
        /// Lấy danh sách tất cả entity.
        /// </summary>
        /// <returns>Danh sách tất cả entity</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        [HttpGet]
        public async Task<List<TDto>> GetAllAsync()
        {
            var result = await BaseReadOnlyService.GetAllAsync();

            return result;
        }
        /// <summary>
        /// Lấy 1 entity theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate> 
        [HttpGet]
        [Route("{id}")]
        public async Task<TDto?> GetCustomerAsync(Guid
            id)
        {
            var result = await BaseReadOnlyService.GetAsync(id);

            return result;
        }
    }
}
