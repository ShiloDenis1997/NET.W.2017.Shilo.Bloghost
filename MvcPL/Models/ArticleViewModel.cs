using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int? Rating { get; set; }
        public DateTime DateAdded { get; set; }
        public int BlogId { get; set; }
        public int? AuthorId { get; set; }
        public string BlogName { get; set; }
        public string AuthorName { get; set; }
    }
}