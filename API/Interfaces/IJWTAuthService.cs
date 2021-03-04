using API.Entities;

namespace API.Interfaces
{
    public interface IJWTAuthService
    {
        public string CreateToken(AppUser user);
    }
}