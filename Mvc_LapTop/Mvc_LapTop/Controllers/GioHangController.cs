using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Mvc_LapTop.Models;
using System.Data.SqlClient;

namespace Mvc_LapTop.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/

        public ActionResult Index()
        {
            return View();
        }
        QLLAPTOPEntities2 db = new QLLAPTOPEntities2();
        public List<GioHangMD> LayGioHang()
        {
            List<GioHangMD> lstGioHang = Session["GioHang"] as List<GioHangMD>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHangMD>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }


        //them gio hang
        public ActionResult ThemGioHang(string ms, string strURL)
        {
            //lay gio hang
            List<GioHangMD> lstGioHang = LayGioHang();
            //kiem tra san pham co ton tai chua
            GioHangMD SanPham = lstGioHang.Find(sp => sp.iMaHang == ms);
            if (SanPham == null)
            {
                SanPham = new GioHangMD(ms);
                lstGioHang.Add(SanPham);
                return Redirect(strURL);
            }
            else
            {
                SanPham.iSoLuong++;
                return Redirect(strURL);
            }
        }


        //tong so luong ?
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHangMD> lstGioHang = Session["GioHang"] as List<GioHangMD>;
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Sum(sp => sp.iSoLuong);
            }
            return tsl;
        }


        private double TongThanhTien()
        {
            double ttt = 0;
            List<GioHangMD> lstGioHang = Session["GioHang"] as List<GioHangMD>;
            if (lstGioHang != null)
            {
                ttt += lstGioHang.Sum(sp => sp.dThanhTien);
            }
            return ttt;
        }


        public ActionResult XemGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("GioHangRong", "GioHang");
            }
            List<GioHangMD> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(lstGioHang);
        }


        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return PartialView();
        }


        public ActionResult XoaGioHang(string ms)
        {
            // LAY GIO HANG
            List<GioHangMD> lstGioHang = LayGioHang();
            // kiem tra xem danh sach can xoa
            GioHangMD sp = lstGioHang.Single(s => s.iMaHang == ms);
            // neu co tien hanh xoa
            if (sp != null)
            {
                lstGioHang.RemoveAll(s => s.iMaHang == ms);
                return RedirectToAction("XemGioHang", "GioHang");
            }
            // neu gio hang rong
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("GioHangRong", "GioHang");
            }
            return RedirectToAction("XemGioHang", "GioHang");
        }


        public ActionResult XoaGioHang_All()
        {
            // lay gio hang
            List<GioHangMD> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("GioHangRong", "GioHang");
        }


        public ActionResult CapNhatGioHang(string ms, FormCollection f)
        {
            // lay gio hang
            List<GioHangMD> lstGioHang = LayGioHang();
            // kiem tra xem danh sach can xoa
            GioHangMD sp = lstGioHang.Single(s => s.iMaHang == ms);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("XemGioHang", "GioHang");
        }


        public ActionResult GioHangRong()
        {
            return View();
        }


        [HttpGet]
        public ActionResult DatHang()
        {
            //kiem tra dang nhap
            if (Session["taikhoan"] == null || Session["taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("XemSanPham", "SanPham");
            }
            //lay gio hang tu session
            List<GioHangMD> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(lstGioHang);
        }


        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            int count=db.HoaDonBans.Count();
            int so = count+1;
            //them don hang
            HoaDonBan ddh = new HoaDonBan();
            KhachHang kh = (KhachHang)Session["taikhoan"];
            List<GioHangMD> gh = LayGioHang();
            ddh.MaHDBan = "HDB"+so;
            ddh.MaKhachHang = kh.MaKhachHang;
            ddh.NgayDat = DateTime.Now;
            var NgayGiao = String.Format("{0:dd/mm/yyyy}", f["Ngaygiao"]);
            ddh.NgayGiao = DateTime.Parse(NgayGiao);
            ddh.DaThanhToan = "Chua Thanh Toan";
            ddh.TinhTrang = "Chua Giao" ;
            db.HoaDonBans.Add(ddh);
            db.SaveChanges();
            //them chi tietdon hang
            foreach (var item in gh)
            {
                ChiTietHDBan ctdh = new ChiTietHDBan();
                ctdh.MaHDBan = ddh.MaHDBan;
                ctdh.MaHang = item.iMaHang;
                ctdh.SLBan = item.iSoLuong;
                ctdh.GiaBan = int.Parse(item.dDonGia.ToString());
                ctdh.MaNhanVien = "NV10";
                db.ChiTietHDBans.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }

    }
}
