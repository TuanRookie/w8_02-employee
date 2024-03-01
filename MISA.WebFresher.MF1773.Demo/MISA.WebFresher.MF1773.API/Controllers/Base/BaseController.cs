

using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher.MF1773.Demo.Application;

namespace MISA.WebFresher.MF1773.Demo.API.Controllers
{
    public class BaseController<TDto, TCreateDto, TUpdateDto> : BaseReadOnlyController<TDto>
    {
        protected readonly IBaseService<TDto, TCreateDto, TUpdateDto> BaseService;
        public BaseController(IBaseService<TDto, TCreateDto, TUpdateDto> baseService) : base(baseService)
        {
            BaseService = baseService;
        }

        /// <summary>
        /// Xóa một entity khỏi hệ thống.
        /// </summary>
        /// <param name="id">Mã định danh của entity cần xoá </param>
        /// <returns>true nếu xóa thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        [HttpDelete]
        [Route("{id}")]
        public async Task<bool> DeleteAsync(Guid
           id)
        {
            var result = await BaseService.DeleteAsync(id);

            return result;
        }


        /// <summary>
        /// Thêm một entity mới vào hệ thống.
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns>true nếu thêm thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate> 
        [HttpPost]
        public async Task<TDto> InsertAsync(TCreateDto createDto)
        {
            var result = await BaseService.InsertAsync(createDto);

            return result;
        }
        /// <summary>
        /// Cập nhật thông tin của một entity.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        /// <returns>true nếu cập nhật thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate> 
        [HttpPut]
        [Route("{id}")]
        public async Task<TDto> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var result = await BaseService.UpdateAsync(id, updateDto);

            return result;
        }
        /// <summary>
        /// Xóa nhieu entity khỏi hệ thống.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>true nếu xóa thành công, false nếu không thành công</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>  
        [HttpDelete("DeleteMany")]
        public async Task<bool> DeleteManyAsync([FromBody] List<string> ids)
        {
            var guidList = ids.Select(id => Guid.Parse(id)).ToList();

            var result = await BaseService.DeleteManyAsync(guidList);

            return result;
        }
    }
}
