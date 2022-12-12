using ProductAlert.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProductAlert.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BatchAlerts
        public async Task<ActionResult> Continue()
        {
            ViewBag.agency = await db.Agencies.CountAsync();
            ViewBag.manuf = await db.Manufacturer.CountAsync();
            ViewBag.prod = await db.Products.CountAsync();
            ViewBag.uses = await db.Users.CountAsync();

            var product = await db.Products.ToListAsync();


            return View(product);
        }
        public ActionResult Index()
        {
            return View();
        }
   
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}