using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Helper.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ecormmerce.Models
{

    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "{0}은 필수 입력값입니다.")]
        [Display(Name = "상품명", Description = "판매 상품명")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}은 필수 입력값입니다.")]
        [Display(Name = "상품관리번호(SKU)", Description = "구매자에게 노출되지 않으며 관리용도로 사용됩니다.")]
        public string Sku { get; set; }

        [Required]
        [Display(Name = "화폐단위", Description = "미입력시 KRW")]
        [EnumDataType(typeof(ECurrency))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ECurrency Currency { get; set; }

        [Display(Name = "상품 원가격", Description = "판매 상품의 원가입니다. 수익 계산에 사용됩니다.")]
        public double? BasePrice { get; set; }

        [Required(ErrorMessage = "{0}은 필수 입력값입니다.")]
        [Display(Name = "상품 등록가격", Description = "판매 상품의 가격입니다.")]
        public double Price{get; set;}

        [Display(Name = "할인율", Description = "기본 판매가를 기준으로 한 할인율입니다.")]
        public double? DiscountRate { get; set; }

        [Display(Name = "할인가", Description = "할인이 적용된 최종 판매가입니다.")]
        public double? DiscountPrice { get; set; }

        [Display(Name = "배송비", Description = "상품의 배송비입니다.")]
        public double? LogisticPrice { get; set; }

        [Required(ErrorMessage = "{0}은 필수 입력값입니다.")]
        [Display(Name = "상품 상세 설명", Description = "상품의 상세한 설명입니다.")]
        public string Description { get; set; }

        [Display(Name = "상품의 무게", Description = "판매 상품의 무게입니다.")]
        public double? Weight { get; set; }

        [Display(Name = "상품의 썸네일 이미지 URL입니다.")]
        public IEnumerable<string> ThumbnailImageUrls { get; set; }

        [Display(Name = "옵션 타입")]
        [EnumDataType(typeof(EVariationType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public EVariationType? VariationType { get; set; }

        [Display(Name = "옵션명")]
        public IEnumerable<string> VariationGroups { get; set; }

        public IEnumerable<string> ImageUrls { get; set; }

        [Display(Name = "옵션")]
        public IEnumerable<Variation> Variations { get; set; }

        [Required(ErrorMessage = "{0}은 필수 입력값입니다.")]
        [Display(Name = "카테고리 아이디", Description = "상품의 카테고리")]
        public Guid CategoryId { get; set; }
        
        [Required]
        public Guid ShopId { get; set; }

        public Shop Shop { get; set; }

        public class _Dimention
        {
            [Display(Name = "세로")]
            public double Length { get; set; }

            [Display(Name = "가로")]
            public double Width { get; set; }

            [Display(Name = "높이")]
            public double Height { get; set; }
        }
    }


}