﻿using com.ftc.Blog.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;


namespace com.ftc.Blog.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                int currentUserId = GetCurrentUserIdFromCookie();
                ViewBag.CurrentUserId = currentUserId; // 或者使用 ViewData 傳遞
                using (WebBlogDBEntities1 db = new WebBlogDBEntities1())
                {
                    List<Article_View_Model> articles = db.Articles.Select(a => new Article_View_Model
                    {
                        PostID = a.PostID,
                        UserID = a.UserID,
                        Title = a.Title,
                        Content = a.Content,
                        CreatedTime = a.CreatedTime
                    }).ToList();

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        articles = articles.Where(a => a.Title.Contains(searchString)).ToList();
                        currentFilter = searchString;
                    }
                    else
                    {
                        searchString = currentFilter;
                    }

                    ViewBag.CurrentSort = searchString;
                    ViewBag.CurrentFilter = currentFilter;
                    int pageSize = 10;
                    int pageNumber = (page ?? 1);
                    IPagedList<Article_View_Model> pagedArticles = articles.OrderByDescending(a => a.CreatedTime).ToPagedList(pageNumber, pageSize);
                    return View(pagedArticles);
                }
            }
            else
            {
                return RedirectToAction("Index","Login");
            }

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
            return 0;
        }
    }
}