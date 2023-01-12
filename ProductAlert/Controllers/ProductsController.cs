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
using System.Web.Helpers;
using System.IO;

namespace ProductAlert.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.Agency).Include(p => p.Manufacturer);
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.AgencyId = new SelectList(db.Agencies, "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturer, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    System.Random randomInteger = new System.Random();
                    int genNumber = randomInteger.Next(1000000);

                    if (upload.ContentLength > 0 && upload.ContentType.ToUpper().Contains("JPEG") || upload.ContentType.ToUpper().Contains("PNG") || upload.ContentType.ToUpper().Contains("JPG"))
                    {

                        WebImage img = new WebImage(upload.InputStream);

                        string fileName = Path.Combine(Server.MapPath("~/Uploads/" + genNumber + upload.FileName));
                        img.Save(fileName);
                        // file.SaveAs(fileName);
                        product.Img = genNumber + upload.FileName;
                    }

                }

                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AgencyId = new SelectList(db.Agencies, "Id", "Name", product.AgencyId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturer, "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgencyId = new SelectList(db.Agencies, "Id", "Name", product.AgencyId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturer, "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProductName,AboutProduct,ManufacturerDate,ExpiringDate,BatchNumber,Description,AgencyId,ManufacturerId,Consequency,Img")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AgencyId = new SelectList(db.Agencies, "Id", "Name", product.AgencyId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturer, "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Send(int id)
        {
            Product product = await db.Products.FindAsync(id);
            string numberss = "";
            var datas = db.ProductNotifies.Include(x => x.User).Include(x => x.Product).ToList();
            numberss = numberss + "," + string.Join(",", datas.Select(x => x.User.PhoneNumber).Select(n => n.ToString()).ToArray());
            var agencyy = db.Agencies.Select(x => x.ContactPhone).ToList();
            numberss = numberss + "," + string.Join(",", agencyy.Select(n => n.ToString()).ToArray());


            var manuf = db.Manufacturer.Select(x => x.ContactPhone).ToList();
            numberss = numberss + "," + string.Join(",", manuf.Select(n => n.ToString()).ToArray());

            string message = "MOUAU PROJECT \r\n PRODUCT  ALERT MANAGEMENT SYSTEM \r\n " + product.ProductName.ToUpper() + "WILL EXPIRE ON "+ product.ExpiringDate + "THANKS";
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
            return RedirectToAction("Alert");
        }
        public ActionResult Alert()
        {
            return View();
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
