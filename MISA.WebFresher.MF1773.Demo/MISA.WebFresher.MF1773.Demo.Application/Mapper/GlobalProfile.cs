using AutoMapper;
using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MISA.WebFresher.MF1773.Demo.Application
{
    public class GlobalProfile : Profile
    {
        public GlobalProfile()
        {
            CreateMap<Customer, CustomerDto>();

            CreateMap<CustomerCreateDto, Customer>();

            CreateMap<CustomerUpdateDto, Customer>();

            CreateMap<CustomerGroup, CustomerGroupDto>();

            CreateMap<Department, DepartmentDto>();

            CreateMap<Employee, EmployeeDto>();

            CreateMap<EmployeeCreateDto, Employee>();

            CreateMap<EmployeeUpdateDto, Employee>();

            CreateMap<CustomerImportDto, Customer>();

            CreateMap<EmployeeImportDto, Employee>();
        }
    }
}
