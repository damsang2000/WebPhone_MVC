using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mvc_LapTop.Models;
namespace Mvc_LapTop.Controllers
{
    public class SanPhamAPTController : ApiController
    {
        QLLAPTOPEntities2 db = new QLLAPTOPEntities2();
        public IEnumerable<HangHoa> GET()
        {
            return db.HangHoas.ToList();
        }
        public IEnumerable<HangHoa> GET(string id)
        {
            return db.HangHoas.Where(ma => ma.MaHang == id).ToList();
        }
    }
}
