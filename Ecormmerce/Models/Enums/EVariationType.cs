using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Ecormmerce.Models
{
    public enum EVariationType{
        
        [Display(Name ="독립형", Description ="단일 상품")]
        [EnumMember(Value ="NORMAL")]
        NORMAL,

        [Display(Name ="조합형", Description ="옵션 조합형")]
        [EnumMember(Value ="COMBINATION")]
        COMBINATION,

    }
}