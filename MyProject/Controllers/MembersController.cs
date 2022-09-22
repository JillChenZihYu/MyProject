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
    

    public class MembersController : Controller
    {
        private ReservationEntities db = new ReservationEntities();

        // GET: Members
        [LoginCheck]
        public ActionResult Index(int page=1) //預設page在第1頁
        {
            var members=db.Members.ToList();

            int pagesize = 15; //一頁要有幾筆資料

            var pagedList = members.ToPagedList(page, pagesize);
            
            
            return View(pagedList);
        }


        // GET: Members/Details/5
        [LoginCheck]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members); 
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Members members)
        {

            var account = db.Members.Where(m => m.Email == members.Email).FirstOrDefault();
            
            if(account != null)
            {
                ViewBag.Error = "此帳號已註冊過";
                return View();
            }


            

            if (ModelState.IsValid)
            {
                db.Members.Add(members);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(); //預設return到Member，但因使用VMMember，所以刪掉
        }

        // GET: Members/Edit/5
        [LoginCheck]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // POST: Members/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [LoginCheck]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,Name,Gender,DateOfBirth,ContactNumber,Email,Password")] Members members)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(members).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(members);
        }

        // GET: Members/Delete/5
        [LoginCheck]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // POST: Members/Delete/5
        [LoginCheck]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Members members = db.Members.Find(id);
            db.Members.Remove(members);
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
