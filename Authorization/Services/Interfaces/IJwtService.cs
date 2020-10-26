using System.Security.Claims;

namespace Authorization.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwtToken(Claim[] claims);
    }
}
