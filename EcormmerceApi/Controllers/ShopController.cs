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
    public class ShopController : ControllerBase
    {
        private readonly ILogger<ShopController> _logger;

        private readonly IShopService _shopService;

        public ShopController(
            ILogger<ShopController> logger,
            IShopService shopService
        )
        {
            _logger = logger;
            _shopService = shopService;
        }



        [HttpGet]
        public IActionResult Get()
        {
            var datas = _shopService.GetList().ToList();
            return Ok(datas);
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync(Shop req){
            
            await _shopService.InsertAsync(req);

            return Ok(req);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(Shop req){

            await _shopService.UpdateAsync(req);

            return Ok(req);
        }
 

    }
}