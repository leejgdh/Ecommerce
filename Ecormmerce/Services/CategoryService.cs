using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Ecormmerce.Models
{

    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly EcormmerceContext _context;
        
        public CategoryService(
            ILogger<CategoryService> logger,
            EcormmerceContext context
        )
        {
            _logger = logger;
            _context = context;
        }

        // public IQueryable<Category> GetList()
        // {
        //     return _context.Categories;
        // }

        // public async Task InsertAsync(Category category)
        // {
        //     _context.Categories.Add(category);
        //     await _context.SaveChangesAsync();
        // }
    }
}