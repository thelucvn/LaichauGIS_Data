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
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Http;
using System.IO;
using System.Web.UI.WebControls;
using PagedList;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    [Authorize]
    public class PhotoAlbumsController : MyBaseController
    {
        public PhotoAlbumsController()
        {
            GetMyBaseController();
        }
        private LaichauDBContext db = new LaichauDBContext();

        // GET: Admin/PhotoAlbums
        public async Task<ActionResult> Index(int page = 1, int pageSize = 15)
        {
            var photoAlbums = db.Database.SqlQuery<PhotoAlbum>("exec sp_GetAllPhotoAlbum").ToList().OrderByDescending(x => x.photoAlbumID).ToPagedList(page, pageSize);
            foreach(PhotoAlbum photoAlbum in photoAlbums)
            {
                var photos = await db.Database.SqlQuery<PhotoInAlbum>("exec sp_AdminGetPhotoAlbum @PhotoAlbumID", new SqlParameter("@PhotoAlbumID",photoAlbum.photoAlbumID)).ToListAsync();
                photoAlbum.PhotoInAlbums = photos;
            }
            
            return View(photoAlbums);
        }

        // GET: Admin/PhotoAlbums/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoAlbum photoAlbum = await db.Database.SqlQuery<PhotoAlbum>("exec sp_GetPhotoAlbumByID @PhotoAlbumID", new SqlParameter("@PhotoAlbumID", id)).FirstOrDefaultAsync();
            var photos = await db.Database.SqlQuery<PhotoInAlbum>("exec sp_AdminGetPhotoAlbum @PhotoAlbumID", new SqlParameter("@PhotoAlbumID", photoAlbum.photoAlbumID)).ToListAsync();
            string baseAddress = ConfigurationManager.AppSettings["BaseAddress_2"];
            foreach (PhotoInAlbum photo in photos)
            {
                photo.photoUrl = baseAddress + photo.photoUrl;
                photo.photoUrl = photo.photoUrl.Replace("\\", @"\\");
            }
                
            photoAlbum.PhotoInAlbums = photos;
            if (photoAlbum == null)
            {
                return HttpNotFound();
            }
            return View(photoAlbum);
        }

        // GET: Admin/PhotoAlbums/Create
        
        public ActionResult Create()
        {
            ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName");
            return View();
        }

        // POST: Admin/PhotoAlbums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "albumTitle,wardID")] PhotoAlbum photoAlbum,HttpPostedFileBase[] ImageFiles)
        {
            if (ModelState.IsValid)
            {
                int wardID = photoAlbum.wardID;
                string albumTitle = photoAlbum.albumTitle;
                DateTime createDate = DateTime.Now;
                int userID = GetUserID();
                string[] photoUrl = await uploadImages(ImageFiles);
                if (photoUrl.Length == 0)
                {
                    ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName");
                    return View();
                }
                SqlParameter[] sqlParams = { new SqlParameter("@AlbumTitle",albumTitle),
                                            new SqlParameter("@WardID",wardID),
                                            new SqlParameter("@CreateDate",createDate),
                                            new SqlParameter("@UserID",userID)};
                var resID = await db.Database.SqlQuery<int>("exec sp_AdminCreateNewPhotoAlbum @AlbumTitle,@WardID,@CreateDate,@UserID", sqlParams).SingleOrDefaultAsync();
                if (resID == -1)
                {
                    ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName");
                    return View();
                }
                bool rollback = false;
                for(int i = 0; i < photoUrl.Length; i++)
                {
                    SqlParameter[] pars = {new SqlParameter("@PhotoAlbumID",resID),
                                            new SqlParameter("@PhotoUrl",photoUrl[i]),
                                            new SqlParameter("@PhotoTitle","")};
                   var res= await db.Database.SqlQuery<int>("exec sp_AddPhotoToAlbum @PhotoAlbumID,@PhotoUrl,@PhotoTitle", pars).SingleOrDefaultAsync();
                    if(res==-1)
                     rollback = true; 
                }
                if (rollback)
                {
                    await db.Database.SqlQuery<int>("Delete From PhotoAlbum where photoAlbumID=" + resID).SingleOrDefaultAsync();
                    ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName");
                    return View();
                }

                return RedirectToAction("Index");
            }
            ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName");
            return View();
        }
        async Task<string[]> uploadImages(HttpPostedFileBase[] ImageFiles)
        {
            string[] result = new string[ImageFiles.Length];
            string baseUlr = ConfigurationManager.AppSettings["BaseAddress_2"];
            string apiPath = "PhotoUpload";
            Uri clientUri = new Uri(baseUlr + @"/api/");
            for (int i=0;i<ImageFiles.Length;i++)
            {
                HttpPostedFileBase file = ImageFiles[i];

                using (var client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        byte[] imageData;
                        using (var reader = new BinaryReader(file.InputStream))
                        {
                            imageData = reader.ReadBytes(file.ContentLength);
                        }
                        HttpContent fileStreamContent = new StreamContent(new MemoryStream(imageData));
                        formData.Add(fileStreamContent, "file", file.FileName);
                        client.BaseAddress = clientUri;
                        var res = await client.PostAsync(apiPath, formData);

                        if (!res.IsSuccessStatusCode)
                        {
                            return new string[0];
                        }
                        result[i] = await res.Content.ReadAsStringAsync();
                    }
                }
            }
            return result;
        }
        // GET: Admin/PhotoAlbums/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoAlbum photoAlbum = await db.Database.SqlQuery<PhotoAlbum>("exec sp_GetPhotoAlbumByID @PhotoAlbumID", new SqlParameter("@PhotoAlbumID", id)).FirstOrDefaultAsync();
            var photos = await db.Database.SqlQuery<PhotoInAlbum>("exec sp_AdminGetPhotoAlbum @PhotoAlbumID", new SqlParameter("@PhotoAlbumID", photoAlbum.photoAlbumID)).ToListAsync();
            photoAlbum.PhotoInAlbums = photos;
            if (photoAlbum == null)
            {
                return HttpNotFound();
            }

            return View(photoAlbum);
        }

        // POST: Admin/PhotoAlbums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "photoAlbumID,albumTitle,wardID,locationID,createDate,userID,albumStatus,latitude,longitude,mLocationName,userName,wardName")] PhotoAlbum photoAlbum)
        {
            if (ModelState.IsValid)
            { SqlParameter[] sqlParams = { new SqlParameter("@AlbumTitle", photoAlbum.albumTitle), new SqlParameter("@AlbumStatus", photoAlbum.albumStatus),
                                            new SqlParameter("@PhotoAlbumID",photoAlbum.photoAlbumID)};
               var res= await db.Database.SqlQuery<int>("exec sp_UpdatePhotoAlbum @AlbumTitle,@AlbumStatus,@PhotoAlbumID", sqlParams).SingleOrDefaultAsync();
                if (res == 1)
                {
                    return RedirectToAction("Index");
                }             
            }
            return View(photoAlbum);
        }

        // GET: Admin/PhotoAlbums/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoAlbum photoAlbum = await db.Database.SqlQuery<PhotoAlbum>("exec sp_GetPhotoAlbumByID @PhotoAlbumID", new SqlParameter("@PhotoAlbumID", id)).FirstOrDefaultAsync();
            var photos = await db.Database.SqlQuery<PhotoInAlbum>("exec sp_AdminGetPhotoAlbum @PhotoAlbumID", new SqlParameter("@PhotoAlbumID", photoAlbum.photoAlbumID)).ToListAsync();
            photoAlbum.PhotoInAlbums = photos;
            if (photoAlbum == null)
            {
                return HttpNotFound();
            }
            return View(photoAlbum);
        }

        // POST: Admin/PhotoAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PhotoAlbum photoAlbum = await db.PhotoAlbums.FindAsync(id);
            db.PhotoAlbums.Remove(photoAlbum);
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
