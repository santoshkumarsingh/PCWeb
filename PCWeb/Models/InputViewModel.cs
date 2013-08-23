using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PCWeb.Models
{
    public class InputViewModel
    {
        [Required()]
        [Display(Name="Enter input string")]
        public string Input { get; set; }
        
        public string Output { get; set; }
        public string Description { get; set; }
    }
}