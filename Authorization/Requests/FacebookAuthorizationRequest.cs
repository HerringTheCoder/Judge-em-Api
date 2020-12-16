using System.ComponentModel.DataAnnotations;

namespace Authorization.Requests
{
    public class FacebookAuthorizationRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
