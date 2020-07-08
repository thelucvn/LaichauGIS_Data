using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.Framework;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    public class MessageTypesController : Controller
    {
        private LaichauDBContext db = new LaichauDBContext();

        // GET: Admin/MessageTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.MessageTypes.ToListAsync());
        }

        // GET: Admin/MessageTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageType messageType = await db.MessageTypes.FindAsync(id);
            if (messageType == null)
            {
                return HttpNotFound();
            }
            return View(messageType);
        }

        // GET: Admin/MessageTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MessageTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "messageTypeID,messageTypeName,typeDescription")] MessageType messageType)
        {
            if (ModelState.IsValid)
            {
                db.MessageTypes.Add(messageType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(messageType);
        }

        // GET: Admin/MessageTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageType messageType = await db.MessageTypes.FindAsync(id);
            if (messageType == null)
            {
                return HttpNotFound();
            }
            return View(messageType);
        }

        // POST: Admin/MessageTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "messageTypeID,messageTypeName,typeDescription")] MessageType messageType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messageType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(messageType);
        }

        // GET: Admin/MessageTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageType messageType = await db.MessageTypes.FindAsync(id);
            if (messageType == null)
            {
                return HttpNotFound();
            }
            return View(messageType);
        }

        // POST: Admin/MessageTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MessageType messageType = await db.MessageTypes.FindAsync(id);
            db.MessageTypes.Remove(messageType);
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
