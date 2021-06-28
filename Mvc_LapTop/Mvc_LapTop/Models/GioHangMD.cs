using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc_LapTop.Models;
namespace Mvc_LapTop.Models
{
    public class GioHangMD
    {
        QLLAPTOPEntities2 db = new QLLAPTOPEntities2();
        public string iMaHang { get; set; }
        public string sTenHang { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        public GioHangMD(string MaHang)
        {
            iMaHang = MaHang;
            HangHoa hang = db.HangHoas.Single(s => s.MaHang == iMaHang);
            sTenHang = hang.TenHang;
            sAnhBia = hang.AnhBia;
            dDonGia = double.Parse(hang.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}