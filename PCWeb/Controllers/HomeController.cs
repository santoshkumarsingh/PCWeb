using Newtonsoft.Json;
using PCWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace PCWeb.Controllers
{

    [HandleError]
    public class HomeController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {

            base.OnException(filterContext);

        }
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
            try
            {
                viewModel.Output = model.Input.ConvertBase64ToString();
            }
            catch
            {
                viewModel.Output = "Invalid Base64 string";

            }
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

            return View("StringToBinary", viewModel);
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
                ModelState.AddModelError("Error", "Invalid Binary Number");
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

            return Encoding.UTF8.GetString(byteList.ToArray());
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
            viewModel.Binary = model.Decimal.ConvetToBase(2);
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
        public ActionResult BitMask()
        {
            return View();
        }
        public ActionResult Regex()
        {
            return View();
        }

        public ActionResult UrlShortner()
        {
            InputViewModel model = new InputViewModel();
            return View(model);

        }
        [HttpPost]
        public ActionResult UrlShortner(InputViewModel model)
        {
            InputViewModel viewModel = new InputViewModel();
            viewModel.Input = model.Input;
            viewModel.Output = GoogleResponse(model.Input);

            ModelState.Clear(); // this is the key, you could also just clear ModelState for the id field

            return View(viewModel);

        }

        [ValidateInput(false)]
        public ActionResult JsonToXml()
        {
            InputViewModel model = new InputViewModel();
            return View(model);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult JsonToXml(InputViewModel model)
        {

            InputViewModel viewModel = new InputViewModel();
            // To convert JSON text contained in string json into an XML node
            XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(model.Input);
            viewModel.Input = model.Input;
            viewModel.Output = doc.InnerXml;

            ModelState.Clear(); // this is the key, you could also just clear ModelState for the id field

            return View(viewModel);


        }

        [ValidateInput(false)]
        public ActionResult XmlToJson()
        {
            InputViewModel model = new InputViewModel();
            ViewBag.Input = "<Root><FirstName>santosh</FirstName><LastName>Singh</LastName></Root>";
            return View(model);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult XmlToJson(InputViewModel model)
        {
            InputViewModel viewModel = new InputViewModel();
            // To convert an XML node contained in string xml into a JSON string   
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(model.Input);
            string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
            viewModel.Input = model.Input;
            viewModel.Output = jsonText;

            ModelState.Clear(); // this is the key, you could also just clear ModelState for the id field

            return View(viewModel);

        }

        [ValidateInput(false)]
        public ActionResult ImageToBase64()
        {
            InputViewModel model = new InputViewModel();
            return View(model);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ImageToBase64(HttpPostedFileBase file)
        {
            InputViewModel viewModel = new InputViewModel();
            if (file != null && file.ContentLength > 0)
            {
                var fileStream = file.InputStream;
                viewModel.Output = "<img alt=\"\" src=\"data:image/bmp;base64," + Convert.ToBase64String(ImageToByteArray(fileStream)) + "/>";

            }
            ModelState.Clear(); // this is the key, you could also just clear ModelState for the id field
            return View(viewModel);
         }
        
        public ActionResult HtmlToDiv()
        {
            InputViewModel model = new InputViewModel();
            return View(model);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HtmlToDiv(InputViewModel model)
        {
            InputViewModel viewModel = new InputViewModel();

            viewModel.Input = model.Input;
            viewModel.Output = HtmlTableToDiv(model.Input);
            ModelState.Clear(); // this is the key, you could also just clear ModelState for the id field
            return View(viewModel);
        }
        private static string HtmlTableToDiv(string input)
        {        

            var data = input.ToLower();
            var replace = @"<div class='td_1'>$1</div>";

            data = data.Replace("<table>", "<div class=table>");
            data = data.Replace("<tr>", "<div clas=tr>");
            data = data.Replace("</table>", "</div>");
            data = data.Replace("</tr>", "</div>");


            var result = System.Text.RegularExpressions.Regex.Replace(data, @"<td.*?>(.*?)</td>", replace, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return result;
        }
        private byte[] ImageToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
        private static string GoogleResponse(string p)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/urlshortener/v1/url");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"longUrl\":\"" + p + "\"}";
                Console.WriteLine(json);
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<UrlShortner>(responseText);
                return result.ID;
            }
        }
    }
}