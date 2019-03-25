using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITDepartment.Attributes;

namespace ITDepartment.Controllers
{
    public class HomeController : Controller
    {
        [Authorized]
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}