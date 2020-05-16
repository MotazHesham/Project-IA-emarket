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
        private storeEntities db = new storeEntities();

        public JsonResult category_select(int category_id)
        {
            var product = db.Products.Where(x => x.category_id == category_id).ToList();
            String data = "";
            foreach (var z in product)
            {
                data += "<div class='col-lg-2 col-md-3 col-sm-6 col-6'>";
                data += "<div class='card item-part' style='width: 14rem;'>";
                data += "<img src = '" + Url.Content(z.image) + "' class='card-img-top' style='height:150px'>";
                data += "<div class='card-body'>";
                data += "<h4 class='card-title' style ='margin -bottom: 25px' >" + z.name + "</h4>";
                data += "<hr />";
                data += "<span style = 'color: grey' > Price: " + z.price + "$</span>";
                data += "</div>";
                data += "<div class='item-part-overlay text-center' > ";
                data += "<a href = '/Products/Details/" + z.Id + "' style ='color: white' class='btn btn-primary' ><i class='fab fa-pagelines' ></i> View</a> <br /> <br />";
                data += "<a href='#' style='color: white' class='btn btn-success'>Add To Cart</a>";
                data += "</div>";
                data += "</div>";
                data += "</div>";
            }
            data += "<div class='clear'></div>";
            return Json(data);
        }

        public JsonResult select_all_products()
        {
            var product = db.Products.ToList();
            String data = "";
            foreach (var z in product)
            {
                data += "<div class='col-lg-2 col-md-3 col-sm-6 col-6'>";
                data += "<div class='card item-part' style='width: 14rem;'>";
                data += "<img src = '" + Url.Content(z.image) + "' class='card-img-top' style='height:150px'>";
                data += "<div class='card-body'>";
                data += "<h4 class='card-title' style ='margin -bottom: 25px' >" + z.name + "</h4>";
                data += "<hr />";
                data += "<span style = 'color: grey' > Price: " + z.price + "$</span>";
                data += "</div>";
                data += "<div class='item-part-overlay text-center' > ";
                data += "<a href = '/Products/Details/" + z.Id + "' style ='color: white' class='btn btn-primary' ><i class='fab fa-pagelines' ></i> View</a> <br /> <br />";
                data += "<a href='#' style='color: white' class='btn btn-success'>Add To Cart</a>";
                data += "</div>";
                data += "</div>";
                data += "</div>";
            }
            data += "<div class='clear'></div>";
            return Json(data);
        }



        // GET: Products
        public ActionResult Index()
        {
            ViewBag.categories_ajax = new SelectList(db.Categories, "Id", "name");
            ViewBag.cartlist= db.Carts.ToList();
            return View(db.Products.ToList());
        }
        // GET: Products1/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "Id", "name");
            return View();
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
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


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Carts, "product_id", "product_id", product.Id);
            ViewBag.category_id = new SelectList(db.Categories, "Id", "name", product.category_id);
            return View(product);
        }
        // POST: Products1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,price,description,category_id")] Product product, HttpPostedFileBase imgFile)
        {
            if (imgFile != null)
            {
                string path = "~/uploads/" + Path.GetFileName(imgFile.FileName);
                imgFile.SaveAs(Server.MapPath(path));
                product.image = path;
            }
            else
            {
                Product obj = db.Products.AsNoTracking().Where(x => x.Id == product.Id).ToList().FirstOrDefault();
                string myObjectstring = obj.image.ToString();
                product.image = myObjectstring;
            }

            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}