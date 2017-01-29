using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPL.Models.Entities;

namespace MvcPL.Models.Lists
{
    public class CommentsListViewModel
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public int ArticleId { get; set; }
        public int CommentPackNumber { get; set; }
    }
}