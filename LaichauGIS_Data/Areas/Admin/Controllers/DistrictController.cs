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
    [Authorize]
    public class DistrictController : MyBaseController
    {
        private LaichauDBContext db = new LaichauDBContext();
        public DistrictController()
        {
            MyBaseController.GetMyBaseController();
        }
        // GET: Admin/District
        public ActionResult Index()
        {
            return View(db.Districts.ToList());
        }

        // GET: Admin/District/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return HttpNotFound();
            }
            return View(district);
        }

        // GET: Admin/District/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CreateNewWard(int districtID)
        {
            return RedirectToAction("CreateForDistrict", "Ward",new { id = districtID } );
        }
        // POST: Admin/District/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "districtID,districtName,latitude,longitude,area,population,districtDescription")] District district)
        {
            if (ModelState.IsValid)
            {
                db.Districts.Add(district);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(district);
        }

        // GET: Admin/District/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return HttpNotFound();
            }
            return View(district);
        }
        public ActionResult EditWard(int wid)
        {
            return RedirectToAction("Edit", "Ward", new { id = wid });
        }
        public ActionResult WardDetails(int wid)
        {
            return RedirectToAction("Details", "Ward", new { id = wid });
        }
        public ActionResult DeleteWard(int wid)
        {
            return RedirectToAction("Delete", "Ward", new { id = wid });
        }
        // POST: Admin/District/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "districtID,districtName,latitude,longitude,area,population,districtDescription")] District district)
        {
            if (ModelState.IsValid)
            {
                db.Entry(district).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(district);
        }

        // GET: Admin/District/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return HttpNotFound();
            }
            return View(district);
        }

        // POST: Admin/District/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            District district = db.Districts.Find(id);
            db.Districts.Remove(district);
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
    }
}
