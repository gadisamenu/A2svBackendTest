using Domain;

namespace Application.Contracts.Services
{
    public interface IJwtService
    {
        public Tuple<string, double> GenerateToken(AppUser user, IList<string> roles);
    }
}