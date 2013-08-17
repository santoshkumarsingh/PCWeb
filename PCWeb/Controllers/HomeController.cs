using PCWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace PCWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
        //Base64ToString

        public ActionResult Base64ToString()
        {
            InputViewModel viewModel = new InputViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Base64ToString(InputViewModel model)
        {
            InputViewModel viewModel = new InputViewModel();
            viewModel.Input = model.Input;
            viewModel.Output = model.Input.ConvertBase64ToString();
            ModelState.Clear(); // this is the key, you could also just clear ModelState for the id field

            return View(viewModel);
        }

        public ActionResult Base64()
        {
            InputViewModel viewModel = new InputViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Base64(InputViewModel model)
        {
            InputViewModel viewModel = new InputViewModel();
            viewModel.Input = model.Input;
            viewModel.Output = model.Input.ConvertoBase64();
            ModelState.Clear(); // this is the key, you could also just clear ModelState for the id field

            return View(viewModel);
        }
        public ActionResult StringToBinary()
        {
            InputViewModel viewModel = new InputViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult StringToBinary(InputViewModel model)
        {
            InputViewModel viewModel = new InputViewModel();
            viewModel.Input = model.Input;
            viewModel.Output = model.Input.StringToBinary();
            ModelState.Clear(); // this is the key, you could also just clear ModelState for the id field

            return View(viewModel);
        }

        public ActionResult HashMd5()
        {
            InputViewModel viewModel = new InputViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult HashMd5(InputViewModel model)
        {
            InputViewModel viewModel = new InputViewModel();
            viewModel.Input = model.Input;
            viewModel.Output = model.Input.Md5();
            ModelState.Clear(); // this is the key, you could also just clear ModelState for the id field

            return View(viewModel);
        }

        public ActionResult HashFile()
        {
            InputViewModel viewModel = new InputViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult HashFile(HttpPostedFileBase file)
        {
            InputViewModel viewModel = new InputViewModel();
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {

                var fileStream = file.InputStream;
                MemoryStream ms = new MemoryStream();
                fileStream.CopyTo(ms);
                viewModel.Input = "";
                var sha1 = HashAlgorithm.Create("SHA1");
                var hash = sha1.ComputeHash(ms.ToArray());
                viewModel.Output = hash.ByteToHexString();

            }
            // redirect back to the index action to show the form once again
            ModelState.Clear(); // this is the key, you could also just clear ModelState for the id field

            return View(viewModel);
        }
        public ActionResult NumberConversion()
        {
            NumberViewModel model = new NumberViewModel();
            return View(model);

        }
        [HttpPost]
        public ActionResult NumberConversion(NumberViewModel model)
        {
            NumberViewModel viewModel = new NumberViewModel();
            viewModel.Decimal = model.Decimal;
            viewModel.Octol = model.Decimal.ToOct();
            viewModel.HexaDecimal = model.Decimal.ToHex();
            ModelState.Clear();
            return View(viewModel);

        }

        public ActionResult RSA()
        {
            RSAViewModel model = new RSAViewModel();
            return View(model);

        }
        [HttpPost]
        public ActionResult RSA(RSAViewModel model)
        {

            RSAViewModel viewModel = RSAUtility.rsa2(model);
   
            ModelState.Clear();
            return View(viewModel);

        }
    }
}