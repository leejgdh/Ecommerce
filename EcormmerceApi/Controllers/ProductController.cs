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
    [Route("[controller]/api")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IProductService _productService;

        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService
        )
        {
            _logger = logger;
            _productService = productService;
        }



        [HttpGet]
        public IActionResult Get()
        {
            return Ok(true);
        }

        /// <summary>
        /// 데이터 하나 추가
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] Product product)
        {
            await _productService.InsertAsync(product);

            return NoContent();
        }

        // [HttpPost]
        // public async Task<IActionResult> InsertAsync(Product request){

        //     await _productService.InsertAsync(request);

        //     return Ok(request);

        // }

    }
}