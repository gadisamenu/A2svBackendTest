namespace Application.Features.Auth.Dtos
{
    public class LoginResponseDto
    {
        public string Id { get; set; }
        public string AccessToken { get; set; }
        public double ExpireInDays { get; set; }
    }
}