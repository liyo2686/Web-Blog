using com.ftc.Blog.DB;
using com.ftc.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace com.ftc.Blog.Controllers
{
    public class LoginController : Controller
    {
        private WebBlogDBEntities1 db = new WebBlogDBEntities1();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // 登入請求的 POST 方法
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 帳號密碼驗證
                if (IsUserValid(model.Account, model.Password))
                {
                    // 登入成功，設置身份驗證 Cookie 或其他識別標記
                    FormsAuthentication.SetAuthCookie(model.Account, false);

                    // 重新導向到首頁或其他需要登入後訪問的頁面
                    return RedirectToAction("Post", "Post");
                }
                else
                {
                    ModelState.AddModelError("password", "帳號或密碼不正確");
                    //ModelState.Clear();
                }
            }

            // 如果驗證失敗，返回登入頁面並顯示錯誤訊息
            return View(model);
        }

        private bool IsUserValid(string account, string password)
        {
            //驗證與資料庫的帳號密碼是否一致
            if (db.Users.Any(u => u.Account == account && u.Password == password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}