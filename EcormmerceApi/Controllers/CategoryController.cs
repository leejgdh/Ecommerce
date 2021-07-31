using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecormmerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EcormmerceApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;

        private readonly ICategoryService _categoryService;

        public CategoryController(
            ILogger<CategoryController> logger,
            ICategoryService categoryService
        )
        {
            _logger = logger;
            _categoryService = categoryService;
        }



        // [HttpGet]
        // public IActionResult Get()
        // {
        //     var datas = _categoryService.GetList();
        //     return Ok(datas);
        // }

        // [HttpPost]
        // public async Task<IActionResult> InsertAsync(Category request){

        //     await _categoryService.InsertAsync(request);

        //     return Ok(request);

        // }

    }
}