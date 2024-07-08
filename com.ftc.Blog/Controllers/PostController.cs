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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article_View_Model model)
        {
            if (ModelState.IsValid)
            {
                // 取得要編輯的文章
                Articles article = db.Articles.Find(model.PostID);

                // 更新文章內容
                article.Title = model.Title;
                article.Content = model.Content;

                // 保存到資料庫
                db.SaveChanges();

                return RedirectToAction("Post"); // 編輯成功，重新導向到文章列表
            }

            return View(model); // 如果模型狀態無效，返回編輯視圖
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