//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            this.LoaiHangs = new HashSet<LoaiHang>();
        }
    
        public string MaNhaCC { get; set; }
        public string TenNhaCC { get; set; }
        public string DiaChi { get; set; }
        public Nullable<int> DienThoai { get; set; }
    
        public virtual ICollection<LoaiHang> LoaiHangs { get; set; }
    }
}
