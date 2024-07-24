using Application.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace Infrastructure.Services
{
    public class UserAccessor : IUserAccessor
    {
        public readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetEmail(){
            return _httpContextAccessor.HttpContext.User.FindFirstValue("Email");
        }

        public string GetUserId(){
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid);
        }
    }
}
