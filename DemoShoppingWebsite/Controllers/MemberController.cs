using DemoShoppingWebsite.Models;
using DemoShoppingWebsite.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DemoShoppingWebsite.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private ProductRepository _productRepository;
        private OrderRepository _orderRepository;
        private OrderDetailRepository _orderDetailRepository;

        public MemberController()
        {
            _productRepository = new ProductRepository();
            _orderRepository = new OrderRepository();
            _orderDetailRepository = new OrderDetailRepository();
        }
        // GET: Member
        public ActionResult Index()
        {
            Session["Welcome"] = $"{User.Identity.Name},您好";
            var products = _productRepository.GetAll();
            return View("../Home/Index", "_LayoutMember", products);
        }

        public ActionResult Logout()
        {
            //using System.Web.Security;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult ShoppingCar()
        {
            string UserId = User.Identity.Name;

            var orderDetails = _orderDetailRepository.GetOrderDetails(UserId);
            return View(orderDetails);
        }

        public ActionResult AddCar(string ProductId)
        {
            //取得目前通過驗證的使用者名稱
            string userId = User.Identity.Name;
            _orderDetailRepository.AddToCar(userId, ProductId);
            return RedirectToAction("ShoppingCar");
        }

        public ActionResult DeleteCar(int Id)
        {
           _orderDetailRepository.Delete(Id);
            return RedirectToAction("ShoppingCar");
        }

        [HttpPost]
        public ActionResult ShoppingCar(string Receiver, string Email, string Address)
        {
            string userId = User.Identity.Name;
            _orderDetailRepository.AddNewOrder(Receiver, Email, Address, userId);
            return RedirectToAction("OrderList");
        }

        public ActionResult OrderList()
        {
            string userId = User.Identity.Name;
            var orders = _orderRepository.GetMemberOrders(userId);
            return View(orders);
        }

        public ActionResult OrderDetail(string OrderGuid)
        {
            var orderDetails = _orderDetailRepository.GetByGuid(OrderGuid);
            return View(orderDetails);
        }
    }
}