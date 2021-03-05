using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.User.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.User
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<AppUser> Login(LoginDto loginDto)
        {
            var user =await _dataContext.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (user == null)
            {
                throw new Exception("User doesnt exist");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            if (computedHash.Where((hash, index) => hash != user.PasswordHash[index]).Any())
            {
                throw new Exception("Password incorrect");
            }

            return user;
        }
        
        private async Task<bool> UserExists(string username)
        {
            return await _dataContext.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}