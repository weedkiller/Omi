﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dashboard.ViewModels;
using DashboardFramework.Web.Controllers;

namespace Dashboard.Controllers.Filters
{
    public class RadioFilterLayoutController : LayoutBaseController<FilterVisibilityConfig>
    {

        public override ViewResult Index()
        {
            return View(Config);
        }

    }
}
