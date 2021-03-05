using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using API.Common;
using API.Constants;
using API.Controllers.Base;
using API.Entities;
using API.Errors;
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
        [HttpPut("{id:int}")]
        public async Task<ActionResult<MainResponse<News>>> UpdateNews(int id, CreateNewsDto newsDto)
        {
            News updatedNews;
            try
            {
                updatedNews = await _newsService.UpdateNews(id, newsDto);
            }
            catch (Exception e)
            {
                return NotFound(new ApiException((int) HttpStatusCode.NotFound, e.Message));
            }
            return new MainResponse<News>
            {
                Path = HttpContext.Request.Path,
                Message = "Successfully Updated News",
                Data = updatedNews
            };
        }
        
        [HttpPost("search")]
        public async Task<ActionResult<MainResponse<List<News>>>> Search(AdminSearchDto searchDto)
        {
            var result = await _newsService.Search(searchDto);
            return new MainResponse<List<News>>
            {
                Path = HttpContext.Request.Path,
                Message = "Successfully found News",
                Data = result
            };
        }
    }
}