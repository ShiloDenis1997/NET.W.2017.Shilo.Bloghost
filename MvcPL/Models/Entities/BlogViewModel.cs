using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Models.Entities
{
    [Bind(Include = "Name")]
    public class BlogViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Blog needs a name")]
        [MaxLength(100, ErrorMessage = "Max length is 100")]
        public string Name { get; set; }

        [Display(Name = "Date added")]
        public DateTime DateStarted { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string UserEmail { get; set; }
        public int UserId { get; set; }
        public int? Rating { get; set; }
    }
}