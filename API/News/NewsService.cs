using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using API.Services.DTOs;
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

        public async Task<News> GetNewsById(int id)
        {
            return await _dataContext.News.FindAsync(id);
        }

        public async Task<News> CreateNews(CreateNewsDto newsDto)
        {
            var news = new News
            {
                Text = newsDto.Text,
                Title = newsDto.Title,
                NewsType = newsDto.NewsType
            };
            _dataContext.News.Add(news);
            await _dataContext.SaveChangesAsync();
            return news ;
        }

        public async Task<List<News>> Search(string title)
        {
            return await _dataContext.News.Where(n => n.Title.Contains(title)).ToListAsync();
        }

        public async Task<News> UpdateNews(int id,CreateNewsDto newsDto)
        {
            var news = await _dataContext.News.FindAsync(id);
            if (news == null) throw new Exception("News not Found");
            news.Text = newsDto.Text;
            if(newsDto.Title != null) news.Title = newsDto.Title;
            if(newsDto.NewsType != 0) news.NewsType = newsDto.NewsType;
            if(newsDto.Text != null) news.Text = newsDto.Text;

            _dataContext.Update(news);
            await _dataContext.SaveChangesAsync();
            return news;
        }
    }
}