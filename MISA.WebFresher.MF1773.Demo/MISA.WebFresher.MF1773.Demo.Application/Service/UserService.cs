
using MISA.WebFresher.MF1773.Demo.Application.IService;
using MISA.WebFresher.MF1773.Demo.Domain;
using MISA.WebFresher.MF1773.Demo.Domain.Resource;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> LoginAsync(UserLogin userLogin)
        {
            var user = await _userRepository.GetUser(userLogin);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new ValidateException(String.Format(Resource.Exception_Account), 400);
            }
        }

        public async Task<bool> UpdateServiceAsyns(User user, Guid userId)
        {
            var res = await _userRepository.UpdateAsync(user, userId);
            return res;
        }
    }
}
