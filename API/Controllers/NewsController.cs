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
        private readonly DataContext _dataContext;
        private readonly INewsService _newsService;

        public NewsController(DataContext dataContext,INewsService newsService)
        {
            _dataContext = dataContext;
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
    }
}