using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    [MemberLoginCheck]

    public class HomeMemberController : Controller
    {

        ReserveRobotNewNewEntities db = new ReserveRobotNewNewEntities();

        // GET: HomeMember

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult RestaurantList()
        {
            var restaurants = db.Restaurants.ToList();

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


    }
}