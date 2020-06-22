using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppication.Models;
//
using PagedList;
using PagedList.Mvc;
using WebAppication.reports;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace firstWeb.Controllers
{
   //[Authorize]
    public class productsController : Controller
    {
        private Model1 db = new Model1();

        // GET: products
        public ActionResult Index(int page=1)
        {
            var products = db.products.Include(p => p.Category);
            return View(products.ToList().OrderByDescending(a=>a.id).ToPagedList(page,8));
        }

        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.CAteId = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ProductName,Type,Total,currentNO,Description,CAteId")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CAteId = new SelectList(db.Categories, "ID", "Name", product.CAteId);
            return View(product);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CAteId = new SelectList(db.Categories, "ID", "Name", product.CAteId);
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ProductName,Type,Total,currentNO,Description,CAteId")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CAteId = new SelectList(db.Categories, "ID", "Name", product.CAteId);
            return View(product);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
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
            product product = db.products.Find(id);
            db.products.Remove(product);
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
        public ActionResult Export()
        {
            #region list
                        var result = (from p in db.products
                                      join c in db.Categories
                                      on p.CAteId equals c.ID
                                
                                      select new
                                      {
                                        
                                          p.ProductName,
                                          p.Total,
                                          p.currentNO,
                                          p.Description,
                                          c.Name,                                          
                                      }).ToList();
            #endregion


            //var result2 = ().tolist();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("../reports/CrystalReport1.rpt")));
            rd.SetDataSource(result);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream steam = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            steam.Seek(0, SeekOrigin.Begin);

            return File(steam, "application/pdf", "ListOfStocks.pdf");
        }
        //public ActionResult ExportExcel()
        //{
        //    #region list
        //    var result = (from p in db.products
        //                  join c in db.Categories
        //                  on p.CAteId equals c.ID
        //                  select new
        //                  {
        //                      p.ProductName,
        //                      p.Total,
        //                      p.Tacked,
        //                      c.Name
        //                  }).ToList();
        //    #endregion
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/reports/CrystalReport1.rpt/")));
        //    rd.SetDataSource(result);
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Stream steam = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
        //    steam.Seek(0, SeekOrigin.Begin);

        //    return File(steam, "application/xls", "ListOfStocks.xls");
        //}

        public ActionResult fullsearch(string search)
        {

            return View("fullsearch", db.products.Where(a => a.ProductName.Contains(search)));
        }

    }
}
