using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoShoppingWebsite.Models.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity instance);
        void Update(TEntity instance);
        void Delete(int id);
        TEntity Get(string id);
        IQueryable<TEntity> GetAll();

        void SaveChanges();
    }
}
