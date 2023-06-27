using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BethanysPieShop.TagHelpers
{
    //By Default, if the name is EmailTagHelper, then you can use <email> as your tag. Asp.Net will automatically remove the "TagHelper"
    //Ensure when you are using TagHelpers ensure that ASP.NET Core knows about your tag helper in the BethanysPieShop namespace. So add it to _ViewImports.cshtml, so it can be accessible everywhere
    public class EmailTagHelper : TagHelper
    {
        public string? Address { get; set; }
        public string? Content { get; set; }

        // Process() is a reserved method in ASP.NET, which they will automatically look for in TagHelper classes
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a"; //anchor tag
            output.Attributes.SetAttribute("href", "mailto:" + Address);
            output.Content.SetContent(Content);
        }
    }
}
