using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace PCWeb.Models
{
    public class NumberViewModel
    {
        [Required()]
        public int Decimal { get; set; }
        public string Octol { get; set; }
        public string HexaDecimal { get; set; }
    }
}