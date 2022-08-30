using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;

namespace MyProject.Controllers
{
   
    public class HomeManagerController : Controller
    {
        ReserveRobotNewEntities1 db = new ReserveRobotNewEntities1();

        // GET: HomeManager
        [LoginCheck]  //把LoginCheck的Controller內寫的登入規則放在Index的Action裡，放在這裡只有Index適用
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(VMAdminLogin vMAdminLogin)
        {
            //Select * from Administers where Email=@Email and password=@password
            var user = db.Administrators.Where(a => a.Email == vMAdminLogin.Email && a.Password == vMAdminLogin.Password).FirstOrDefault();

            if (user == null)
            {
                ViewBag.ErrMsg = "帳號或密碼有誤！";
                return View(vMAdminLogin);
            }
            else
            {
                //未啟用授權管理員帳號禁止登入功能
                var userBlock = db.Administrators.Where(a => a.AdministratorID == user.AdministratorID).FirstOrDefault();

                if (userBlock.Authorize ==false) 
                {
                    ViewBag.BlockMsg = "此管理員帳號尚未啟用！";
                    return View(vMAdminLogin);
                }

                Session["user"] = user;/*Session作為判斷是否為管理員的狀態，可全域使用，生命週期為瀏覽器關掉為止，user為管理員狀態*/
                return RedirectToAction("Index");
            }
        }

        [LoginCheck]  //把LoginCheck的Controller內寫的登入規則放在Index的Action裡，放在這裡只有Index適用
        public ActionResult Logout()
        {
            Session["user"] = null; /*null為非會員狀態*/
            return RedirectToAction("Index","Home");
        }
    }
}