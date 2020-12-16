using System.Threading.Tasks;
using Authorization.Requests;
using Authorization.Services.Interfaces;
using Core.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [AllowAnonymous, Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("login/facebook")]
        public async Task<IActionResult> FacebookLogin([FromBody] FacebookAuthorizationRequest request)
        {
            var user = await _authService.AuthorizeFacebookUser(request.Token);
            if(user == null)
            {
                return Unauthorized();
            }

            string token = _authService.GetUserToken(user);
            return Ok(new TokenDto(token));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("login/guest")]
        public IActionResult GuestLogin()
        {
            string token =_authService.GetGuestToken();
            return Ok(new TokenDto(token));
        }
    }
}
