using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Common;
using API.Controllers.Base;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Enums;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AuthenticateController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IJWTAuthService _jwtAuthService;
        public AuthenticateController(DataContext dataContext, IJWTAuthService jwtAuthService)
        {
            _dataContext = dataContext;
            _jwtAuthService = jwtAuthService;
        }
        
        [HttpPost]
        public async Task<ActionResult<MainResponse<AppUser>>> Register(string username, string password)
        {
            if (await UserExists(username))
            {
                return BadRequest("User exists");
            }
            using var hmac = new HMACSHA512();
            var user = new AppUser()
            {
                UserName = username,
                UserRole = UserRole.Admin,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return new MainResponse<AppUser>()
            {
                Path = HttpContext.Request.Path,
                Message = "Successfully Cr",
                Data = user
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<MainResponse<UserTokenDto>>> Login(LoginDto loginDto)
        {
            var user =await _dataContext.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (user == null)
            {
                return Unauthorized("User doesnt exist");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Password incorrect");
            }
            var token = _jwtAuthService.CreateToken(user);
            return new MainResponse<UserTokenDto>
            {
                Path = HttpContext.Request.Path,
                Message = "Successfully Logged in",
                Data = new UserTokenDto
                {
                    Username = loginDto.Username,
                    Token = token
                }
            };
        }
        private async Task<bool> UserExists(string username)
        {
           return await _dataContext.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
    
}