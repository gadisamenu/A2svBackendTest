using Application.Features.Common;

namespace Application.Features.Auth.Dtos
{
    public class UserDetailDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
