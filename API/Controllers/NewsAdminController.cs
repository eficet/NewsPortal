using System.Collections.Generic;
using System.Threading.Tasks;
using API.Constants;
using API.Controllers.Base;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class NewsAdminController :BaseApiController
    {
        private readonly INewsService _newsService;

        NewsAdminController(INewsService newsService)
        {
            _newsService = newsService;
        }
        [HttpGet]
        public async Task<ActionResult<List<News>>> GetAllNews()
        {
            return await _newsService.GetAllNews();
        }
    }
}