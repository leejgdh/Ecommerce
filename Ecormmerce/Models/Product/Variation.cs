using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Helper.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ecormmerce.Models
{

    /// <summary>
    /// Variation은 조합형, 독립형이 있음
    /// </summary>
    public class Variation
    {

        public Guid Id { get; set; }

        public string GroupName { get; set; }

        [Required( ErrorMessage = "{0}은 필수 입력값입니다.")]
        [Display(Name = "옵션명")]
        public string Name { get; set; }

        [Display(Name = "옵션 관리번호 (SKU)")]
        public string Sku { get; set; }

        [Required]
        [Display(Name = "화폐단위", Description = "미입력시 KRW")]
        [EnumDataType(typeof(ECurrency))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ECurrency Currency { get; set; }

        [Display(Name = "상품 원가격")]
        public double? BasePrice { get; set; }

        [Required( ErrorMessage = "{0}은 필수 입력값입니다.")]
        [Display(Name = "기본 판매가")]
        public double Price { get; set; }

        [Display(Name = "할인율")]
        public double? DiscountRate { get; set; }

        [Display(Name = "할인가", Description = "판매 할인가 절대값")]
        public double? DiscountPrice { get; set; }


        [Display(Name = "배송비")]
        public double? LogisticPrice { get; set; }

        [Display(Name = "옵션 설명")]
        public string Description { get; set; }

        [Display(Name = "무게")]
        public double? Weight { get; set; }

        [Display(Name = "이미지 URL")]
        public IEnumerable<string> ImageUrls { get; set; }

        public Product Product { get; set; }

    }
}