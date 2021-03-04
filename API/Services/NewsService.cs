using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class NewsService : INewsService
    {
        private readonly DataContext _dataContext;
        public NewsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<News>> GetAllNews()
        {
            return await _dataContext.News.ToListAsync();
        }
    }
}