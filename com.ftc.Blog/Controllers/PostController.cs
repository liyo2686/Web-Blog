using com.ftc.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace com.ftc.Blog.Controllers
{
    public class PostController : Controller
    {
        private WebBlogDBEntities1 db = new WebBlogDBEntities1();
        // GET: Post
        public ActionResult Post()
        {
            int currentUserId = GetCurrentUserIdFromCookie();
            ViewBag.CurrentUserId = currentUserId; // 或者使用 ViewBag 或 ViewData 傳遞
            List<Article_View_Model> articles = db.Articles.Select(a => new Article_View_Model
            {
                PostID = a.PostID,
                UserID = a.UserID,
                Title = a.Title,
                Content = a.Content,
                CreatedTime = a.CreatedTime
            }).ToList();
            return View(articles);
        }

        private int GetCurrentUserIdFromCookie()
        {
            HttpCookie userCookie = Request.Cookies["userId"];
            if (userCookie != null)
            {
                int userId;
                if (int.TryParse(userCookie.Value, out userId))
                {
                    return userId;
                }
            }
            return 0; // 如果 Cookie 中不存在或解析失败，返回适当的默认值
        }
    }
}