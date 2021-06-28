using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_LapTop.Models;
namespace Mvc_LAPTOP.Areas.Admin.Controllers
{
    public class NhaCungCapController : Controller
    {
        //
        // GET: /Admin/NhaCungCap/
        QLLAPTOPEntities2 db = new QLLAPTOPEntities2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowNhaCC()
        {
            var listncc = db.NhaCungCaps.ToList();
            return View(listncc);
        }
        [HttpGet]
        public ActionResult ADD()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ADD(FormCollection f)
        {
            NhaCungCap ncc = new NhaCungCap();
            var mancc = f["mancc"];
            var tenncc = f["tenncc"];
            var diachi = f["diachi"];
            var sdt = f["sdt"];
            if (string.IsNullOrEmpty(mancc))
            {
                ViewData["loi1"] = "Vui lòng Nhập Mã Nhà Cung Cấp";
            }
            if (string.IsNullOrEmpty(tenncc))
            {
                ViewData["loi2"] = "Vui Lòng Nhập Tên Nhà Cung Cấp";
            }
            if (string.IsNullOrEmpty(diachi))
            {
                ViewData["loi3"] = "vui lòng Nhập Địa Chỉ";
            }
            if (string.IsNullOrEmpty(sdt))
            {
                ViewData["loi4"] = "vui Lòng Nhập Số Điện Thoại";
            }
            if (!string.IsNullOrEmpty(mancc) && !string.IsNullOrEmpty(tenncc) && !string.IsNullOrEmpty(diachi) && !string.IsNullOrEmpty(sdt))
            {
                NhaCungCap ncc1 = new NhaCungCap();
                ncc1 = db.NhaCungCaps.SingleOrDefault(ma => ma.MaNhaCC == mancc);
                if (ncc1 == null)
                {
                    ncc.MaNhaCC = mancc;
                    ncc.TenNhaCC = tenncc;
                    ncc.DiaChi = diachi;
                    ncc.DienThoai = int.Parse(sdt.ToString());
                    db.NhaCungCaps.Add(ncc);
                    db.SaveChanges();
                }
                else
                    ViewBag.TB = "NHÀ CUNG CẤP ĐÃ TỒN TẠI";
            }
            return View();
        }
        [HttpPost]
        public ActionResult EDIT(NhaCungCap ncc)
        {
            if (ModelState.IsValid)
            {
                db.NhaCungCaps.Attach(ncc);
                db.Entry(ncc).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowNhaCC", "NhaCungCap");
            }
            return View(ncc);
        }
        public ActionResult EDIT(string mancc = "")
        {
            NhaCungCap ncc = db.NhaCungCaps.Single(ma => ma.MaNhaCC == mancc);
            if (ncc == null)
            {
                return HttpNotFound();
            }
            return View(ncc);
        }
        public ActionResult DETAILS(string mancc)
        {
            NhaCungCap ncc= db.NhaCungCaps.Single(ma => ma.MaNhaCC==mancc);
            if (ncc == null)
            {
                return HttpNotFound();
            }
            return View(ncc);
        }
        public ActionResult REMOVE(string mancc)
        {
            NhaCungCap ncc = db.NhaCungCaps.Single(ma => ma.MaNhaCC == mancc);
            if (ncc == null)
            {
                return HttpNotFound();
            }
            return View(ncc);
        }
        [HttpPost, ActionName("REMOVE")]
        public ActionResult DeleteHHConfirm(string mancc)
        {
            //Lấy ra emp cần xóa
            NhaCungCap ncc = db.NhaCungCaps.Single(ma => ma.MaNhaCC == mancc);
            if (ncc == null)
            {
                return HttpNotFound();
            }
            db.Entry(ncc).State = System.Data.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("ShowNhaCC", "NhaCungCap");
        }
        public ActionResult SeachSP(string seachstring)
        {
            var link = from l in db.NhaCungCaps
                       select l;
            if (!string.IsNullOrEmpty(seachstring))
            {
                link = link.Where(t => t.TenNhaCC.Contains(seachstring));
            }
            return View(link);
        }
    }
}
