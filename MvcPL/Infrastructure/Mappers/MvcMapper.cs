using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interfaces.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMapper
    {
        public static BlogEntity ToBllBlog(this BlogViewModel blog)
        {
            return new BlogEntity
            {
                Id = blog.Id,
                Name = blog.Name,
                Rating = blog.Rating ?? 0,
                UserId = blog.UserId,
                DateStarted = blog.DateStarted,
            };
        }

        public static BlogViewModel ToMvcBlog(this BlogEntity blog, string userName)
        {
            return new BlogViewModel
            {
                Id = blog.Id,
                Name = blog.Name,
                Rating = blog.Rating,
                UserId = blog.UserId,
                DateStarted = blog.DateStarted,
                UserName = userName,
            };
        }
    }
}