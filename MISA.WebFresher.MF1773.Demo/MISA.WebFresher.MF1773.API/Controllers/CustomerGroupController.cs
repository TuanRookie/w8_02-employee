using MISA.WebFresher.MF1773.Demo.Application;

namespace MISA.WebFresher.MF1773.Demo.API.Controllers
{
    public class CustomerGroupController : BaseReadOnlyController<CustomerGroupDto>
    {
        public CustomerGroupController(ICustomerGroupService customerGroupService) : base(customerGroupService)
        {
        }
    }
}
