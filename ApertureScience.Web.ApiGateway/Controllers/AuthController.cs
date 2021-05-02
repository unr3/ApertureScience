using ApertureScience.Web.ApiGateway.Commons;
using ApertureScience.Web.ApiGateway.Data;
using ApertureScience.Web.ApiGateway.Services;
using ApertureScience.Web.ApiGateway.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApertureScience.Web.ApiGateway.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginRepository _repo;
        private readonly ITokenService _tokenService;

        public AuthController(ILoginRepository repo, ITokenService tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoggedInUserViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public async Task<ActionResult<LoggedInUserViewModel>> Authenticate(AuthenticationModel authentication)
        {
            
            var user =await _repo.FindUser(authentication.EmailAddress);
            
            if (user == null)
            {
                return Unauthorized(new { message = "email or password is incorrect" });
            }
            var password = HashService.Hash(authentication.Password, $"{user.Email}{user.Code}", HashTypeEnum.Sha512);
            
            if (user.Password != password)
            {
                return Unauthorized(new { message = "email or password is incorrect" });
            }

            string role = user.IsAdmin ? Role.Admin : Role.User;

            string token = _tokenService.GenerateToken(user.Id, role);
            if (string.IsNullOrEmpty(token))
                return Forbid();

            var loggedInUser = new LoggedInUserViewModel {
              FullName=user.FullName,
              Token=token
            };

            return Ok(loggedInUser);
        }
    }
}
