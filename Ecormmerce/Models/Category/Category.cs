using System;
using System.Collections.Generic;

namespace Ecormmerce.Models
{


    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ParentId { get; set; }

        public IEnumerable<Category> Child { get; set; }

        public Category Parent { get; set; }

    }
}