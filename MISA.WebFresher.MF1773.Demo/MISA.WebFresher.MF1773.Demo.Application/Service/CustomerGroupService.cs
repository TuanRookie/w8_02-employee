using AutoMapper;
using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public class CustomerGroupService : BaseReadOnlyService<CustomerGroup, CustomerGroupDto>, ICustomerGroupService
    {
        private readonly IMapper _mapper;
        public CustomerGroupService(ICustomerGroupRepository customerGroupRepository , IMapper mapper) : base(customerGroupRepository)
        {
            _mapper = mapper;
        }

        public override CustomerGroupDto MapEntityToDto(CustomerGroup entity)
        {
            var customerGroupDto = _mapper.Map<CustomerGroupDto>(entity);
            return customerGroupDto;
        }
    }
}
