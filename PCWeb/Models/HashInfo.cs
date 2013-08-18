using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PCWeb.Models
{
    public class HashInfo
    {
      static  string FILEPATH = HttpContext.Current.Server.MapPath(@"~/App_Data/HashingInfo.xml");
        public static string GetHashInfo(string type)
        {
            var xmlDoc = (from x in XDocument.Load(FILEPATH).Descendants("HashAlgo")
                         where x.Attribute("Type").Value == type
                         select x.Element("Description").Value).FirstOrDefault();
            return xmlDoc;
                   
        }
    }
}