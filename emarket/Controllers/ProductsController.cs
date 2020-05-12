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
    public class ProductsController : Controller
    {
        private storeEntities2 db = new storeEntities2();

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        // GET: Products1/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "Id", "name");
            return View();
        }

        // POST: Products1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,price,description,category_id")] Product product, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                string path = "";
                if (imgFile != null)
                {
                      path = "~/uploads/" + Path.GetFileName(imgFile.FileName);
                    imgFile.SaveAs(Server.MapPath(path));
                   
                }
                product.image = path;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }
    }
}