using com.ftc.Blog.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.ftc.Blog.Controllers
{
    public class ArticleController : Controller
    {
        private WebBlogDBEntities1 db = new WebBlogDBEntities1();

        // GET: Article
        public ActionResult addArticle()
        {
            return View();
        }

        // 登入請求的 POST 方法
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addArticle(Article_View_Model model)
        {   
            if (ModelState.IsValid)
            {
                string currentUserAccount = User.Identity.Name;
                var user = db.Users.FirstOrDefault(u => u.Account == currentUserAccount);
                var article = new Articles
                {
                    Title = model.Title,
                    Content = model.Content,
                    UserID = user.UserID,
                    CreatedTime = DateTime.Now
                };
                db.Articles.Add(article);
                db.SaveChanges();
                // 跳轉到文章列表
                return RedirectToAction("Post", "Post");
            }
            else
            {
                ModelState.AddModelError("", "發生錯誤，無法新增文章：");
            }

            // 如果模型驗證失敗，返回原始的新增文章的視圖，並顯示驗證錯誤訊息
            return View(model);
        }
        public ActionResult editArticle(int postId)
        {
            var model = new Article_View_Model
            {
                PostID = postId,
                // 其他属性的设置
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editArticle(Article_View_Model model)
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

                return RedirectToAction("Post", "Post");
            }

            return View(model); // 如果模型狀態無效，返回編輯視圖
        }
    }
}