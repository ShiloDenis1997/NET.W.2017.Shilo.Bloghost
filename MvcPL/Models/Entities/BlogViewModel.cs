using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models.Entities
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStarted { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int? Rating { get; set; }
    }
}