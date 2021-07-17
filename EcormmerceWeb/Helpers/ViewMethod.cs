using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using Helper.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcormmerceWeb.Helpers
{

    public static class ViewMethod
    {
        
        public static List<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : Enum
        {

            var enum_values = Enum.GetValues(typeof(TEnum));

            List<SelectListItem> results = new List<SelectListItem>();


            foreach (TEnum value in enum_values)
            {

                var enum_member = AttributeHelper.GetAttribute<EnumMemberAttribute>(value);
                var display = AttributeHelper.GetAttribute<DisplayAttribute>(value);

                results.Add(new SelectListItem()
                {
                    Value = enum_member != null ? enum_member.Value : value.ToString(),
                    Text = display != null ? display.Name : value.ToString()
                });
            }


            return results;
        }


        public static SelectListItem ConvertToSelectList<TEnum>(this TEnum data) where TEnum : Enum
        {

            var enum_values = Enum.GetValues(typeof(TEnum));

            SelectListItem results = new SelectListItem();

            var enum_member = AttributeHelper.GetAttribute<EnumMemberAttribute>(data);
            var display = AttributeHelper.GetAttribute<DisplayAttribute>(data);


            SelectListItem result = new SelectListItem()
            {
                Value = enum_member != null ? enum_member.Value : data.ToString(),
                Text = display != null ? display.Name : data.ToString()
            };


            return result;

        }

        public static IEnumerable<SelectListItem> ConvertToSelectList<TEnum>(TEnum[] datas) where TEnum : Enum
        {

            var enum_values = Enum.GetValues(typeof(TEnum));

            var result =  datas.Select(e => new SelectListItem()
            {
                Text = AttributeHelper.GetAttribute<DisplayAttribute>(e) == null ? e.ToString() : AttributeHelper.GetAttribute<DisplayAttribute>(e).Name,
                Value =  e.ToString()
            });

            return result;
        }
    }
}