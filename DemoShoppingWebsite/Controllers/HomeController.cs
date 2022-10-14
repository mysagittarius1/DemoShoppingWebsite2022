using DemoShoppingWebsite.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DemoShoppingWebsite.Controllers
{
    //[RoutePrefix("Main")]
    public class HomeController : Controller
    {
        //dbShoppingCarAzureEntities db = new dbShoppingCarAzureEntities();
        dbShoppingCarAzureEntities db = ConnectStringService.CreateDBContext();

        //[Route()]
        //[Route("Index")]
        public ActionResult Index(string text = "")
        {
            string user = User.Identity.Name;
            var products = db.table_Product
                    .Where(p=>p.Name.Contains(text))
                    .OrderByDescending(m => m.Id).ToList();
            if (user == string.Empty)
                return View(products);
            else
                return View("../Home/Index", "_LayoutMember", products);
        }

        [HttpPost]
        public ActionResult Query(string SearchContent)
        {
            return RedirectToAction("Index",new { text = SearchContent });
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(table_Member Member)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var member = db.table_Member.Where(m => m.UserId == Member.UserId).FirstOrDefault();
            if (member == null)
            {
                db.table_Member.Add(Member);
                db.SaveChanges();

                return RedirectToAction("Login");
            }
            ViewBag.Message = "帳號已被使用，請重新註冊";
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userid, string password, bool isRemember)
        {
            var member = db.table_Member.Where(m => m.UserId == userid && m.Password == password).FirstOrDefault();
            if (member == null)
            {
                ViewBag.Message = "帳號 or 密碼錯誤，請重新確認登入";
                return View();
            }

            FormsAuthentication.RedirectFromLoginPage(userid, isRemember);
            return RedirectToAction("Index", "Member");
        }

        public ActionResult GoogleMap()
        {
            return View();
        }
    }
}