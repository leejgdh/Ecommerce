using System;
using System.Linq;
using System.Threading.Tasks;
using Helper.Helpers;
using Helper.Models;
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

        public Product Get(Guid Id)
        {
            var entity = _context.Products.Find(Id);

            return entity;
        }

        public IQueryable<Product> GetAll()
        {
            var entities = _context.Products.AsQueryable();

            return entities;
        }

        public async Task<TaskResult<Product>> InsertAsync(Product product)
        {
            TaskResult<Product> result = new TaskResult<Product>();
            
            try
            {
                product.Id = product.Id == Guid.Empty ? Guid.NewGuid() : product.Id;

                _context.Products.Add(product);

                await _context.SaveChangesAsync();


                result.IsSuccess = true;
                result.Result = product;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} : {1}", nameof(ProductService.InsertAsync), e.Message);
                result.IsSuccess = false;
                result.Result = product;
                result.Message = e.Message;
            }

            return result;
        }

        public async Task<TaskResult<Product>> UpdateAsync(Product product)
        {
            TaskResult<Product> result = new TaskResult<Product>();

            var entity = Get(product.Id);

            if (entity != null)
            {
                PropertyHelper.UpdatePropertyFromName(ref entity, product);
                await _context.SaveChangesAsync();

                result.IsSuccess = true;
                result.Result = entity;
            }else{

                result.IsSuccess = false;
                result.Message = "Content not found";
            }

            return result;
        }


        public async Task<TaskResult<Product>> DeleteAsync(Guid Id){
            TaskResult<Product> result = new TaskResult<Product>();
            
            var entity = Get(Id);

            if(entity != null){
                _context.Products.Remove(entity);

                await _context.SaveChangesAsync();


                result.IsSuccess = true;
                result.Result = entity;
            }else{
                result.IsSuccess = false;
                result.Result = entity;
                result.Message = "Content not found";
            }

            return result;
        }
    }
}