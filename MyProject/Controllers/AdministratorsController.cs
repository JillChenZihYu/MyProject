﻿using System;
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
    public class AdministratorsController : Controller
    {
        private ReserveRobotEntities1 db = new ReserveRobotEntities1();

        // GET: Administrators
        public ActionResult Index()
        {
            return View(db.Administrators.ToList());
        }

        // GET: Administrators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrators administrators = db.Administrators.Find(id);
            if (administrators == null)
            {
                return HttpNotFound();
            }
            return View(administrators);
        }

        // GET: Administrators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrators/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdministratorID,Name,Email,Password,Authorize")] Administrators administrators)
        {
            if (ModelState.IsValid)
            {
                db.Administrators.Add(administrators);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(administrators);
        }

        // GET: Administrators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrators administrators = db.Administrators.Find(id);
            if (administrators == null)
            {
                return HttpNotFound();
            }
            return View(administrators);
        }

        // POST: Administrators/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdministratorID,Name,Email,Password,Authorize")] Administrators administrators)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrators).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(administrators);
        }

        // GET: Administrators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrators administrators = db.Administrators.Find(id);
            if (administrators == null)
            {
                return HttpNotFound();
            }
            return View(administrators);
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrators administrators = db.Administrators.Find(id);
            db.Administrators.Remove(administrators);
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
