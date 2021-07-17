using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EcormmerceWeb.Models;
using EcormmerceWeb.Models.ViewModels.Product;

namespace EcormmerceWeb.Controllers
{


    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "보유 상품 목록";

            IndexViewModel vm = new IndexViewModel();

            return View(vm);
        }
        
        public IActionResult Detail()
        {
            DetailViewModel vm = new DetailViewModel();

            return View(vm);
        }

    }


}