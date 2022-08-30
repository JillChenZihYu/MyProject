﻿using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        ReserveRobotNewEntities1 db = new ReserveRobotNewEntities1();

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

                if (userBlock.Blocks) //()內已是true，所以不需要再寫==true
                {
                    ViewBag.BlockMsg = "此帳號已被封鎖！";
                    return View(vMLogin);
                }
                else
                    return RedirectToAction("Index");
            }
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Index");
        }


        public ActionResult RestaurantList()
        {
            var restaurants = db.Restaurants.ToList();

            return View(restaurants);
        }

    }
}