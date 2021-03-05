using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Services.DTOs;

namespace API.Interfaces
{
    public interface INewsService
    {
        public Task<List<News>> GetAllNews();
        public Task<News> GetNewsById(int id);
        public Task<News> CreateNews(CreateNewsDto newsDto);
        public Task<List<News>> Search(string titleSearch);
        public Task<List<News>> Search(AdminSearchDto titleSearch);

        public Task<News> UpdateNews(int id,CreateNewsDto newsDto);
    }
}