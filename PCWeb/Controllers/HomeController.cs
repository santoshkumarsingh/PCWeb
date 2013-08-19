using PCWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        [MultipleButton(Name = "action", Argument = "Binary")]
        public ActionResult Binary(InputViewModel model)
        {
            InputViewModel viewModel = new InputViewModel();
            viewModel.Input = model.Input;
            viewModel.Output = new string(model.Input.StringToBinary().Reverse().ToArray());
            // this is the key, you could also just clear ModelState for the id field
            ModelState.Clear(); 

            return View("StringToBinary",viewModel);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "StringM")]
        public ActionResult StringM(InputViewModel model)
        {
            string pattern = @"[10]{8}";
            InputViewModel viewModel = new InputViewModel();
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(pattern);
            if (!r.IsMatch(model.Input))
            {
                ModelState.AddModelError("Error","Invalid Binary Number");
                return View("StringToBinary", viewModel);
            }

            
            //IEnumerable<string> groups = Enumerable.Range(0, model.Input.Length / 8)
            //                            .Select(i => model.Input.Substring(i * 8, 8));
            viewModel.Input = model.Input;
            viewModel.Output = BinaryToString(model.Input);
            ModelState.Clear(); // this is the key, you could also just clear ModelState for the id field

            return View("StringToBinary", viewModel);


        }
        private static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }

            return Encoding.ASCII.GetString(byteList.ToArray());
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


        public ActionResult SHA(string id)
        {
            InputViewModel model = new InputViewModel();
            var desc = HashInfo.GetHashInfo(id);
            model.Description = desc;
            return View(model);

        }
        [HttpPost]
        public ActionResult SHA(InputViewModel model, string id)
        {
            var desc = HashInfo.GetHashInfo(id);
            
            InputViewModel viewModel = new InputViewModel();
            viewModel.Input = model.Input;
            viewModel.Output = model.Input.SHAAlgorithm(id);
            viewModel.Description = desc;
            ModelState.Clear(); // this is the key, you could also just clear ModelState for the id field

            return View(viewModel);

        }

        public ActionResult Regex()
        {
            return View();
        }
    }
}