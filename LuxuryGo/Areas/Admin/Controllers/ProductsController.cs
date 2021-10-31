using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LuxuryGo.Models;
using LuxuryGo.Models.Entities;
using Newtonsoft.Json;

namespace LuxuryGo.Areas.Admin.Controllers
{
    

    public class ProductsController : AdminController
    {
        string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }

       

        public partial class Ethenic
        {

        }

        public partial class Class
        {

        }

        public partial class StudentView
        {
            public string MaSV { get; set; }
            public string HoTen { get; set; }
            public string NgaySinh { get; set; }
            public string QueQuan { get; set; }
            public string DanToc { get; set; }
            public string Lop { get; set; }
        }

        public List<Student> fetchAllStudent()
        {
            string json = GET("http://localhost/studentman/api/sinhvien/getall.php");
            var result = JsonConvert.DeserializeObject<List<Student>>(json);
            return result;
        }
        // GET: Admin/Products
        public ActionResult Index()
        {
            //var products = db.Products.Include(p => p.User).Include(p => p.Manufact).Include(p => p.ProductCategory).Include(p => p.ProductType).OrderByDescending(p => p.CreateDate);
            //return View(products.ToList());
            return View(fetchAllStudent());
            //return View(result.ToList<Student>);
        }

        // GET: Admin/Products/Details/5
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

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.ManufactId = new SelectList(db.Manufacts, "id", "name");
            ViewBag.CatId = new SelectList(db.ProductCategories, "CatId", "Name");
            ViewBag.TypeId = new SelectList(db.ProductTypes, "id", "name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToAscii();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", product.UserId);
            
            ViewBag.ManufactId = new SelectList(db.Manufacts, "id", "name", product.ManufactId);
            ViewBag.CatId = new SelectList(db.ProductCategories, "CatId", "Name", product.CatId);
            ViewBag.TypeId = new SelectList(db.ProductTypes, "id", "name", product.TypeId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
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
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", product.UserId);
            ViewBag.ManufactId = new SelectList(db.Manufacts, "id", "name", product.ManufactId);
            ViewBag.CatId = new SelectList(db.ProductCategories, "CatId", "Name", product.CatId);
            ViewBag.TypeId = new SelectList(db.ProductTypes, "id", "name", product.TypeId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                product.Slug = product.Name.ToAscii();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", product.UserId);
            ViewBag.ManufactId = new SelectList(db.Manufacts, "id", "name", product.ManufactId);
            ViewBag.CatId = new SelectList(db.ProductCategories, "CatId", "Name", product.CatId);
            ViewBag.TypeId = new SelectList(db.ProductTypes, "id", "name", product.TypeId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
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

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
