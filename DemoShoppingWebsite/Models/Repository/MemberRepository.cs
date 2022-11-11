using DemoShoppingWebsite.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoShoppingWebsite.Models.Repository
{
    public class MemberRepository
    {
        protected dbShoppingCarAzureEntities db = ConnectStringService.CreateDBContext();
        public void Create(table_Member instance)
        {
            db.table_Member.Add(instance);
            db.SaveChanges();
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
    }
}