using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EcormmerceWeb.Helpers
{


    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting &lt;span&gt; elements with an <c>asp-description-for</c> attribute.
    /// </summary>
    [HtmlTargetElement("span", Attributes = AttributeName)]
    public class DescriptionTagHelper : TagHelper
    {
        private const string AttributeName = "asp-description-for";

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

                if (!string.IsNullOrEmpty(For.Metadata.Description))
                {
                    output.Content.SetContent(For.Metadata.Description);

                }
                else if (!string.IsNullOrEmpty(For.Metadata.Name))
                {
                    output.Content.SetContent(For.Metadata.Name);
                }

            }
        }
    }
}