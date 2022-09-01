﻿using System;
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
    public class FavoriteListsController : Controller
    {
        private ReserveRobotNewNewEntities db = new ReserveRobotNewNewEntities();

        // GET: FavoriteLists
        public ActionResult Index(int page = 1)//預設page在第1頁
        {
           
            var favoriteList = db.FavoriteLists.ToList();

            int pagesize = 15; //一頁要有幾筆資料

            var pagedList = favoriteList.ToPagedList(page, pagesize);


            return View(pagedList);
        }

        // GET: FavoriteLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteLists favoriteLists = db.FavoriteLists.Find(id);
            if (favoriteLists == null)
            {
                return HttpNotFound();
            }
            return View(favoriteLists);
        }

        // GET: FavoriteLists/Create
        public ActionResult Create()
        {
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name");
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "Name");
            return View();
        }

        // POST: FavoriteLists/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UID,MemberID,RestaurantID")] FavoriteLists favoriteLists)
        {
            if (ModelState.IsValid)
            {
                db.FavoriteLists.Add(favoriteLists);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", favoriteLists.MemberID);
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "Name", favoriteLists.RestaurantID);
            return View(favoriteLists);
        }

        // GET: FavoriteLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteLists favoriteLists = db.FavoriteLists.Find(id);
            if (favoriteLists == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", favoriteLists.MemberID);
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "Name", favoriteLists.RestaurantID);
            return View(favoriteLists);
        }

        // POST: FavoriteLists/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UID,MemberID,RestaurantID")] FavoriteLists favoriteLists)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favoriteLists).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", favoriteLists.MemberID);
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "Name", favoriteLists.RestaurantID);
            return View(favoriteLists);
        }

        // GET: FavoriteLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteLists favoriteLists = db.FavoriteLists.Find(id);
            if (favoriteLists == null)
            {
                return HttpNotFound();
            }
            return View(favoriteLists);
        }

        // POST: FavoriteLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FavoriteLists favoriteLists = db.FavoriteLists.Find(id);
            db.FavoriteLists.Remove(favoriteLists);
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
