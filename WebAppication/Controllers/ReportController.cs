using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppication.Models;
namespace WebAppication.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private Model1 db = new Model1();

        // GET: proreport
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.Category);

            return View(products.ToList());
        }






             
    }
}
