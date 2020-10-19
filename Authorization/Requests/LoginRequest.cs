using System.ComponentModel.DataAnnotations;

namespace Authorization.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
