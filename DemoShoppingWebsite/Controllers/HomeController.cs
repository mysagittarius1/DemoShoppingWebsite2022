using DemoShoppingWebsite.Models;
using DemoShoppingWebsite.Models.Interface;
using DemoShoppingWebsite.Models.Repository;
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
    public class HomeController : Controller
    {
        private ProductRepository _productRepository;
        private MemberRepository _memberRepository;

        public HomeController()
        {
            _productRepository = new ProductRepository();
            _memberRepository = new MemberRepository();
        }

        public ActionResult Index(string text = "")
        {
            string user = User.Identity.Name;
            var products = _productRepository.Query(text);

            if (user == string.Empty)
                return View(products);
            else
                return View("../Home/Index", "_LayoutMember", products);
        }

        [HttpGet]
        public ActionResult Query(string SearchContent)
        {
            return RedirectToAction("Index", new { text = SearchContent });
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

            var member = _memberRepository.Get(Member.UserId);
            if (member == null)
            {
                _memberRepository.Create(member);
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
            var member = _memberRepository.GetUser(userid, password);
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