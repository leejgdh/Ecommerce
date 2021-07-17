using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Ecormmerce.Models
{


    public class Category
    {
        [Key]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringConverter))]
        public Guid ParentId { get; set; }

        [NotMapped]
        public IList<Category> Child { get; set; }

        public Category Parent { get; set; }

    }
}