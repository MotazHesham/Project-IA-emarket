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
        
        public ActionResult add (int id)
        {
            Cart cart = new Cart();
            DateTime now = DateTime.Now;
            cart.product_id = id;
            cart.added_at = now;
            db.Carts.Add(cart);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: carts/Delete/5
        public ActionResult Deleteid(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: carts/Delete/5
        
        public ActionResult Delete(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("index", "Products");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




    }
}