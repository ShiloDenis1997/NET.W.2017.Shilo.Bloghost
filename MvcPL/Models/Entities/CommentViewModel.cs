using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.Entities
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Comment can't be empty")]
        public string Content { get; set; }
        public int? Rating { get; set; }
        public DateTime DateAdded { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ArticleId { get; set; }
    }
}