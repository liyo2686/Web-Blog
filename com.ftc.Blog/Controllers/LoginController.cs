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
                    return RedirectToAction("Index", "Home");
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

        // 假設這裡是一個簡單的帳號密碼驗證方法（實際應該從資料庫中驗證）
        private bool IsUserValid(string username, string password)
        {
            //目前測試用
            if (username == "123" && password == "123")
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