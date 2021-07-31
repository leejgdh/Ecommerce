using System.Linq;
using System.Threading.Tasks;
using Ecormmerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace EcormmerceUnitTest
{
    public class ProductUnitTest
    {

        private IProductService _productService;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("local.settings.json").Build();

            var cosmos = config.GetSection("EcormmerceConnection");

            var service_provider = new ServiceCollection()
            .AddLogging()
            .AddDbContext<EcormmerceContext>(e => {
                e.UseCosmos(cosmos["AccountEndPoint"], cosmos["AccountKey"], cosmos["DatabaseName"]);
            })
            .AddTransient<IProductService,ProductService>();

            var services = service_provider.BuildServiceProvider();

            _productService = services.GetService<IProductService>();

        }

        [Test]
        public async Task Get()
        {
            var entities = _productService.GetAll().ToList();

            // var delete_tasks = entities.Select(e => _productService.DeleteAsync(e.Id));

            // await Task.WhenAll(delete_tasks);
            
            Assert.Pass();
        }
    }
}