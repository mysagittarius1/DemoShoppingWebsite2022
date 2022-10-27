using DemoShoppingWebsite.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DemoShoppingWebsite.Models
{
    public class EFGenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private DbContext _context { get; set; }
        public EFGenericRepository(DbContext context)
        {
            _context = context;
        }

        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public TEntity Read(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();

            if (_context.Configuration.ValidateOnSaveEnabled == false)
            {
                _context.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public void Update(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
        }
    }
}