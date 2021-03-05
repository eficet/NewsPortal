using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace APITest
{
    public class NewsServiceTests
    {
        
        [Fact]
        public async Task GetAllNews_shouldReturnNews_WhenItExists()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "NewsPortalDatabase")
                .Options;
            var userTest = new News {Id = 1, Title = "heyy", Text = "text"};
            using (var context = new DataContext(options))
            {
                context.News.Add(userTest);
                await context.SaveChangesAsync();
            }
            using (var context = new DataContext(options))
            {
                var newsService = new NewsService(context);
               var news = await newsService.GetAllNews();

                Assert.Single(news);
                Assert.Equal(userTest.Title,news[0].Title);
            }
        }
        
    }
}
