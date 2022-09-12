using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    [MemberLoginCheck]

    public class HomeMemberController : Controller
    {

        ReservationEntities db = new ReservationEntities();

        // GET: HomeMember

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult RestaurantList(string SearchValue)
        {
            var restaurants = db.Restaurants.ToList();
            if (SearchValue !=  null) 
            { 
            restaurants= db.Restaurants.Where(x=>x.Name.Contains(SearchValue)).ToList();
            }
      
           

            return View(restaurants);
        }


        public ActionResult ReservationList()
        {

            Members MemberUser = (Members)Session["MemberUser"];
            //var reserves = db.Reserves.ToList();
            var MemResList = db.Reserves.Where(a => a.MemberID == MemberUser.MemberID).ToList();
            return View(MemResList);
        }





        // GET: Reserves/Create
        public ActionResult Create()
        {
            Members MemberUser = (Members)Session["MemberUser"];
            
            ViewBag.MemberID = MemberUser.MemberID;
            ViewBag.Name= MemberUser.Name;
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "Name");
            return View();
        }

        // POST: Reserves/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reserves reserves)
        {
            if (ModelState.IsValid)
            {
                db.Reserves.Add(reserves);
                db.SaveChanges();
                return RedirectToAction("RestaurantList", "HomeMember");
            }

            Members MemberUser = (Members)Session["MemberUser"];
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", MemberUser.MemberID);
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
            Members MemberUser = (Members)Session["MemberUser"];
            //ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", reserves.MemberID);
            ViewBag.MemberID = MemberUser.MemberID;
            ViewBag.Name = MemberUser.Name;
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
                return RedirectToAction("ReservationList");
            }
            Members MemberUser = (Members)Session["MemberUser"];
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", reserves.MemberID);
            ViewBag.ReservationID = new SelectList(db.Restaurants, "RestaurantID", "Name", reserves.ReservationID);
            return View(reserves);
        }


        // GET: Administers/Delete/5
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

        // POST: Administers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserves reserves = db.Reserves.Find(id);
            db.Reserves.Remove(reserves);
            db.SaveChanges();
            return RedirectToAction("ReservationList");
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