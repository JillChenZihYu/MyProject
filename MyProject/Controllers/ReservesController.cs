using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class ReservesController : Controller
    {
        private ReserveRobotNewEntities1 db = new ReserveRobotNewEntities1();

        // GET: Reserves
        public ActionResult Index()
        {
            var reserves = db.Reserves.Include(r => r.Members).Include(r => r.Restaurants);
            return View(reserves.ToList());
        }

        // GET: Reserves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserves reserves = db.Reserves.Find(id);
            if (reserves == null)
            {
                return HttpNotFound();
            }
            return View(reserves);
        }

        // GET: Reserves/Create
        public ActionResult Create()
        {
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name");
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "Name");
            return View();
        }

        // POST: Reserves/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Reserves reserves)
        {
            if (ModelState.IsValid)
            {
                db.Reserves.Add(reserves);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", reserves.MemberID);
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "Name", reserves.RestaurantID);
            return View(reserves);
        }

        // GET: Reserves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserves reserves = db.Reserves.Find(id);
            if (reserves == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", reserves.MemberID);
            ViewBag.ReservationID = new SelectList(db.Restaurants, "RestaurantID", "Name", reserves.ReservationID);
            return View(reserves);
        }

        // POST: Reserves/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID,MemberID,RestaurantID,Date,Time,Adult,Child,Note,BookDate,BookTime")] Reserves reserves)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reserves).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", reserves.MemberID);
            ViewBag.ReservationID = new SelectList(db.Restaurants, "RestaurantID", "Name", reserves.ReservationID);
            return View(reserves);
        }

        // GET: Reserves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserves reserves = db.Reserves.Find(id);
            if (reserves == null)
            {
                return HttpNotFound();
            }
            return View(reserves);
        }

        // POST: Reserves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserves reserves = db.Reserves.Find(id);
            db.Reserves.Remove(reserves);
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
