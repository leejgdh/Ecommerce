using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Helper.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ecormmerce.Models
{

    public class Shop
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        // [EnumDataType(typeof(ECurrency))]
        // [JsonConverter(typeof(StringEnumConverter))]
        // public ECountry Country { get; set; }

        public string BizNum { get; set; }


        //public IEnumerable<Product> Products { get; set; }




    }
}