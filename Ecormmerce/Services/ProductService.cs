using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Ecormmerce.Models
{


    public class ProductService : IProductService
    {

        private readonly ILogger<ProductService> _logger;
        private EcormmerceContext _context;

        public ProductService(
            ILogger<ProductService> logger,
            EcormmerceContext context
        )
        {
            _logger = logger;
            _context = context;
        }


        // public async Task InsertAsync(Product product)
        // {
        //     _context.Products.Add(product);

        //     await _context.SaveChangesAsync();
        // }
    }
}