using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    /// <summary>
    /// Classs validate nghiệp vụ
    /// </summary>
    public class CustomerValidate : ICustomerValidate
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerValidate(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task CheckCustomerExistByCodeAsync(Customer customer)
        {
            var isExistCustomer = await _customerRepository.IsExistCustomerAsync(customer.CustomerCode);
            if (isExistCustomer)
            {
                throw new ConflictException(MISA.WebFresher.MF1773.Demo.Domain.Resource.Resource.Exception_Duplicate_Customer, 409);
            }

        }
    }
}
