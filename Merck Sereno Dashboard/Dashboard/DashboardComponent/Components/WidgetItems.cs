﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dashboard.Configuration;
using Dashboard.DashboardComponent.Models;

namespace Dashboard.DashboardComponent.Components
{
    public static class WidgetItems
    {
        public static WidgetItem WidgetWelcomeTextnImage()
        {
            return new WidgetItem()
            {
                Name = "WidgetWelcomeTextnImage",
                ViewId = "not applicable",
                HasParamDependency = new List<string>(){ParameterList.RegionOrCluster}
            };
        }

        public static WidgetItem AllRegionCombinationChart()
        {
            return new WidgetItem()
            {
                Name = "AllRegionCombinationChart",
                ViewId = "103",
                HasParamDependency = new List<string>() { ParameterList.UncheckedItems}
            };
        }

        public static WidgetItem CategoryTrendTableChartWidget()
        {
            return new WidgetItem()
            {
                Name = "CategoryTrendTableChartWidget",
                ViewId = "505",
                HasParamDependency = new List<string>() { ParameterList.UncheckedItems}
            };
        } 

        /* User Management Navigation Widgets */
        public static WidgetItem UserList()
        {
            return new WidgetItem()
            {
                Name = "UserManagement1",
                HasParamDependency = new List<string>() {  },
                ViewId = "1"
            };
        }

    }
}