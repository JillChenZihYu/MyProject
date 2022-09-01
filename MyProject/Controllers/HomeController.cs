using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        ReserveRobotNewNewEntities db = new ReserveRobotNewNewEntities();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(VMLogin vMLogin)
        {
            //Select * from Administers where Email=@Email and password=@password
            var user = db.Members.Where(m => m.Email == vMLogin.Email && m.Password == vMLogin.Password).FirstOrDefault();

            if (user == null)
            {
                ViewBag.ErrMsg = "帳號或密碼有誤！";
                return View(vMLogin);
            }
            else
            {


                //封鎖帳號禁止登入功能
                var userBlock = db.Administers.Where(a => a.MemberID == user.MemberID).FirstOrDefault();
            if (userBlock == null)
                {
                    return RedirectToAction("MemberIndex");

                }
                if (userBlock.Blocks) //()內已是true，所以不需要再寫==true
                {
                    ViewBag.BlockMsg = "此帳號已被封鎖！";
                    return View(vMLogin);
                }
                else
                    Session["MemberUser"] = user;/*Session作為判斷是否為會員的狀態，可全域使用，生命週期為瀏覽器關掉為止，user為會員狀態*/
             
                return RedirectToAction("MemberIndex");
            }
        }

        
        [MemberLoginCheck]
        public ActionResult Logout()
        {
            Session["MemberUser"] = null;
            return RedirectToAction("Index");
        }


        [MemberLoginCheck]
        public ActionResult RestaurantList()
        {
            var restaurants = db.Restaurants.ToList();

            return View(restaurants);
        }

        [MemberLoginCheck]
        public ActionResult MemberIndex()
        {
            return View();
        }

        [MemberLoginCheck]
        public ActionResult ReservationList()
        {

            Members MemberUser = (Members)Session["MemberUser"];
            //var reserves = db.Reserves.ToList();
            var MemResList = db.Reserves.Where(a => a.MemberID == MemberUser.MemberID).ToList();
            return View(MemResList);
        }

    }
}