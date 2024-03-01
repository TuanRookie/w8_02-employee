using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public abstract class BaseReadOnlyService<TEntity, TDto> : IBaseReadOnlyService<TDto>
    {
       protected readonly IBaseRepository<TEntity> BaseRepository;

        protected BaseReadOnlyService(IBaseRepository<TEntity> baseRepository)
        {
            BaseRepository = baseRepository;
        }

        public async Task<List<TDto>> GetAllAsync()
        {
            var entities = await BaseRepository.GetAllAsync();
            var result = entities.Select(entity => MapEntityToDto(entity)).ToList();
            return result;
        }

        public async Task<TDto> GetAsync(Guid id)
        {
            var entity = await BaseRepository.GetAsync(id);
            var result = MapEntityToDto(entity);
            return result;
        }

        public abstract TDto MapEntityToDto(TEntity entity);
    }
}
