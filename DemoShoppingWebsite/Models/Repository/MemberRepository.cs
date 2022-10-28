using DemoShoppingWebsite.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoShoppingWebsite.Models.Repository
{
    public class MemberRepository : IMemberRepository
    {
        protected dbShoppingCarAzureEntities db = ConnectStringService.CreateDBContext();
        public void Create(table_Member instance)
        {
            db.table_Member.Add(instance);
            this.SaveChanges();
        }

        public void Delete(string primaryId)
        {
            var member = db.table_Member.Find(primaryId);
            if (member != null)
            {
                db.table_Member.Remove(member);
                this.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("member");
            }
        }

        public table_Member Get(string userId)
        {
            return db.table_Member.Find(userId);
        }

        public table_Member GetUser(string userid,string password)
        {
            var user = db.table_Member.Where(m => m.UserId == userid && m.Password == password).FirstOrDefault();
            return user;
        }

        public IQueryable<table_Member> GetAll()
        {
            var members = db.table_Member.OrderByDescending(x => x.UserId);
            return members;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Update(table_Member instance)
        {
            var item = db.table_Member.
                    Where(x => x.UserId == instance.UserId).FirstOrDefault();
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                item.Name = instance.Name;
                item.UserId = instance.UserId;
                item.Email = instance.Email;
                item.Password = instance.Password;
                this.SaveChanges();

                //db.Entry(product).State = EntityState.Modified;
                //this.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}