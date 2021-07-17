using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EcormmerceWeb.Helpers
{


    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting &lt;span&gt; elements with an <c>asp-description-for</c> attribute.
    /// Input 태그에 Placeholder로 DIsplay Attribute의 Description이나, Name을 넣어준다.
    /// </summary>
    [HtmlTargetElement("input", Attributes = AttributeName)]
    public class InputTagHelper : TagHelper
    {
        private const string AttributeName = "asp-for";

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        [HtmlAttributeName(AttributeName)]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (!output.IsContentModified)
            {
                //자동완성 꺼버림
                output.Attributes.SetAttribute("autocomplete","off");

                //id 중복되는거 막기 위해서 id는 다 지워버린다.
                
                TagHelperAttribute id_attribute ;

                bool has_id_attribute = output.Attributes.TryGetAttribute("id", out id_attribute);

                output.Attributes.Remove(id_attribute);

                if (!string.IsNullOrEmpty(For.Metadata.Description))
                {

                    output.Attributes.SetAttribute("placeholder", For.Metadata.Description);

                }
                else if (!string.IsNullOrEmpty(For.Metadata.Name))
                {

                    output.Attributes.SetAttribute("placeholder", For.Metadata.Name);
                }
            }
        }
    }


}