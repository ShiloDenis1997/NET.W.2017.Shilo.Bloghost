using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcPL.Models;

namespace MvcPL.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PagingLinks(this HtmlHelper html,
            PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tag = CreatePageLink(1, pageUrl);
            if (1 == pagingInfo.CurrentPage)
                tag.AddSelection();
            result.Append(tag);
            for (int i = Math.Max(2, pagingInfo.CurrentPage - 1); i < pagingInfo.CurrentPage; i++)
            {
                tag = CreatePageLink(i, pageUrl);
                result.Append(tag);
            }
            if (1 != pagingInfo.CurrentPage)
            {
                tag = CreatePageLink(pagingInfo.CurrentPage, pageUrl);
                tag.AddSelection();
                result.Append(tag);
            }
            if (pagingInfo.CurrentItemsCount == pagingInfo.ItemsPerPage)
                result.Append(CreatePageLink(pagingInfo.CurrentPage + 1, pageUrl));
            return MvcHtmlString.Create(result.ToString());
        }

        private static TagBuilder CreatePageLink(int pageNumber, Func<int, string> pageUrl)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(pageNumber));
            tag.InnerHtml = pageNumber.ToString();
            tag.AddCssClass("btn btn-default");
            return tag;
        }

        private static TagBuilder AddSelection(this TagBuilder tag)
        {
            tag.AddCssClass("selected");
            tag.AddCssClass("btn-primary");
            return tag;
        }
    }
}