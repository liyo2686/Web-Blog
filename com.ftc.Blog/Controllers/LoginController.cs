﻿using com.ftc.Blog.Models;
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
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // 登入請求的 POST 方法
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 帳號密碼驗證
                if (IsUserValid(model.Account, model.Password))
                {
                    using (WebBlogDBEntities1 db = new WebBlogDBEntities1())
                    {
                        var user = db.Users.FirstOrDefault(u => u.Account == model.Account);
                        int userId = GetUserIdFromDatabase(model.Account);
                        SetUserIdCookie(userId); // 設置使用者 ID 到 cookie 中

                        // 使用 FormsAuthentication.SetAuthCookie 設置身份驗證 cookie
                        FormsAuthentication.SetAuthCookie(model.Account, false);
                    }
                    // 重新導向到首頁或其他需要登入後訪問的頁面
                    return RedirectToAction("Index", "Post");
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
            using (WebBlogDBEntities1 db = new WebBlogDBEntities1())
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

        private void SetUserIdCookie(int userId)
        {
            HttpCookie userCookie = new HttpCookie("userId", userId.ToString());
            Response.Cookies.Add(userCookie);
        }

        private int GetUserIdFromDatabase(string userAccount)
        {
            using (WebBlogDBEntities1 db = new WebBlogDBEntities1())
            {
                // 使用 Entity Framework 進行資料庫查詢
                var user = db.Users.FirstOrDefault(u => u.Account == userAccount);
                if (user != null)
                {
                    return user.UserID;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}   