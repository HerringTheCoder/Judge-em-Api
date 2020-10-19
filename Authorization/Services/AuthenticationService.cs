using Authorization.Requests;
using Authorization.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Authorization.Services
{
    class AuthenticationService : IAuthenticationService
    {
        public Task Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
