using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
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
using Newtonsoft.Json.Linq;
using Storage.Tables;

namespace WebApi.Controllers
{
    [AllowAnonymous, Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Authorization.Services.Interfaces.IAuthenticationService _authenticationService;

        public AuthenticationController(Authorization.Services.Interfaces.IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var newUser = _authenticationService.Register(request);

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return RedirectToPage("/");
        }

        [Route("google-login")]
        public IActionResult GoogleLogin()
        {

            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("AuthenticationResponse")
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("facebook-login")]
        public IActionResult FacebookLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("AuthenticationResponse")
            };

            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        [Route("authentication-response")]
        public async Task<IActionResult> AuthenticationResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = _authenticationService.AuthenticationResponse(result);

            return Ok(token);
        }
    }
}
