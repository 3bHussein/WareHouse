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

namespace firstWeb.Controllers
{
    [Authorize]
    public class outputsController : Controller
    {
        private Model1 db = new Model1();

        // GET: outputs
        public ActionResult Index(int page =1)
        {
            var outputs = db.outputs.Include(o => o.Department).Include(o => o.product);
            return View(outputs.ToList().OrderByDescending
                (a=>a.id).ToPagedList(page,8));
        }

        // GET: outputs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            output output = db.outputs.Find(id);
            if (output == null)
            {
                return HttpNotFound();
            }
            return View(output);
        }

        // GET: outputs/Create
        public ActionResult Create()
        {


            ViewBag.Depid = new SelectList(db.Departments, "Id", "Name");
            ViewBag.productid = new SelectList(db.products, "id", "ProductName");
                                                        
                                                       
            return View();
        }

        // POST: outputs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Data,Depid,productid,Qty,Description,iscancel")] output output)
        {
            if (ModelState.IsValid)
            {
                db.outputs.Add(output);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Depid = new SelectList(db.Departments, "Id", "Name", output.Depid);
            ViewBag.productid = new SelectList(db.products, "id", "ProductName", output.productid);
            return View(output);
        }

        // GET: outputs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            output output = db.outputs.Find(id);
            if (output == null)
            {
                return HttpNotFound();
            }
            ViewBag.Depid = new SelectList(db.Departments, "Id", "Name", output.Depid);
            ViewBag.productid = new SelectList(db.products, "id", "ProductName", output.productid);
            return View(output);
        }

        // POST: outputs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Data,Depid,productid,Qty,Description")] output output)
        {
            if (ModelState.IsValid)
            {
                db.Entry(output).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Depid = new SelectList(db.Departments, "Id", "Name", output.Depid);
            ViewBag.productid = new SelectList(db.products, "id", "ProductName", output.productid);
            return View(output);
        }

        // GET: outputs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            output output = db.outputs.Find(id);
            if (output == null)
            {
                return HttpNotFound();
            }
            return View(output);
        }

        // POST: outputs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            output output = db.outputs.Find(id);
            db.outputs.Remove(output);
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
        public PartialViewResult Search(string txt)
        {
            return PartialView("_outsearch", db.outputs.Where(a => a.product.ProductName
             .Contains(txt)));
        }


        public ActionResult fullsearch(string search )
        {
           
            return View("fullsearch",db.outputs.Where(a=>a.product.ProductName.Contains(search)));
        }
    }
}
