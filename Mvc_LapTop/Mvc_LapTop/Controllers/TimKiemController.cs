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
    public class TimKiemController : Controller
    {
        //
        // GET: /TimKiem/
        QLLAPTOPEntities2 db = new QLLAPTOPEntities2();
        public ActionResult Index()
        {
            return View();
        }
    }
}
