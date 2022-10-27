using DemoShoppingWebsite.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace DemoShoppingWebsite.Models.Repository
{
    public class ProductRepository : IProductRepository,IDisposable
    {
        protected dbShoppingCarAzureEntities db = new dbShoppingCarAzureEntities();
        public void Create(table_Product product)
        {
            db.table_Product.Add(product);
            this.SaveChanges();
        }

        public void Delete(table_Product product)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public table_Product Get(string productId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<table_Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(table_Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            this.SaveChanges();
        }
    }
}