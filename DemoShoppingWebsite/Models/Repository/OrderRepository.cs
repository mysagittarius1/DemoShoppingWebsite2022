using DemoShoppingWebsite.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoShoppingWebsite.Models.Repository
{
    public class OrderRepository : IOrderRepository
    {
        protected dbShoppingCarAzureEntities db = ConnectStringService.CreateDBContext();
        public void Create(table_Order instance)
        {
            db.table_Order.Add(instance);
            this.SaveChanges();
        }

        public void Delete(int primaryId)
        {
            var order = db.table_Order.Find(primaryId);
            if (order != null)
            {
                db.table_Order.Remove(order);
                this.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("order");
            }
        }

        public table_Order Get(string userId)
        {
            return db.table_Order.Find(userId);
        }

        public IQueryable<table_Order> GetAll()
        {
            var orders = db.table_Order.OrderByDescending(x => x.OrderGuid);
            return orders;
        }

        public IEnumerable<table_Order> GetMemberOrders(string userid)
        {
            var orders = db.table_Order.
                Where(m => m.UserId == userid).OrderByDescending(m => m.Date).ToList();
            return orders;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Update(table_Order instance)
        {
            var item = db.table_Order.
                    Where(x => x.OrderGuid == instance.OrderGuid).FirstOrDefault();
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                item.Address = instance.Address;
                item.UserId = instance.UserId;
                item.Email = instance.Email;
                item.Receiver = instance.Receiver;
                item.Date = DateTime.Now;
                this.SaveChanges();

                //db.Entry(product).State = EntityState.Modified;
                //this.SaveChanges();
            }
        }
    }
}