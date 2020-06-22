using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppication.Models;

namespace firstWeb.Controllers
{
    public class homeController : Controller
    {
        // GET: home
        //[Authorize]
        public ActionResult index()
        {
            return View();

        }
        public ActionResult Main()
        {
            return View();

        }

        [Authorize]
          public ActionResult BackupDatabase()
        {
            var dbPath = Server.MapPath("~/App_Data/DBBackup.bak");
            using (var db = new Model1())
            {
                var cmd = String.Format("BACKUP DATABASE {0} TO DISK='{1}' WITH FORMAT, MEDIANAME='DbBackups', MEDIADESCRIPTION='Media set for {0} database';"
                                            , "dbo.wareHouses", dbPath);
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd);
                return null;
            }

        }

        [Authorize]
        public ActionResult Cpanal()
        {
            return View();
        }

        }
    }