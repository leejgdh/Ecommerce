using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Ecormmerce.Models
{

    public class ShopService : IShopService
    {

        private readonly ILogger<ShopService> _logger;
        private readonly EcormmerceContext _context;

        public ShopService(
            ILogger<ShopService> logger,
            EcormmerceContext context
        )
        {
            _logger = logger;
            _context = context;

        }

        public Shop Get(Guid Id)
        {
            return _context.Shops.Find(Id);
        }

        public IQueryable<Shop> GetList()
        {
            return _context.Shops;
        }

        public async Task InsertAsync(Shop shop)
        {
            shop.Id = Guid.NewGuid();
            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Shop shop)
        {
            var entity = Get(shop.Id);

            if(entity != null){

                entity.Name = shop.Name;

                await _context.SaveChangesAsync();
            }else{
                
            }
            
        }
    }
}