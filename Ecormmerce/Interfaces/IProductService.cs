using System;
using System.Linq;
using System.Threading.Tasks;
using Helper.Models;

namespace Ecormmerce.Models
{

    public interface IProductService
    {
        Task<TaskResult<Product>> DeleteAsync(Guid Id);
        Product Get(Guid Id);
        IQueryable<Product> GetAll();
        Task<TaskResult<Product>> InsertAsync(Product product);
        Task<TaskResult<Product>> UpdateAsync(Product product);
    }
}