﻿using Dashboard.Configuration.Widgets;
using Dashboard.Controllers;
using Dashboard.Controllers.Layouts;
using Dashboard.DashboardComponent.Models;
using DashboardFramework.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dashboard.Export;

namespace Dashboard.Configuration.Navigations
{
    public class IntProductsAllLocationByProductsConfiguration : NavigationConfiguration
    {
        public IntProductsAllLocationByProductsConfiguration(NavigationItem navigationItem)
        {
            HasName(navigationItem.Name);
            HasLabel(navigationItem.Label);
            Layout.HasConfig(navigationItem).HasController<AtAGlanceNavigationLayoutController>();
            HasWidget(new IntProductsAllLocationByProductsWidgetConfiguration(navigationItem.Widgets[0]));

            this.HasExportController<NavExportController>();
            

            ExtendedProperties.Add("ExportFileName").WithValue(navigationItem.Label + "_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}