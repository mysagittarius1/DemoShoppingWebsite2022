using DemoShoppingWebsite.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DemoShoppingWebsite.Controllers
{
    [RoutePrefix("Main")]
    public class HomeController : Controller
    {
        dbShoppingCarAzureEntities db = new dbShoppingCarAzureEntities();

        //[Route()]
        //[Route("Index")]
        public ActionResult Index()
        {
            var products = db.table_Product.OrderByDescending(m => m.Id).ToList();
            return View(products);
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
        public ActionResult Login(string userid,string password)
        {
            var member = db.table_Member.Where(m => m.UserId == userid && m.Password == password).FirstOrDefault();
            if (member == null)
            {
                ViewBag.Message = "帳號 or 密碼錯誤，請重新確認登入";
                return View();
            }

            Session["Welcome"] = $"{member.Name} 您好";

            FormsAuthentication.RedirectFromLoginPage(userid, true);

            return RedirectToAction("Index", "Member");
        }
    }
}