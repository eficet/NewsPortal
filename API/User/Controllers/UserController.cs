using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Common;
using API.Controllers.Base;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Errors;
using API.Interfaces;
using API.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IJWTAuthService _jwtAuthService;
        private readonly IUserService _userService;

        public UserController(DataContext dataContext, IJWTAuthService jwtAuthService,IUserService userService)
        {
            _dataContext = dataContext;
            _jwtAuthService = jwtAuthService;
            _userService = userService;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<MainResponse<UserTokenDto>>> Login(LoginDto loginDto)
        {
            AppUser user;
            try
            {
                user = await _userService.Login(loginDto);
            }
            catch (Exception ex)
            {
                return Unauthorized(new ApiException((int)HttpStatusCode.Unauthorized,ex.Message,ex.StackTrace));
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
    }
    
}