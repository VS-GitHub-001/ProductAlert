using ProductAlert.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProductAlert.Controllers
{
    public class NotifyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductNotifies
        public async Task<ActionResult> Index()
        {
            var ProductNotifies = db.ProductNotifies.Include(p => p.Product).Include(p => p.User).Include(p => p.BatchAlert);
            return View(await ProductNotifies.ToListAsync());
        }

        // GET: ProductNotifies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductNotify product = await db.ProductNotifies.Include(p => p.Product).Include(p => p.User).Include(p => p.BatchAlert).FirstOrDefaultAsync(x=>x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: ProductNotifies/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname");
            ViewBag.BatchAlertId = new SelectList(db.BatchAlert, "Id", "Batch");
            return View();
        }

        // POST: ProductNotifies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductNotify product)
        {
            if (ModelState.IsValid)
            {
                

                db.ProductNotifies.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", product.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", product.UserId);
            ViewBag.BatchAlertId = new SelectList(db.BatchAlert, "Id", "Batch", product.BatchAlertId);

            return View(product);
        }

        // GET: ProductNotifies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductNotify product = await db.ProductNotifies.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", product.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", product.UserId);
            ViewBag.BatchAlertId = new SelectList(db.BatchAlert, "Id", "Batch", product.BatchAlertId);
            return View(product);
        }

        // POST: ProductNotifies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductNotify product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", product.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", product.UserId);
            ViewBag.BatchAlertId = new SelectList(db.BatchAlert, "Id", "Batch", product.BatchAlertId);
            return View(product);
        }

        // GET: ProductNotifies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductNotify product = await db.ProductNotifies.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: ProductNotifies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductNotify product = await db.ProductNotifies.FindAsync(id);
            db.ProductNotifies.Remove(product);
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

