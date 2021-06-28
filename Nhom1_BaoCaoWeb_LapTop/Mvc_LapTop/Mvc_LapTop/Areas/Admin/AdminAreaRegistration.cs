﻿using System.Web.Mvc;

namespace Mvc_LAPTOP.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "DangNhapAdmin", id = UrlParameter.Optional },
                new[] { "Mvc_LAPTOP.Areas.Admin.Controllers" }
            );
        }
    }
}
