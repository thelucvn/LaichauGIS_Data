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
    public class DataTypeController : MyBaseController
    {
        private LaichauDBContext db = new LaichauDBContext();
        public DataTypeController()
        {
            MyBaseController.GetMyBaseController();
        }

        // GET: Admin/DataType
        public ActionResult Index()
        {
            return View(db.DataTypes.ToList());
        }

        // GET: Admin/DataType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataType dataType = db.DataTypes.Find(id);
            if (dataType == null)
            {
                return HttpNotFound();
            }
            return View(dataType);
        }

        // GET: Admin/DataType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DataType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dataTypeID,dataTypeName,mUnit")] DataType dataType)
        {
            if (ModelState.IsValid)
            {
                db.DataTypes.Add(dataType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dataType);
        }

        // GET: Admin/DataType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataType dataType = db.DataTypes.Find(id);
            if (dataType == null)
            {
                return HttpNotFound();
            }
            return View(dataType);
        }

        // POST: Admin/DataType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dataTypeID,dataTypeName,mUnit")] DataType dataType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dataType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dataType);
        }

        // GET: Admin/DataType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataType dataType = db.DataTypes.Find(id);
            if (dataType == null)
            {
                return HttpNotFound();
            }
            return View(dataType);
        }

        // POST: Admin/DataType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DataType dataType = db.DataTypes.Find(id);
            db.DataTypes.Remove(dataType);
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
