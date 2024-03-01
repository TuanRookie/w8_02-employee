using Dapper;
using MISA.WebFresher.MF1773.Demo.Application;
using MISA.WebFresher.MF1773.Demo.Domain;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher.MF1773.Demo.Infractructure
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {

        /*
        public override DynamicParameters ConvertToDynamicParameters(Customer entity, Guid id)
        {
            var param = new DynamicParameters();
            param.Add("CustomerId", id);
            param.Add("CustomerGroupId", entity.CustomerGroupId);
            param.Add("CustomerCode", entity.CustomerCode);
            param.Add("FullName", entity.FullName);
            param.Add("DateOfBirth", entity.DateOfBirth);
            param.Add("Gender", entity.Gender);
            param.Add("Email", entity.Email);
            param.Add("PhoneNumber", entity.PhoneNumber);
            param.Add("Address", entity.Address);
            param.Add("DebitAmount", entity.DebitAmount);
            param.Add("CompanyName", entity.CompanyName);
            param.Add("IdentityNumber", entity.IdentityNumber);
            param.Add("IdentityDate", entity.IdentityDate);
            param.Add("IdentityPlace", entity.IdentityPlace);
            param.Add("CreatedBy", entity.CreatedBy);
            param.Add("CreatedDate", entity.CreatedDate);
            param.Add("ModifiedBy", entity.ModifiedBy);
            param.Add("ModifiedDate", entity.ModifiedDate);
            return param;
        }
        */
        public CustomerRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsExistCustomerAsync(string entityCode)
        {
            var proc = $"Proc_CheckCustomerCodeExits";

            var param = new DynamicParameters();
            param.Add($"CustomerCode", entityCode);

            var result = await
              _dbContext.Connection.ExecuteScalarAsync<bool>(
                   proc,
                    param,
                   commandType: CommandType.StoredProcedure,
                   transaction: _dbContext.Transaction
                   );

            return result;
        }
    }
}
