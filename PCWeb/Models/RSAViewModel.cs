﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PCWeb.Models
{
    public class RSAViewModel
    {
        [Required()]
        public string PlainText { get; set; }
        public string CipherText { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string DecryptedText { get; set; }
    }
}