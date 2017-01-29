using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.HtmlHelpers
{
    public static class LinksHelpers
    {
        public static MvcHtmlString GlyphLink
            (this HtmlHelper html, string glyphClass, string text, Func<string> pageUrl)
        {
            TagBuilder span = new TagBuilder("span");
            span.AddCssClass(glyphClass);

            TagBuilder button = new TagBuilder("button");
            button.MergeAttribute("type", "button");
            button.AddCssClass("btn btn-link btn-lg");
            button.AddCssClass("nodecor");
            button.InnerHtml = (span + text);

            TagBuilder aTag = new TagBuilder("a");
            aTag.MergeAttribute("href", pageUrl());
            aTag.InnerHtml = button.ToString();

            return MvcHtmlString.Create(aTag.ToString());
        }
    }
}