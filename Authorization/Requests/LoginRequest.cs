using System.ComponentModel.DataAnnotations;

namespace Authorization.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Nickname { get; set; }
    }
}
