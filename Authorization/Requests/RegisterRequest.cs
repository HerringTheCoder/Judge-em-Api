using System.ComponentModel.DataAnnotations;

namespace Authorization.Requests
{
    public class RegisterRequest : LoginRequest
    {
        public string Email { get; set; }
    }
}
