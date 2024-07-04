﻿using com.ftc.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.ftc.Blog.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // 登入請求的 POST 方法
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Account))
                {
                    ModelState.AddModelError("Account", "帳號不得為空");
                    return View(model);
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("Password", "密碼不得為空");
                    return View(model);
                }
                else
                {
                    // 重新導向到首頁或其他需要登入後訪問的頁面
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
    }
}