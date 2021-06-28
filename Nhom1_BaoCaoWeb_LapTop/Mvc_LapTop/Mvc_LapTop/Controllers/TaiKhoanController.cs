using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Mvc_LapTop.Models;

namespace Mvc_LapTop.Controllers
{
    public class TaiKhoanController : Controller
    {
        //
        // GET: /TaiKhoan/

        public ActionResult Index()
        {
            return View();
        }
        QLLAPTOPEntities2 db = new QLLAPTOPEntities2();
        //ĐĂNG KÝ
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(KhachHang kh, FormCollection f)
        {
            //Gán các giá trị người dùng nhập từ form f cho các biến

            var tendn = f["TenDN"];
            var hoten = f["HoTenKH"];
            var matkhau = f["MatKhau"];
            var rematkhau = f["ReMatKhau"];
            var dienthoai = f["DienThoai"];
            var ngaysinh = String.Format("{0:MM/DD/YYYY}", f["NgaySinh"]);
            var email = f["Email"];
            var diachi = f["DiaChi"];
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["loi1"] = "Họ tên không được bỏ trống";
            }
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi2"] = "Tên đăng nhập không được bỏ trống";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi3"] = "Vui lòng nhập mật khẩu";
            }
            if (String.IsNullOrEmpty(rematkhau))
            {
                ViewData["loi4"] = "Vui lòng nhập lại mật khẩu";
            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["loi5"] = "Vui lòng nhập số điện thoại";
            }
            if (!String.IsNullOrEmpty(hoten) && !String.IsNullOrEmpty(tendn) && !String.IsNullOrEmpty(matkhau) && !String.IsNullOrEmpty(rematkhau) && !String.IsNullOrEmpty(dienthoai) && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(diachi))
            {
                kh.TaiKhoan = tendn;
                kh.MaKhachHang = hoten;
                kh.MatKhau = matkhau;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                kh.DiaChi = diachi;
                kh.Email = email;
                //ghi xuong csdl
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("DangNhap", "TAIKHOAN");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(KhachHang kh, FormCollection f)
        {
            //khai bao cac bien nhan gia tri tu form f
            var tendn = f["TenDN"];
            var matkhau = f["MatKhau"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["loi6"] = "Tên đăng nhập không được trống";
            }
            if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["loi7"] = "vui lòng nhập mật khẩu";
            }
            if (!String.IsNullOrEmpty(tendn) && !String.IsNullOrEmpty(matkhau))
            {
                kh = db.KhachHangs.SingleOrDefault(tk => tk.TaiKhoan == tendn && tk.MatKhau == matkhau);
                if (kh != null)
                {
                    Session["taikhoan"] = kh;
                    return RedirectToAction("XemGioHang", "GioHang");

                }
                else
                {
                    ViewBag.TB = "SAI TÊN ĐĂNG NHẬP";
                }
            }
            return View();
        }

    }
}
