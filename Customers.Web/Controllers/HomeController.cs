using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace  Customers.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string INICIO = "Inicio";

        public ActionResult Index()
        {
            ViewBag.Section = INICIO;
            return View();
        }

    }
}