using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public interface IBaseReadOnlyService<TDto>
    {
        /// <summary>
        /// Lấy danh sách tất cả entity.
        /// </summary>
        /// <returns>Danh sách tất cả entity</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        Task<List<TDto>> GetAllAsync();

        /// <summary>
        /// Lay 1 entity theo id
        /// </summary>
        /// <param name="id">Dinh danh entity</param>
        /// <exception cref="NotFoundException">Khi không tìm thấy bản ghi</exception>
        /// <returns>Một entity</returns>
        /// <CreatedBy>DCTuan</CreatedBy>
        /// <CreatedDate>(25/12/2023)</CreatedDate>
        Task<TDto> GetAsync(Guid id);

    }
}
