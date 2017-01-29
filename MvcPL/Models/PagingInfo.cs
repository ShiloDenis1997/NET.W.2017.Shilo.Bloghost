using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class PagingInfo
    {
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentItemsCount { get; set; }
    }
}