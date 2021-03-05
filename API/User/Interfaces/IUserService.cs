using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.User.Interfaces
{
    public interface IUserService
    {
        public Task<AppUser> Login(LoginDto loginDto);
    }
}