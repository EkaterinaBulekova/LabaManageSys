using System;
using System.Text;
using System.Web.Mvc;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            var curPage = pagingInfo.CurrentPage;
                for (int i = (curPage != 1) ? curPage - 1 : 1; i <= ((curPage < pagingInfo.TotalPages) ? curPage + 1 : curPage); i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", pageUrl(i));
                    tag.InnerHtml = "Page " + i.ToString();
                    if (i == pagingInfo.CurrentPage)
                    {
                        tag.AddCssClass("selected");
                        tag.AddCssClass("btn-primary");
                    }

                    tag.AddCssClass("btn btn-default");
                    result.Append(tag.ToString());
                }

                return MvcHtmlString.Create(result.ToString());
            }
    }
}