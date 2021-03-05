using System.Collections.Generic;
using System.Threading.Tasks;
using API.Common;
using API.Constants;
using API.Controllers.Base;
using API.Entities;
using API.Interfaces;
using API.Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/admin/news")]
    public class NewsAdminController :BaseApiController
    {
        private readonly INewsService _newsService;

        public NewsAdminController(INewsService newsService)
        {
            _newsService = newsService;
        }
        [HttpGet]
        public async Task<ActionResult<MainResponse<List<News>>>> GetAllNews()
        {
            var result = await _newsService.GetAllNews();
            return new MainResponse<List<News>>
            {
                Path = HttpContext.Request.Path,
                Message = "Successfully found News",
                Data = result
            } ;
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MainResponse<News>>> GetNewsById(int id)
        {
            var result =await _newsService.GetNewsById(id);
            return new MainResponse<News>
            {
                Path = HttpContext.Request.Path,
                Message = "Successfully found News",
                Data = result
            };
        }
        
        [HttpPost]
        public async Task<ActionResult<MainResponse<News>>> CreateNews(CreateNewsDto newsDto)
        {
            var createdNews = await _newsService.CreateNews(newsDto);
            return new MainResponse<News>
            {
                Path = HttpContext.Request.Path,
                Message = "Successfully Created News",
                Data = createdNews
            };
        }
        [HttpPost("{id:int}")]
        public async Task<ActionResult<MainResponse<News>>> UpdateNews(int id, CreateNewsDto newsDto)
        {
            var updatedNews = await _newsService.UpdateNews(id, newsDto);
            return new MainResponse<News>
            {
                Path = HttpContext.Request.Path,
                Message = "Successfully Updated News",
                Data = updatedNews
            };
        }
        
        [HttpGet("search")]
        public async Task<ActionResult<MainResponse<List<News>>>> Search(string query)
        {
            var result = await _newsService.Search(query);
            return new MainResponse<List<News>>
            {
                Path = HttpContext.Request.Path,
                Message = "Successfully found News",
                Data = result
            };
        }
    }
}