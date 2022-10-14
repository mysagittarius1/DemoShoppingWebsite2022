using DemoShoppingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoShoppingWebsite.Helpers
{
    public static class ScriptHelper
    {
        public static IHtmlString GoogleMapScript(this HtmlHelper helper,string url,string key)
        {
            var builder = new TagBuilder("script");
            key = EncryptService.DecryptBase64(key);
            var urlWithKey = url.Replace("*", key);
            builder.MergeAttribute("src", urlWithKey);
            builder.MergeAttribute("async", "");
            builder.MergeAttribute("defer", "");

            var result = builder.ToString();

            return new HtmlString(result);
        }
    }
}