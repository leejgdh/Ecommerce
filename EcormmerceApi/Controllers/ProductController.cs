using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EcormmerceApi.Controllers
{

    [ApiController]
    [Route("[controller]/api")]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {
            
        }



        [HttpGet]
        public IActionResult Get()
        {
            return Ok(true);
        }

    }
}