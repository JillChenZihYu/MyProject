using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
using PagedList;

namespace MyProject.Controllers
{
    public class AdministersController : Controller
    {
        private ReserveRobotNewNewEntities db = new ReserveRobotNewNewEntities();

        // GET: Administers
        public ActionResult Index(int page = 1)
        {

            var administers = db.Administers.Include(a => a.Administrators).Include(a => a.Members).ToList();

            int pagesize = 15; //一頁要有幾筆資料

            var pagedList = administers.ToPagedList(page, pagesize);


            return View(pagedList);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administers administers = db.Administers.Find(id);
            if (administers == null)
            {
                return HttpNotFound();
            }
            return View(administers);
        }



        // GET: Administers/Create
        public ActionResult Create()
        {
            ViewBag.AdministratorID = new SelectList(db.Administrators, "AdministratorID", "Name");
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name");
            return View();
        }

        // POST: Administers/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdministerID,AdministratorID,MemberID,Blocks,Reason")] Administers administers)
        {
            if (ModelState.IsValid)
            {
                db.Administers.Add(administers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdministratorID = new SelectList(db.Administrators, "AdministratorID", "Name", administers.AdministratorID);
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", administers.MemberID);
            return View(administers);
        }

        // GET: Administers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administers administers = db.Administers.Find(id);
            if (administers == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdministratorID = new SelectList(db.Administrators, "AdministratorID", "Name", administers.AdministratorID);
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", administers.MemberID);
            return View(administers);
        }

        // POST: Administers/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdministerID,AdministratorID,MemberID,Blocks,Reason")] Administers administers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdministratorID = new SelectList(db.Administrators, "AdministratorID", "Name", administers.AdministratorID);
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", administers.MemberID);
            return View(administers);
        }

        // GET: Administers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administers administers = db.Administers.Find(id);
            if (administers == null)
            {
                return HttpNotFound();
            }
            return View(administers);
        }

        // POST: Administers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administers administers = db.Administers.Find(id);
            db.Administers.Remove(administers);
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
