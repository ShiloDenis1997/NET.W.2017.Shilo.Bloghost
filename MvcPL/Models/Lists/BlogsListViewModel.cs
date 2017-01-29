using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPL.Models.Entities;

namespace MvcPL.Models.Lists
{
    public class BlogsListViewModel
    {
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<BlogViewModel> Blogs { get; set; }
    }
}