using System;
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

        public async Task InsertAsync(Product product)
        {
             try
            {
                product.Id = product.Id == Guid.Empty ? Guid.NewGuid() : product.Id;

                _context.Products.Add(product);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} : {1}", nameof(ProductService.InsertAsync), e.Message);
            }
        }


        // public async Task InsertAsync(Product product)
        // {
        //     _context.Products.Add(product);

        //     await _context.SaveChangesAsync();
        // }
    }
}