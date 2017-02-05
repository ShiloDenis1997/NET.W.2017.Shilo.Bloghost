using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Services;

namespace MvcPL.Controllers
{
    public class TagsController : Controller
    {
        private ITagService tagService;

        public TagsController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        // GET: Tags
        public JsonResult Find(string term)
        {
            var tags = tagService.GetTagsByPrefix(term)
                .Select(tag => new
                {
                    id = tag.Id,
                    label = tag.Name,
                    value = tag.Name,
                });
            return Json(tags, JsonRequestBehavior.AllowGet);
        }
    }
}