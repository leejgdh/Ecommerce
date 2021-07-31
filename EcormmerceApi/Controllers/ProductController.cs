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



        [HttpGet("{Id}")]
        public IActionResult Get([FromRoute] Guid Id)
        {
            var entity = _productService.Get(Id);
            return Ok(entity);
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var entities = _productService.GetAll();
            return Ok(entities);
        }

        /// <summary>
        /// 데이터 하나 추가
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] Product product)
        {
            var result = await _productService.InsertAsync(product);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Get), new { Id = result.Result.Id }, result.Result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid Id, Product product)
        {

            var result = await _productService.UpdateAsync(product);

            if (result.IsSuccess)
            {
                return AcceptedAtAction(nameof(Get), new { id = result.Result.Id }, result.Result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }


        [HttpDelete("Id")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid Id)
        {

            var result = await _productService.DeleteAsync(Id);

            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Message);
            }


        }
    }
}