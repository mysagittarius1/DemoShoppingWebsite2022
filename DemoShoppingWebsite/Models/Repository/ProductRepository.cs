using DemoShoppingWebsite.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace DemoShoppingWebsite.Models.Repository
{
    public class ProductRepository : IProductRepository
    {
        protected dbShoppingCarAzureEntities db = ConnectStringService.CreateDBContext();
        public void Create(table_Product product)
        {
            db.table_Product.Add(product);
            this.SaveChanges();
        }

        public void Delete(string productId)
        {
            var product = db.table_Product.Find(productId);
            if (product != null)
            {
                db.table_Product.Remove(product);
                this.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("product");
            }
        }

        public IEnumerable<table_Product> Query(string text = "")
        {
            var products = db.table_Product
                    .Where(p => p.Name.Contains(text))
                    .OrderByDescending(m => m.Id).ToList();
            return products;
        }

        public table_Product Get(string productId)
        {
            return db.table_Product.Find(productId);
        }

        public IEnumerable<table_Product> GetAll()
        {
            var products = db.table_Product.OrderByDescending(x => x.ProductId);
            return products.ToList();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Update(table_Product product)
        {
            var item = db.table_Product.
                Where(x => x.ProductId == product.ProductId).FirstOrDefault();
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                item.Name = product.Name;
                item.Price = product.Price;
                item.ProductId = product.ProductId;
                item.Image = product.Image;
                this.SaveChanges();

                //db.Entry(product).State = EntityState.Modified;
                //this.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        IQueryable<table_Product> IRepository<table_Product>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}