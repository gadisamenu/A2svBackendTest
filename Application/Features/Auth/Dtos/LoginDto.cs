using Application.Features.Common;

namespace Application.Features.Auth.Dtos
{
    public class LoginDto
    { 
        public string Email { get; set; }
        public string Password { get; set; }
    }
}