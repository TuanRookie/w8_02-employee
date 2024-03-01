using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MISA.WebFresher.MF1773.Demo.Application;
using MISA.WebFresher.MF1773.Demo.Application.IService;
using MISA.WebFresher.MF1773.Demo.Domain;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MISA.WebFresher.MF1773.Demo.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        ITokenService _tokenService;

        public UsersController(IConfiguration config , IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await _userService.LoginAsync(userLogin);
            var token = await _tokenService.Login(user);
            return Ok(new { User = user, Token = token });
        }

        [HttpPost("RenewToken")]
        public async Task<IActionResult> RenewToken(TokenModel tokenModel)
        {
            var renewToken = await _tokenService.RenewToken(tokenModel);

            return Ok(renewToken);

        }
    }
}
