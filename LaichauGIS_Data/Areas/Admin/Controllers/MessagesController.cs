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
using System.Configuration;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    public class MessagesController : MyBaseController
    {
        private LaichauDBContext db = new LaichauDBContext();
        public MessagesController()
        {
            MyBaseController.GetMyBaseController();
        }

        // GET: Admin/Messages
        public async Task<ActionResult> Index()
        {
            var messages = db.Messages.Include(m => m.MessageType).Include(m => m.UserAccount);
            return View(await messages.ToListAsync());
        }

        // GET: Admin/Messages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            if (message.reiceiverID == null)
            {
                ViewBag.receiver = "Tất cả người dùng";
            }
            else
            {
                var ward = await db.Wards.FindAsync(message.reiceiverID);
                ViewBag.receiver = ward.wardName;
            }
            return View(message);
        }
        //GET: Admin/Messages/Send/5
        public async Task<ActionResult> Send(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message != null)
            {
                sendMessage(message);
                message.messageStatus = 4;
                db.Entry(message).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public void sendMessage(Message message)
        {
            //Send message to firebase
            var messageFirebase=db.Database.SqlQuery<MessageFirebase>("exec sp_getMessageFirebase").ToList();
            string messageTitle = message.messageTitle;
            string messageBody = message.messageContent;
            List<string> receiver = new List<string>();
            foreach(MessageFirebase mess in messageFirebase)
            {
                receiver.Add(mess.userToken);
            }
            string serviceApi = ConfigurationManager.AppSettings["BaseAddress_2"];
            string messageServiceUrl = serviceApi + "/api/MessageFirebase";

        }
        // GET: Admin/Messages/Create
        public ActionResult Create()
        {
            ViewBag.messageTypeID = new SelectList(db.MessageTypes, "messageTypeID", "messageTypeName");
            ViewBag.senderID = new SelectList(db.UserAccounts, "userID", "userName");
            List<Ward> wards = getSelectWardList();
            ViewBag.reiceiverID = new SelectList(wards, "wardID", "wardName");
            return View();
        }

        // POST: Admin/Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "reiceiverID,messageTypeID,messageTitle,messageContent")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.senderID = GetMyBaseController().GetUserID();
                message.messageStatus = 2;
                if (message.reiceiverID == 0)
                {
                    message.reiceiverID = null;
                    message.messageStatus = 3;
                }
                db.Messages.Add(message);
                await db.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }

            ViewBag.messageTypeID = new SelectList(db.MessageTypes, "messageTypeID", "messageTypeName", message.messageTypeID);
            ViewBag.senderID = new SelectList(db.UserAccounts, "userID", "userName", message.senderID);
            List<Ward> wards = getSelectWardList();
            ViewBag.reiceiverID = new SelectList(wards, "wardID", "wardName", message.reiceiverID);
            return View(message);
        }
        List<Ward> getSelectWardList()
        {
            Ward ward = new Ward();
            ward.wardID = 0;
            ward.wardName = "Broadcast(Send to All)";
            List<Ward> wards = new List<Ward>();
            wards.Add(ward);
            wards.AddRange(db.Wards);
            return wards;
        }
        // GET: Admin/Messages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.messageTypeID = new SelectList(db.MessageTypes, "messageTypeID", "messageTypeName", message.messageTypeID);
            ViewBag.senderID = new SelectList(db.UserAccounts, "userID", "userName", message.senderID);
            List<Ward> wards = getSelectWardList();
            ViewBag.reiceiverID = new SelectList(wards, "wardID", "wardName", message.reiceiverID);
            return View(message);
        }

        // POST: Admin/Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "messageID,senderID,reiceiverID,messageTypeID,messageTitle,messageContent,messageStatus")] Message message)
        {
            if (ModelState.IsValid)
            {
                if (message.reiceiverID == 0)
                {
                    message.reiceiverID = null;
                }
                db.Entry(message).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.messageTypeID = new SelectList(db.MessageTypes, "messageTypeID", "messageTypeName", message.messageTypeID);
            ViewBag.senderID = new SelectList(db.UserAccounts, "userID", "userName", message.senderID);
            List<Ward> wards = getSelectWardList();
            ViewBag.reiceiverID = new SelectList(wards, "wardID", "wardName", message.reiceiverID);
            return View(message);
        }

        // GET: Admin/Messages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            if (message.reiceiverID == null)
            {
                ViewBag.receiver = "Tất cả người dùng";
            }
            else
            {
                var ward = await db.Wards.FindAsync(message.reiceiverID);
                ViewBag.receiver = ward.wardName;
            }
            return View(message);
        }

        // POST: Admin/Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Message message = await db.Messages.FindAsync(id);
            db.Messages.Remove(message);
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
