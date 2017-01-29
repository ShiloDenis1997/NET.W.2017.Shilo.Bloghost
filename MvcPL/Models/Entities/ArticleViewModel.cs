using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.Entities
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Article must have name")]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Content can't be empty")]
        public string Content { get; set; }
        public int? Rating { get; set; }
        public DateTime DateAdded { get; set; }
        public int BlogId { get; set; }
        public int? UserId { get; set; }
        public string BlogName { get; set; }
        public string UserName { get; set; }
        
        public string[] Tags { get; set; }
    }
}