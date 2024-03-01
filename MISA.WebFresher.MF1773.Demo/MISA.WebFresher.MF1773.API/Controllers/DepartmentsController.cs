using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher.MF1773.Demo.Application;

namespace MISA.WebFresher.MF1773.Demo.API.Controllers
{
    public class DepartmentsController : BaseReadOnlyController<DepartmentDto>
    {
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
        }
    }
}
