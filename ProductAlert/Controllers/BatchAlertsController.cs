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

namespace ProductAlert.Controllers
{
    public class BatchAlertsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BatchAlerts
        public async Task<ActionResult> Index()
        {
            return View(await db.BatchAlert.ToListAsync());
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
        public async Task<ActionResult> Create([Bind(Include = "Id")] BatchAlert batchAlert)
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
        public async Task<ActionResult> Edit([Bind(Include = "Id")] BatchAlert batchAlert)
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
