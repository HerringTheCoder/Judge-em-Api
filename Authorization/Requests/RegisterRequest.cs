using System.ComponentModel.DataAnnotations;

namespace Authorization.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string Nickname { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
