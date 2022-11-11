using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoShoppingWebsite.Models.Repository
{
    public class OrderRepository
    {
        protected dbShoppingCarAzureEntities db = ConnectStringService.CreateDBContext();

        public IEnumerable<table_Order> GetMemberOrders(string userid)
        {
            var orders = db.table_Order.
                Where(m => m.UserId == userid).OrderByDescending(m => m.Date).ToList();
            return orders;
        }
    }
}