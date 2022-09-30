using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DemoShoppingWebsite.Models
{
    public class ConnectStringService
    {
        public static dbShoppingCarAzureEntities CreateDBContext()
        {
            var db_ConnStr = ConfigurationManager.AppSettings["db_ConnStr"];
            var db_Pwd = EncryptService.DecryptBase64(ConfigurationManager.AppSettings["db_Pwd"]);

            var db_ConnStrFull = $@"{db_ConnStr}password={db_Pwd}""";

            return new dbShoppingCarAzureEntities(db_ConnStrFull);
        }
    }
}