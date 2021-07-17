using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ecormmerce.Models;
using EcormmerceWeb.Helpers;
using Helper.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Converters;

namespace EcormmerceWeb.Models.ViewModels.Product
{

    public class DetailViewModel : Ecormmerce.Models.Product
    {
        public DetailViewModel()
        {
            VariationTypes = ViewMethod.GetEnumSelectList<EVariationType>();
        }
        [Display(Name = "카테고리 명", Description = "상품의 카테고리 명")]
        public string CategoryName { get; set; }

        public IEnumerable<SelectListItem> VariationTypes { get; set; }

    }
}