using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context,ILogger logger)
        {
            if (await context.Users.AnyAsync()) return;
            logger.LogInformation("Started seeding Users to DB ...");
            using var hmac = new HMACSHA512();
            context.Users.Add(new AppUser
            {
                Id = 1,
                UserName = "admin",
                UserRole = UserRole.Admin,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin2021")),
                PasswordSalt = hmac.Key
            });
            await context.SaveChangesAsync();
            logger.LogInformation("Finished seeding Users to Db ..");
        }

        public static async Task SeedNews(DataContext context, ILogger logger)
        {
            if (await context.News.AnyAsync()) return;
            logger.LogInformation("Started seeding News to DB ...");
            List<News> newsList = new List<News>();
            for (int i = 0; i < 10; i++)
            {
                newsList.AddRange(new []{new News
                    {
                    Title = $" New news {i}",
                    NewsType = NewsType.Politics,
                    Text = $" hey this is my random text {i}",
                    CreatedBy = "seeder"
                },new News{
                    Title = $" Something Happened {i}",
                    NewsType = NewsType.Sport,
                    Text = $" hey this is my sport news number {i}",
                    CreatedBy = "seeder"
              
                    
                }});
            }
            context.News.AddRange(newsList);
            await context.SaveChangesAsync();
            logger.LogInformation("Finished seeding News to Db ..");

        }
    }
}