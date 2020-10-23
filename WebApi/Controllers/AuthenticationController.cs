using System.Threading.Tasks;
using Authorization.Requests;
using Authorization.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
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

        public AuthenticationController(Authorization.Services.Interfaces.IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var token = await _authService.Register(request);
            if (token == null) 
                return NotFound();

            return Ok(token);
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status307TemporaryRedirect)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("login/google")]
        public IActionResult GoogleLogin()
        {

            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("SocialLogin")
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status307TemporaryRedirect)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("login/facebook")]
        public IActionResult FacebookLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("SocialLogin")
            };

            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("login/social")]
        public async Task<IActionResult> SocialLogin()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = await _authService.GetToken(result);

            return Ok(token);
        }
    }
}
