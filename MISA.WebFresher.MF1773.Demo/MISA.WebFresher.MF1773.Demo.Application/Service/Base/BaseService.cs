using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public abstract class BaseService<TEntity, TDto, TCreateDto, TUpdateDto> : BaseReadOnlyService<TEntity, TDto>,
    IBaseService<TDto, TCreateDto, TUpdateDto> where TEntity : IEntity
    {

        protected BaseService(IBaseRepository<TEntity> baseRepository) : base(baseRepository)
        {
        }

        public async Task<TDto> InsertAsync(TCreateDto createDto)
        {
            var entity = MapCreateDtoToEntity(createDto);

            //Nếu không có giá trị ID gửi xuống
            if (entity.GetId() == Guid.Empty)
            {
                entity.SetId(Guid.NewGuid());
            }

            //Nếu không có giá trị truyền xuống thì gán giá trị
            if(entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedBy ??= "Đinh Công Tuấn";
                baseEntity.CreatedDate ??= DateTime.Now;

                baseEntity.ModifiedBy ??= "Đinh Công Tuấn";
                baseEntity.ModifiedDate ??= DateTime.Now;
            }

            await ValidateCreateBusiness(entity);

            await BaseRepository.InsertAsync(entity);

            var result = MapEntityToDto(entity);

            return result;
        }

        public async Task<TDto> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var entity = await BaseRepository.GetAsync(id);

            var newEntity = MapUpdateDtoToEntity(updateDto, entity);

            //Nếu không có giá trị truyền xuống thì gán giá trị
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.ModifiedBy ??= "Đinh Công Tuấn";
                baseEntity.ModifiedDate ??= DateTime.Now;
            }

            await ValidateUpdateBusiness(newEntity);

            await BaseRepository.UpdateAsync(id, newEntity);

            var result = MapEntityToDto(entity);

            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await BaseRepository.GetAsync(id);

            var result = await BaseRepository.DeleteAsync(id);

            return result; 

        }

        public async Task<bool> DeleteManyAsync(List<Guid> ids)
        {
            var entities = await BaseRepository.GetByListIdAsync(ids);

            var result = await BaseRepository.DeleteManyAsync(ids);

            return result;
        }

        public async Task<byte[]> ExportAllAsync()
        {
            var data = await BaseRepository.GetAllAsync();
            var bytes = ExportExcelAsync(data);
            return bytes;
        }

        public abstract TEntity MapCreateDtoToEntity(TCreateDto createDto);

        public virtual async Task ValidateCreateBusiness(TEntity entity)
        {
            await Task.CompletedTask;
        }

        public abstract TEntity MapUpdateDtoToEntity(TUpdateDto updateDto, TEntity entity);

        public virtual async Task ValidateUpdateBusiness(TEntity newEntity)
        {
            await Task.CompletedTask;
        }

        public abstract byte[] ExportExcelAsync(List<TEntity> data);
        
    }
}
