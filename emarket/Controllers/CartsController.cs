using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using emarket.Models;
using System.IO;

namespace emarket.Controllers
{
    public class CartsController : Controller
    {
        private storeEntities db = new storeEntities();
        // GET: Carts
        [ChildActionOnly]
        public PartialViewResult cartlist()
        {
            var carts = db.Carts.Include(c => c.Product);
            return PartialView("_CartList", carts.ToList());
        }

    }
}