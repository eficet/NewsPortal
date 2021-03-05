using System.Collections.Generic;
using System.Threading.Tasks;
using API.Common;
using API.Controllers.Base;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NewsController : BaseApiController
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<MainResponse<IEnumerable<News>>>> GetNews()
        {
           var result =await _newsService.GetAllNews();
            return new MainResponse<IEnumerable<News>>
            {
                Path = HttpContext.Request.Path,
                Message = "Success",
                Data = result
            };
        }

        [AllowAnonymous]
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
        
        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<MainResponse<List<News>>>> Search(string search)
        {
            var result = await _newsService.Search(search);
            return new MainResponse<List<News>>
            {
                Path = HttpContext.Request.Path,
                Message = "Successfully found News",
                Data = result
            };
        }
        
    }
}