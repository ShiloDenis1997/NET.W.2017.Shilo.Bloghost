using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPL.Models.Entities;

namespace MvcPL.Models.Lists
{
    public class ArticlesListViewModel
    {
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<ArticleViewModel> Articles { get; set; }
        public int? UserId { get; set; }
        public int? BlogId { get; set; }
        public string Tag { get; set; }
        public string Text { get; set; }
    }
}