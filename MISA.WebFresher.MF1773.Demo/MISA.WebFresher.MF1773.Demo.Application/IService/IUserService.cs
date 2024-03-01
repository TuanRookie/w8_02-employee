using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application.IService
{
    public interface IUserService
    {
        /// <summary>
        /// Kiếm tra xem tài khoản đăng nhập có hợp lệ hay không
        /// </summary>
        /// <param name="userLogin">thông tin đăng nhập</param>
        /// <exception cref="ValidateException"></exception>
        /// <returns>Thông tin tài khoản hợp lệ</returns>
        /// Created By: DCTuan(20/02/2024)
        Task<User> LoginAsync(UserLogin userLogin);

        /// <summary>
        /// Cập nhật thời gian refresh token User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userId"></param>
        /// <returns>Cập nhật thành công trả về true,cập nhật không thành công trả về false</returns>
        /// Created By: DCTuan(29/02/2024)
        Task<bool> UpdateServiceAsyns(User user, Guid userId);

    }
}
