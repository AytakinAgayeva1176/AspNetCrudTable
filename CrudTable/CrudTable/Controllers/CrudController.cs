using CrudTable.Db_context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace CrudTable.Controllers
{
    public class CrudController : Controller
    {
        List<Product> products;
        // GET: Crud
        public ActionResult Index()
        {
            using (Crud db = new Crud())
            {
                products = db.Products.ToList();
            }

            return View(products);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            using (Crud db = new Crud())
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                else
                {
                    // Response.Write(@"<script language='javascript'>alert('Message:  \n" + "Hi!" + " .');</script>");
                    // TempData["Message"] = "You are not authorized.";
                    // return Json(new { Message = "Empty" });
                    return RedirectToAction("AddProduct", "Crud");
                    //return new HttpStatusCodeResult(400);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProduct(int? id)
        {
            using (Crud db = new Crud())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                var product = db.Products.FirstOrDefault(x => x.Id == id);

                if (product == null)
                {
                    return new HttpStatusCodeResult(404);
                }
                db.Products.Remove(product);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditProduct(int? id)
        {
            using (Crud db = new Crud())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                var product = db.Products.FirstOrDefault(x => x.Id == id);

                if (product == null)
                {
                    return new HttpStatusCodeResult(404);
                }

                return View(product);
            }

        }


        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            using (Crud db = new Crud())
            {
                if (ModelState.IsValid)
                {
                    var product_ = db.Products.FirstOrDefault(x => x.Id == product.Id);

                    var user = product_;
                    db.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

        }

        public ActionResult GoBack()
        {
            return RedirectToAction("Index");
        }
    }
}