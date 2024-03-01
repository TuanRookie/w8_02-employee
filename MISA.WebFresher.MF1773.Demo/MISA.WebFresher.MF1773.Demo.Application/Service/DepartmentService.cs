using AutoMapper;
using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public class DepartmentService : BaseReadOnlyService<Department, DepartmentDto>, IDepartmentService
    {
        private readonly IMapper _mapper;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : base(departmentRepository)
        {
            _mapper = mapper;
        }

        public override DepartmentDto MapEntityToDto(Department entity)
        {
             var departmentDto = _mapper.Map<DepartmentDto>(entity);
            return departmentDto;
        }
    }
}
