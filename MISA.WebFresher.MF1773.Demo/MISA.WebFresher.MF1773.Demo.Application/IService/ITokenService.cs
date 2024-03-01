using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public interface ITokenService
    {
        /// <summary>
        /// Hàm đăng nhập vào hệ thống
        /// </summary>
        /// <param name="user">thông tin đăng nhập</param>
        /// <returns>
        /// Thông tin của token gồm access token và refreshToken
        /// </returns
        /// Created By: DCTuan(29/02/2024)
        Task<TokenModel> Login(User user);

        /// <summary>
        /// Hàm lấy ra thông tin token mới khi token cũ hết hạn
        /// </summary>
        /// <param name="tokenModel">Thông tin token cũ</param>
        /// <returns>
        /// Thông tin của token mới gồm access token và refreshToken
        /// </returns
        /// Created By: DCTuan(29/02/2024)
        Task<TokenModel> RenewToken(TokenModel tokenModel);
    }
}
