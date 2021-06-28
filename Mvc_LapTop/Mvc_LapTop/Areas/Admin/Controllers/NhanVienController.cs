using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_LapTop.Models;
namespace Mvc_LAPTOP.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        //
        // GET: /Admin/NhanVien/
        QLLAPTOPEntities2 db = new QLLAPTOPEntities2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowNhanVien()
        {
            var listnv = db.NhanViens.ToList();
            return View(listnv);
        }
        [HttpGet]
        public ActionResult ADD()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ADD(FormCollection f)
        {
            NhanVien nv = new NhanVien();
            var manv = f["manv"];
            var tennv = f["tennv"];
            var gioitinh = f["gioitinh"];
            var luong = f["luong"];
            var chucvu = f["chucvu"];
            var diachi = f["diachi"];
            var sdt = f["sdt"];
            var ngaysinh = string.Format("{0:MM/DD/YYYY}", f["ngaysinh"]);
            if (string.IsNullOrEmpty(manv))
            {
                ViewData["loi1"] = "Vui lòng Nhập Mã Nhân Viên";
            }
            if (string.IsNullOrEmpty(tennv))
            {
                ViewData["loi2"] = "Vui Lòng Nhập Họ Tên";
            }
            if (string.IsNullOrEmpty(luong))
            {
                ViewData["loi3"] = "vui lòng Nhập Lương";
            }
            if (string.IsNullOrEmpty(diachi))
            {
                ViewData["loi4"] = "vui Lòng Nhập Địa Chỉ";
            }
            if (string.IsNullOrEmpty(sdt))
            {
                ViewData["loi5"] = "vui Lòng Nhập Số Điện Thoại";
            }
            if (!string.IsNullOrEmpty(manv) && !string.IsNullOrEmpty(tennv) && !string.IsNullOrEmpty(luong) && !string.IsNullOrEmpty(diachi) && !string.IsNullOrEmpty(sdt))
            {
                NhanVien nv1 = new NhanVien();
                nv1 = db.NhanViens.SingleOrDefault(ma => ma.MaNhanVien == manv);
                if (nv1 == null)
                {
                    nv.MaNhanVien = manv;
                    nv.TenNhanVien = tennv;
                    nv.GioiTinh = gioitinh;
                    nv.Luong = int.Parse(luong);
                    nv.ChucVu = chucvu;
                    nv.DiaChi = diachi;
                    nv.SDT = int.Parse(sdt);
                    nv.NgaySinh = DateTime.Parse(ngaysinh);
                    db.NhanViens.Add(nv);
                    db.SaveChanges();
                }
                else
                    ViewBag.TB = "NHÂN VIÊN ĐÃ TỒN TẠI";
            }
                return View();
        }
        public ActionResult DETAILS(string manv)
        {
            NhanVien nv = db.NhanViens.Single(h => h.MaNhanVien == manv);
            if (nv== null)
            {
                return HttpNotFound();
            }
            return View(nv);
        }
        [HttpPost]
        public ActionResult EDIT(NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Attach(nv);
                db.Entry(nv).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowNhanVien", "NhanVien");
            }
            return View(nv);
        }
        public ActionResult EDIT(string manv= "")
        {
            NhanVien nv = db.NhanViens.Single(ma => ma.MaNhanVien == manv);
            if (nv== null)
            {
                return HttpNotFound();
            }
            return View(nv);
        }
        public ActionResult REMOVE(string manv)
        {
            NhanVien hh = db.NhanViens.Single(ma => ma.MaNhanVien==manv);
            if (manv== null)
            {
                return HttpNotFound();
            }
            return View(hh);
        }
        [HttpPost, ActionName("REMOVE")]
        public ActionResult DeleteHHConfirm(string manv)
        {
            //Lấy ra emp cần xóa
            NhanVien nv = db.NhanViens.Single(ma => ma.MaNhanVien==manv);
            if (nv == null)
            {
                return HttpNotFound();
            }
            db.Entry(nv).State = System.Data.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("ShowNhanVien", "NhanVien");
        }
        public ActionResult SeachSP(string seachstring)
        {
            var link = from l in db.NhanViens
                       select l;
            if (!string.IsNullOrEmpty(seachstring))
            {
                link = link.Where(t => t.TenNhanVien.Contains(seachstring));
            }
            return View(link);
        }
    }
}
