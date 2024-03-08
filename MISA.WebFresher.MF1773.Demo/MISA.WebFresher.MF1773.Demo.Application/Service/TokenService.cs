
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MISA.WebFresher.MF1773.Demo.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public class TokenService : ITokenService
    {
        IConfiguration _config;
        IUserRepository _userRepository;
        public TokenService(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        public async Task<TokenModel> Login(User user)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var TokenValidityInMinutes = double.Parse(_config["Jwt:TokenValidityInMinutes"]);
            var RefreshTokenValidityInDays = double.Parse(_config["Jwt:RefreshTokenValidityInDays"]);
            var token = CreateToken(authClaims);

            user.RefreshToken = GenerateRefreshToken();
            user.AccessTokenExpiryTime = DateTime.Now.AddMinutes(TokenValidityInMinutes);
            user.RefreshTRefreshTokenExpiryTime = DateTime.Now.AddDays(RefreshTokenValidityInDays);

            await _userRepository.UpdateAsync(user, user.UserId);

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = user.RefreshToken,
                ExpiredAccessToken = DateTime.Now.AddMinutes(TokenValidityInMinutes),
                ExpiredRefreshToken = DateTime.Now.AddDays(RefreshTokenValidityInDays),
            };
        }

        public async Task<TokenModel> RenewToken(TokenModel tokenModel)
        {
            string? accessToken = tokenModel.AccessToken;
            var TokenValidityInMinutes = double.Parse(_config["Jwt:TokenValidityInMinutes"]);

            var principal = GetPrincipalFromExpiredToken(accessToken);

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var user = await _userRepository.GetUserByToken(tokenModel.RefreshToken.ToString());

            user.RefreshToken = GenerateRefreshToken();
            user.AccessTokenExpiryTime = DateTime.Now.AddMinutes(TokenValidityInMinutes);

            await _userRepository.UpdateAsync(user, user.UserId);

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = user.RefreshToken,
                ExpiredAccessToken = DateTime.Now.AddMinutes(TokenValidityInMinutes),
                ExpiredRefreshToken = tokenModel.ExpiredRefreshToken,
            };

        }

        /// <summary>
        /// Tạo ra chuỗi token truy cập
        /// </summary>
        /// <param name="authClaims"></param>
        /// <returns>JwtSecurityToken</returns>
        /// Created By: DCTuan(29/02/2024)
        public JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var key = _config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var authSigningKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(1),
                claims: authClaims,
                signingCredentials: authSigningKey);

            return token;
        }

        /// <summary>
        /// Hàm set lại thông tin claim
        /// </summary>
        /// <param name="token">thông tin token</param>
        /// <returns></returns>
        /// <exception cref="SecurityTokenException"></exception>
        /// Created By: DCTuan(29/02/2024)
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

        /// <summary>
        /// Hàm lấy ra chuỗi refreshToken
        /// </summary>
        /// <returns>chuỗi refreshToken</returns>
        /// Created By: DCTuan(29/02/2024)
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
