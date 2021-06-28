using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_LapTop.Models;
namespace Mvc_LAPTOP.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/
        QLLAPTOPEntities2 db = new QLLAPTOPEntities2();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhapAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhapAdmin(FormCollection f)
        {
            var username = f["username"];
            var password = f["password"];
            if (string.IsNullOrEmpty(username))
            {
                ViewData["loi1"] = "Enter Username";
            }
             if (string.IsNullOrEmpty(password))
            {
                ViewData["loi2"] = "Enter Password";
            }
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                int count = db.ADMINs.Where(t => t.username == username && t.password == password).Count();
                if (count == 1)
                    return RedirectToAction("ShowHangHoa","HangHoa");
                else
                    ViewBag.TB = "Error UserName or Password";
            }
            return View();
        }
        public ActionResult TrangChu()
        {
            return View();
        }
    }
}
