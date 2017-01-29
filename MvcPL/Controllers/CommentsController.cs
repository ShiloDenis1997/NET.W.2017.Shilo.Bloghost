using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Entities;

namespace MvcPL.Controllers
{
    public class CommentsController : Controller
    {
        private ICommentService commentService;
        private IUserService userService;
        private ILikeService likeService;

        private const int CommentPackSize = 10;

        public CommentsController
            (ICommentService commentService, IUserService userService,
            ILikeService likeService)
        {
            this.commentService = commentService;
            this.userService = userService;
            this.likeService = likeService;
        }

        // GET: Comments
        public ActionResult Comments(int articleId, int commentPackNumber = 1)
        {
            ViewBag.ArticleId = articleId;
            ViewBag.CommentPackNumber = commentPackNumber;
            IEnumerable<CommentViewModel> comments = commentService
                .GetCommentsByCreationDate(articleId, CommentPackSize * commentPackNumber)
                .Select(comment => comment.ToMvcComment
                    (userService.GetUserEntity(comment.UserId).Login));
            return PartialView(comments);
        }
        
        [Authorize(Roles = "User")]
        public ActionResult AddComment(CommentViewModel comment)
        {
            comment.DateAdded = DateTime.Now;
            var user = userService.GetUserByPredicate(
                u => u.Email.Equals(User.Identity.Name));
            comment.UserId = user.Id;
            commentService.CreateComment(comment.ToBllComment());
            if (Request.IsAjaxRequest())
            {
                var mvcComment = commentService.GetLastUserComment
                    (comment.ArticleId, comment.UserId)?.ToMvcComment(user.Login);
                return PartialView("_CommentPartial", mvcComment);
            }
            return RedirectToAction("Details", "Articles", new {id = comment.ArticleId});
        }

        [Authorize(Roles = "User")]
        public ActionResult Like(int commentId)
        {
            var user = userService.GetUserByPredicate(u => u.Email.Equals(User.Identity.Name));
            if (!likeService.LikeComment(commentId, user.Id))
                likeService.RemoveLikeComment(commentId, user.Id);
            var comment = commentService.GetCommentEntity(commentId);
            return RedirectToAction("Details", "Articles", new {id = comment.ArticleId});
        }
    }
}