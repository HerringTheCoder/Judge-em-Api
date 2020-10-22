using System.IdentityModel.Tokens.Jwt;

namespace Authorization.Services.Interfaces
{
    public interface IJwtService
    {
        public JwtSecurityToken GenerateJwtToken(string email);
    }
}
