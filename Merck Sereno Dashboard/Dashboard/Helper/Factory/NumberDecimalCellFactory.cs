﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using Component.Node;

namespace Dashboard.Helper.Factory
{
    public class NumberDecimalCellFactory : NodeFactoryBase<string, NodeBase>
    {
        public TextFormat TextFormat;
        public int colId { get; set; }
        public string suffix { get; set; }

        public NumberDecimalCellFactory()
        {
            TextFormat = new TextFormat { FormatString = "#,#0.0" };
        }

        protected override NodeBase CreateInternal(string data)
        {
            if (data == null || string.IsNullOrWhiteSpace(data))
                return new SimpleNode("td", string.Empty)
                {
                    Attributes = new Dictionary<string, string>() { { "column-index" ,colId.ToString()} }
                };
            if (data == "--")//rankname == '--'
            {
                return new SimpleNode("td", data+"%")
                {
                    Attributes = new Dictionary<string, string>() { { "column-index", colId.ToString() } }
                };
            }
            if (TextFormat != null)
                data = TextFormat.Format(data);

            if (suffix != null || !string.IsNullOrEmpty(suffix))
                data += suffix;
            return new SimpleNode("td", data)
            {
                Attributes = new Dictionary<string, string>() { { "column-index", colId.ToString() } }
            };
        }
    }
}