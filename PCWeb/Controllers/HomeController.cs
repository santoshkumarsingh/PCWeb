using PCWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");        
        }

    }
}