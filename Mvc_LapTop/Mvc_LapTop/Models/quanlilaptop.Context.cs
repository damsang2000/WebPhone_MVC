﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mvc_LapTop.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLLAPTOPEntities2 : DbContext
    {
        public QLLAPTOPEntities2()
            : base("name=QLLAPTOPEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ADMIN> ADMINs { get; set; }
        public DbSet<ChiTietHDBan> ChiTietHDBans { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<HoaDonBan> HoaDonBans { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<LoaiHang> LoaiHangs { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<TrungTamBaoHanh> TrungTamBaoHanhs { get; set; }
    }
}
