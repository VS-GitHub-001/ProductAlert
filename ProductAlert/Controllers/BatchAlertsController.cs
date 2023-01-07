using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProductAlert.Models;
using System.IO;

namespace ProductAlert.Controllers
{
    public class BatchAlertsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BatchAlerts
        public async Task<ActionResult> Index()
        {
            return View(await db.BatchAlert.Include(x=>x.ProductNotifies).ToListAsync());
        }

        public async Task<ActionResult> SendAll(int id)
        {
            string numberss = "";
            var datas = db.ProductNotifies.Include(x => x.User).Include(x => x.Product).ToList();
            numberss = numberss + "," + string.Join(",", datas.Select(x => x.User.PhoneNumber).Select(n => n.ToString()).ToArray());
           

                var agencyy = db.Agencies.Select(x => x.ContactPhone).ToList();
                numberss = numberss + "," + string.Join(",", agencyy.Select(n => n.ToString()).ToArray());
          
                var manuf = db.Manufacturer.Select(x => x.ContactPhone).ToList();
                numberss = numberss + "," + string.Join(",", manuf.Select(n => n.ToString()).ToArray());
            
            string message = "PRODUCT ALERT MANAGEMENT SYSTEM \r\n EXPIRED PRODUCTS ALERT BY ONWUKA EBUKA IN COMPUTER SCIENCE.";
            message = message.Replace("0", "O");
            message = message.Replace("Services", "Servics");
            message = message.Replace("gmail", "g -mail");
            string response = "";
            //Peter Ahioma

            try
            {
                var getApi = "http://account.kudisms.net/api/?username=ponwuka123@gmail.com&password=sms@123&message=@@message@@&sender=@@sender@@&mobiles=@@recipient@@";
                string apiSending = getApi.Replace("@@sender@@", "EBUKA").Replace("@@recipient@@", HttpUtility.UrlEncode(numberss)).Replace("@@message@@", HttpUtility.UrlEncode(message));

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(apiSending);
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";

                //getting the respounce from the request
                HttpWebResponse httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                response = await streamReader.ReadToEndAsync();
                //response = "OK";
            }
            catch (Exception c)
            {
                response = c.ToString();
            }

            if (response.ToUpper().Contains("OK") || response.ToUpper().Contains("1701"))
            {
                //return response = "OK Sent";
            }
            TempData["r"] = "Alert Sent";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Send(int id)
        {
            string numberss = "";
            var datas = db.ProductNotifies.Include(x=>x.User).Include(x => x.Product).Where(x => x.BatchAlertId == id).ToList();
             numberss = numberss + ","+ string.Join(",", datas.Select(x => x.User.PhoneNumber).Select(n => n.ToString()).ToArray());
                foreach(var sd in datas)
            {
                var agencyy = db.Agencies.Select(x => x.ContactPhone).ToList();
                 numberss = numberss + "," + string.Join(",", agencyy.Select(n => n.ToString()).ToArray());
            }
            foreach (var sd in datas)
            {
                var manuf = db.Manufacturer.Select(x => x.ContactPhone).ToList();
                numberss = numberss + "," + string.Join(",", manuf.Select(n => n.ToString()).ToArray());
            }
            string message = "PRODUCT  ALERT MANAGEMENT SYSTEM \r\n EXPIRED PRODUCTS ALERT BY ONWUKA EBUKA IN COMPUTER SCIENCE.";
            message = message.Replace("0", "O");
            message = message.Replace("Services", "Servics");
            message = message.Replace("gmail", "g -mail");
            string response = "";
            //Peter Ahioma

            try
            {
                var getApi = "http://account.kudisms.net/api/?username=ponwuka123@gmail.com&password=sms@123&message=@@message@@&sender=@@sender@@&mobiles=@@recipient@@";
                string apiSending = getApi.Replace("@@sender@@", "EBUKA").Replace("@@recipient@@", HttpUtility.UrlEncode(numberss)).Replace("@@message@@", HttpUtility.UrlEncode(message));

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(apiSending);
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";

                //getting the respounce from the request
                HttpWebResponse httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                response = await streamReader.ReadToEndAsync();
                //response = "OK";
            }
            catch (Exception c)
            {
                response = c.ToString();
            }

            if (response.ToUpper().Contains("OK") || response.ToUpper().Contains("1701"))
            {
                //return response = "OK Sent";
            }
            TempData["r"] = "Alert Sent";
            return RedirectToAction("Index");
        }
        // GET: BatchAlerts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchAlert batchAlert = await db.BatchAlert.FindAsync(id);
            if (batchAlert == null)
            {
                return HttpNotFound();
            }
            return View(batchAlert);
        }

        // GET: BatchAlerts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BatchAlerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BatchAlert batchAlert)
        {
            if (ModelState.IsValid)
            {
                db.BatchAlert.Add(batchAlert);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(batchAlert);
        }

        // GET: BatchAlerts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchAlert batchAlert = await db.BatchAlert.FindAsync(id);
            if (batchAlert == null)
            {
                return HttpNotFound();
            }
            return View(batchAlert);
        }

        // POST: BatchAlerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BatchAlert batchAlert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batchAlert).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(batchAlert);
        }

        // GET: BatchAlerts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchAlert batchAlert = await db.BatchAlert.FindAsync(id);
            if (batchAlert == null)
            {
                return HttpNotFound();
            }
            return View(batchAlert);
        }

        // POST: BatchAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BatchAlert batchAlert = await db.BatchAlert.FindAsync(id);
            db.BatchAlert.Remove(batchAlert);
            await db.SaveChangesAsync();
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
