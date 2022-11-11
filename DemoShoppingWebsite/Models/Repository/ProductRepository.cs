using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace DemoShoppingWebsite.Models.Repository
{
    public class ProductRepository
    {
        protected dbShoppingCarAzureEntities db = ConnectStringService.CreateDBContext();

        public IEnumerable<table_Product> Query(string text = "")
        {
            var products = db.table_Product
                    .Where(p => p.Name.Contains(text))
                    .OrderByDescending(m => m.Id).ToList();
            return products;
        }

        public IEnumerable<table_Product> GetAll()
        {
            var products = db.table_Product.OrderByDescending(x => x.ProductId);
            return products.ToList();
        }
    }
}