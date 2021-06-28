using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Mvc_LapTop.Models;
using PagedList;
using PagedList.Mvc;
namespace Mvc_LapTop.Controllers
{
    public class SanPhamController : Controller
    {
        //
        // GET: /SanPham/
        QLLAPTOPEntities2 db = new QLLAPTOPEntities2();
        public ActionResult Index(int page=1,int pagelist=5)
        {
            var model = db.HangHoas.ToPagedList(page, pagelist);
            return View(model);
        }
        public ActionResult XemSanPham(int page = 1, int pagelist = 4)
        {
            var model = db.HangHoas.OrderBy(t=>t.TenHang).ToPagedList(page, pagelist);
            return View(model);
        }

        public ActionResult XemChiTietSP(string ms)
        {
            //var ListHangHoa = db.HangHoas.OrderBy(h => h.MaHang).ToList();
            //return View(ListHangHoa);
            HangHoa hang = db.HangHoas.Single(h => h.MaHang == ms);
            if (hang == null)
            {
                return HttpNotFound();
            }
            return View(hang);
        }
        public ActionResult LienHe()
        {
           return View();
        }
        public ActionResult SeachSP(string seachstring)
        {
            var link = from l in db.HangHoas
                       select l;
            if (!string.IsNullOrEmpty(seachstring))
            {
                link = link.Where(t => t.TenHang.Contains(seachstring));
            }
            return View(link);
        }
    }
}
