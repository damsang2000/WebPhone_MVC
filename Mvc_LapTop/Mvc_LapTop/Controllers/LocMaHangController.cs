using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_LapTop.Models;

namespace Mvc_LapTop.Controllers
{
    public class LocMaHangController : Controller
    {
        //
        // GET: /LocMaHang/
        QLLAPTOPEntities2 db = new QLLAPTOPEntities2();
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult ShowLoaiHang()
        {
            var listLoaiHang = db.LoaiHangs.Take(10).OrderBy(lh => lh.TenLoaiHang).ToList();
            return View(listLoaiHang);
        }

        public ActionResult SanPhamTheoloai(string cd)
        {
            var listLoaiHang = db.HangHoas.Where(s => s.MaLoaiHang ==cd).ToList();
            if (listLoaiHang.Count == 0)
            {
                ViewBag.LoaiHang = "Không có sản loại này";
            }
            return View(listLoaiHang);
        }
    }
}
