using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShoppingWebsite.Models.Interface
{
    public interface IProductRepository
    {
        void Create(table_Product product);
        void Update(table_Product product);
        void Delete(table_Product product);
        table_Product Get(string productId);
        IQueryable<table_Product> GetAll();
        void SaveChanges();
    }
}
