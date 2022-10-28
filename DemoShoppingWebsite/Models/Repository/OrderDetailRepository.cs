using DemoShoppingWebsite.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DemoShoppingWebsite.Models.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        protected dbShoppingCarAzureEntities db = ConnectStringService.CreateDBContext();
        public void Create(table_OrderDetail instance)
        {
            db.table_OrderDetail.Add(instance);
            this.SaveChanges();
        }

        public void AddNewOrder(string Receiver, string Email, string Address, string userId)
        {
            string guid = Guid.NewGuid().ToString(); //產生隨機訂單編號
            //加入訂單至 table_Order 資料表
            var order = new table_Order();
            order.OrderGuid = guid;
            order.UserId = userId;
            order.Receiver = Receiver;
            order.Email = Email;
            order.Address = Address;
            order.Date = DateTime.Now;
            db.table_Order.Add(order);

            //訂單加入後，需一併更新訂單明細內容
            var carList = db.table_OrderDetail.Where(m => m.IsApproved == "否" && m.UserId == userId).ToList();
            foreach (var item in carList)
            {
                item.OrderGuid = guid;
                item.IsApproved = "是";
            }
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var orderDetail = db.table_OrderDetail.Find(id);
            if (orderDetail != null)
            {
                db.table_OrderDetail.Remove(orderDetail);
                this.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("order");
            }
        }

        public IEnumerable<table_OrderDetail> GetByGuid(string orderGuid)
        {
            return db.table_OrderDetail.Where(m => m.OrderGuid == orderGuid).ToList();
        }

        public table_OrderDetail Get(string userid)
        {
            return db.table_OrderDetail.Where(x=>x.UserId==userid).FirstOrDefault();
        }

        public IQueryable<table_OrderDetail> GetOrderDetails(string userid)
        {
            var orderDetails = db.table_OrderDetail.
                Where(m => m.UserId == userid && m.IsApproved == "否");
            return orderDetails;
        }

        public void AddToCar(string userId,string ProductId)
        {
            //取得該使用者目前購物車內是否已有此商品，且尚未形成訂單的資料
            var currentCar = db.table_OrderDetail
                .Where(m => m.ProductId == ProductId && m.IsApproved == "否" && m.UserId == userId).FirstOrDefault();
            if (currentCar == null)
            {
                //如果篩選條件資料為null，代表要新增全新一筆訂單明細資料
                //將產品資料欄位一一對照至訂單明細的欄位
                var product = db.table_Product.Where(m => m.ProductId == ProductId).FirstOrDefault();
                var orderDetail = new table_OrderDetail();
                orderDetail.UserId = userId;
                orderDetail.ProductId = product.ProductId;
                orderDetail.Name = product.Name;
                orderDetail.Price = product.Price;
                orderDetail.Quantity = 1;
                orderDetail.IsApproved = "否";
                db.table_OrderDetail.Add(orderDetail);
            }
            else
            {
                //如果購物車已有此商品，僅需將數量加1
                currentCar.Quantity++;
            }

            //儲存資料庫並導至購物車檢視頁面
            db.SaveChanges();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Update(table_OrderDetail instance)
        {
            var item = db.table_OrderDetail.
                    Where(x => x.OrderGuid == instance.OrderGuid).FirstOrDefault();
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                item.Quantity = instance.Quantity;
                item.UserId = instance.UserId;
                item.Price = instance.Price;
                item.IsApproved = instance.IsApproved;
                item.Name = instance.Name;
                this.SaveChanges();

                //db.Entry(product).State = EntityState.Modified;
                //this.SaveChanges();
            }
        }

        public IQueryable<table_OrderDetail> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}