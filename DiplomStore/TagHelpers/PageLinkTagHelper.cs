using DiplomStore.ViewsModels.Tovar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DiplomStore.TagHelpers
{
    public class PageLinkTagHelper:TagHelper
    {
        IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory _urlHelperFactory)
        {
            urlHelperFactory = _urlHelperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext viewContext { get; set; }
        public PageViewModel? PageModel { get; set; }        
        public int? PageCategoriesId { get; set; }
        public string PageAction { get; set; } = "";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(viewContext);
            output.TagName = "div";
            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");
            TagBuilder currentItem = CreateTag(PageCategoriesId, urlHelper, PageModel.PageNuber);
            if (PageModel.HasPreviousPage)
            {
                TagBuilder prevItem = CreateTag(PageCategoriesId, urlHelper, PageModel.PageNuber - 1);
                tag.InnerHtml.AppendHtml(prevItem);
            }
            tag.InnerHtml.AppendHtml(currentItem);
            if (PageModel.HasNextPage)
            {
                TagBuilder nextItem = CreateTag(PageCategoriesId, urlHelper, PageModel.PageNuber + 1);
                tag.InnerHtml.AppendHtml(nextItem);
            }
            output.Content.AppendHtml(tag);
        }
        TagBuilder CreateTag(int? CategoriyId, IUrlHelper urlHelper, int pageNumber = 1)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            if(pageNumber == PageModel.PageNuber)
            {
                item.AddCssClass("active");
            }
            else
            {
                //Прислаиваем аттрибуту href значение ссылки на следующую страницу, а также присваиваем дополниетльные сигменты со значением колличества страниц и id категории
                link.Attributes["href"] = urlHelper.Action(PageAction, new {page = pageNumber, CategoriesId = CategoriyId});                                
            }
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
