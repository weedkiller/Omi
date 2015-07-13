﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Component.Node;
using Dashboard.DataComponents.DataSources;

namespace Dashboard.Helper.Factory
{
    public class ColorfulDivCellFactory : NodeFactoryBase<object, NodeBase>
    {
        private ColorListDataSource _colorSource;
        public string UncheckedItem { get; set; }
        public string KPI { get; set; }
        public int RecordCount = 1;

        public ColorfulDivCellFactory()
        {
            _colorSource = new ColorListDataSource();
            Styles = new StyleDictionary();
        }

        protected override NodeBase CreateInternal(object data)
        {
            var values = data as List<string>;

            var complexNode = new ComplexNode("td");

            complexNode.ChildNodes.Add(new SimpleNode("span", values != null ? Convert.ToString(RecordCount) : "") { Classes = new List<string>() { "rank-div" } });
                string colorValue = "";
                if (values[1].ToUpper().Contains("TOTAL"))
                {
                    colorValue = ColorListDataSource.ColorOfTotal.Replace("#", "");
                }
                else
                    colorValue = _colorSource.GetNextColor();
                complexNode.ChildNodes.Add(new SimpleNode("span", string.Empty)
                {
                    Classes = new List<string>() {"color-div"},
                    Styles = new Dictionary<string, string>()
                    {
                        {"background-color", "#" + colorValue}
                    }
                });

            RecordCount++;

            return complexNode;
        }
    }
}