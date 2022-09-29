using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoShoppingWebsite.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult ShowContent(int id)
        {
            return Content($"會員編號是: {id}");
        }
    }
}