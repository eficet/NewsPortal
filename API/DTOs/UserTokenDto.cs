using System;

namespace API.DTOs
{
    public class UserTokenDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime ExpiringTime { get; set; }


    }
}