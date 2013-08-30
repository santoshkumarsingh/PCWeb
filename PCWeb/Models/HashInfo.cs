using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PCWeb.Models
{

    public class HashInfo
    {
        public static string InnerXml(XElement thiz)
        {
            return thiz.Nodes().Aggregate(string.Empty, (element, node) => element += node.ToString());
        }
      static  string FILEPATH = HttpContext.Current.Server.MapPath(@"~/App_Data/HashingInfo.xml");
        public static string GetHashInfo(string type)
        {
            var xmlDoc = (from x in XDocument.Load(FILEPATH).Descendants("HashAlgo")
                         where x.Attribute("Type").Value == type
                          select InnerXml(x.Element("Description"))).FirstOrDefault();
            return xmlDoc;
                   
        }
    }
}