using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace emarket.Controllers
{
    public class CartsController : Controller
    {
        // GET: Carts
        public ActionResult Index()
        {
            return View();
        }
    }
}