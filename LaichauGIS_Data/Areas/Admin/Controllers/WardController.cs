using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.Framework;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    public class WardController : Controller
    {
        private LaichauDBContext db = new LaichauDBContext();

        // GET: Admin/Ward
        public ActionResult Index()
        {
            var wards = db.Wards.Include(w => w.District);
            return View(wards.ToList());
        }

        // GET: Admin/Ward/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward ward = db.Wards.Find(id);
            if (ward == null)
            {
                return HttpNotFound();
            }
            return View(ward);
        }

        // GET: Admin/Ward/Create
        public ActionResult Create()
        {
            ViewBag.districtID = new SelectList(db.Districts, "districtID", "districtName");
            return View();
        }
        public ActionResult CreateForDistrict(int id)
        {
            Ward ward = new Ward(id);
            return View(ward);
        }
        [HttpPost]
        public ActionResult CreateForDistrict(Ward collection)
        {
            if (ModelState.IsValid)
            {
                db.Wards.Add(collection);
                db.SaveChanges();
                return RedirectToAction("DistrictDetails", new { dictID = collection.districtID });
            }
            return View(collection);
        }
        // POST: Admin/Ward/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "wardID,wardName,latitude,longitude,districtID,area,population,wardDescription")] Ward ward)
        {
            if (ModelState.IsValid)
            {
                db.Wards.Add(ward);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.districtID = new SelectList(db.Districts, "districtID", "districtName", ward.districtID);
            return View(ward);
        }

        // GET: Admin/Ward/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward ward = db.Wards.Find(id);
            if (ward == null)
            {
                return HttpNotFound();
            }
            ViewBag.districtID = new SelectList(db.Districts, "districtID", "districtName", ward.districtID);
            return View(ward);
        }

        // POST: Admin/Ward/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "wardID,wardName,latitude,longitude,districtID,area,population,wardDescription")] Ward ward)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ward).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DistrictDetails",new {dictID=ward.districtID});
            }
            ViewBag.districtID = new SelectList(db.Districts, "districtID", "districtName", ward.districtID);
            return View(ward);
        }

        // GET: Admin/Ward/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward ward = db.Wards.Find(id);
            if (ward == null)
            {
                return HttpNotFound();
            }
            return View(ward);
        }

        // POST: Admin/Ward/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ward ward = db.Wards.Find(id);
            db.Wards.Remove(ward);
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
        public ActionResult DistrictDetails(int dictID)
        {
            return RedirectToAction("Details", "District", new { id = dictID });
        }
    }
}
