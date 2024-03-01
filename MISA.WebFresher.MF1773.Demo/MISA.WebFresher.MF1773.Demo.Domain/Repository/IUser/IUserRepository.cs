using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Domain
{
    public interface IUserRepository
    {
        /// <summary>
        /// Lấy ra tài khoản
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        Task<User?> GetUser(UserLogin userLogin);

        Task<bool> UpdateAsync(User user, Guid userId);

        Task<User?> GetUserByToken(string token);
    }
}
