using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Models.Entities
{
    [Bind(Include = "Content,ArticleId")]
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