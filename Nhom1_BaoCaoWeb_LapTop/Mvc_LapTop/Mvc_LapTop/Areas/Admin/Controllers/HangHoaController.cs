using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_LapTop.Models;
using System.Data.Entity;
namespace Mvc_LapTop.Areas.Admin.Controllers
{
    public class HangHoaController : Controller
    {
        //
        // GET: /Admin/HangHoa/
        QLLAPTOPEntities2 db = new QLLAPTOPEntities2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowHangHoa()
        {
            var listhh = db.HangHoas.ToList();
            return View(listhh);
        }
        [HttpGet]
        public ActionResult ADD()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ADD(FormCollection f)
        {
            HangHoa hh = new HangHoa();
            var mahang = f["mahang"];
            var tenhang = f["tenhang"];
            var maloaihg = f["maloaihg"];
            var soluongton = f["soluongton"];
            var giaban = f["giaban"];
            var anhbia = f["anhbia"];
            var mota = f["mota"];
            if (string.IsNullOrEmpty(mahang))
            {
                ViewData["loi1"] = "Vui lòng Nhập Mã Hàng";
            }
            if (string.IsNullOrEmpty(tenhang))
            {
                ViewData["loi2"] = "Vui Lòng Tên Hàng";
            }
            if (string.IsNullOrEmpty(soluongton))
            {
                ViewData["loi3"] = "vui lòng Số Lượng Tồn";
            }
            if (string.IsNullOrEmpty(giaban))
            {
                ViewData["loi4"] = "vui Lòng Giá Bán";
            }
            if (string.IsNullOrEmpty(anhbia))
            {
                ViewData["loi5"] = "vui Lòng Chọn Ảnh";
            }
            if (!string.IsNullOrEmpty(mahang) && !string.IsNullOrEmpty(tenhang) && !string.IsNullOrEmpty(maloaihg) && !string.IsNullOrEmpty(soluongton) && !string.IsNullOrEmpty(giaban))
            {
                HangHoa hh1 = new HangHoa();
                hh1 = db.HangHoas.SingleOrDefault(ma => ma.MaHang == mahang);
                if (hh1 == null)
                {
                    hh.MaHang = mahang;
                    hh.TenHang = tenhang;
                    hh.MaLoaiHang = maloaihg;
                    hh.SoLuongTon = int.Parse(soluongton);
                    hh.GiaBan = int.Parse(giaban);
                    hh.AnhBia = anhbia;
                    hh.MoTa = mota;
                    db.HangHoas.Add(hh);
                    db.SaveChanges();
                }
                else
                    ViewBag.TB = "HÀNG HÓA ĐÃ TỒN TẠI";
            }
            return View();
        }
        public ActionResult DETAILS(string mahg)
        {
            HangHoa hh = db.HangHoas.Single(ma=>ma.MaHang==mahg);
            if (hh== null)
            {
                return HttpNotFound();
            }
            return View(hh);
        }
        [HttpPost]
        public ActionResult EDIT(HangHoa hh)
        {
            if (ModelState.IsValid)
            {
                db.HangHoas.Attach(hh);
                db.Entry(hh).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowHangHoa", "HangHoa");
            }
            return View(hh);
        }
        public ActionResult EDIT(string mahh="")
        {
            HangHoa hh = db.HangHoas.Single(ma => ma.MaHang == mahh);
            if (hh == null)
            {
                return HttpNotFound();
            }
            return View(hh);
        }
        //[HttpPost]
        //public ActionResult EDIT(HangHoa hh)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.HangHoa.Attach(hh);
        //        db.Entry(hh).s
        //    }
        //}
        public ActionResult REMOVE(string mahh)
        {
            HangHoa hh = db.HangHoas.Single(ma => ma.MaHang == mahh);
            if (mahh == null)
            {
                return HttpNotFound();
            }
            return View(hh);
        }
        [HttpPost, ActionName("REMOVE")]
        public ActionResult DeleteHHConfirm(string mahh)
        {
            //Lấy ra emp cần xóa
            HangHoa hh = db.HangHoas.Single(ma => ma.MaHang == mahh);
            if (hh == null)
            {
                return HttpNotFound();
            }
            db.Entry(hh).State = System.Data.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("ShowHangHoa","HangHoa");
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
