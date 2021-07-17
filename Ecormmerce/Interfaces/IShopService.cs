using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ecormmerce.Models
{


    public interface IShopService
    {

        Shop Get(Guid Id);

        IQueryable<Shop> GetList();

        Task InsertAsync(Shop shop);

        Task UpdateAsync(Shop shop);
    }
}