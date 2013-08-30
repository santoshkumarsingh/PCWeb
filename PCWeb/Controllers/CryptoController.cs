using PCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCWeb.Controllers
{
    public class CryptoController : Controller
    {
        public ActionResult CeaserCipher()
        {
            InputViewModel viewModel = new InputViewModel();
            return View(viewModel);
        }

    }
}
