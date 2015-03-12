using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EPocalipse.Json.Viewer
{
    public partial class GridVisualizer : UserControl, IJsonVisualizer
    {
        public GridVisualizer()
        {
            InitializeComponent();
        }

        Control IJsonVisualizer.GetControl(JsonObject jsonObject)
        {
            return this;
        }

        void IJsonVisualizer.Visualize(JsonObject jsonObject)
        {
            lvGrid.BeginUpdate();
            try
            {
                lvGrid.Columns.Clear();
                lvGrid.Items.Clear();
                FillHeaders(jsonObject.Fields["headers"]);
                FillRows(jsonObject.Fields["rows"]);
            }
            finally
            {
                lvGrid.EndUpdate();
            }
        }

        private void FillHeaders(JsonObject jsonObject)
        {
            foreach (JsonObject header in jsonObject.Fields)
            {
                JsonObject nameHeader = header.Fields["name"];
                if (nameHeader.JsonType == JsonType.Value && nameHeader.Value is string)
                {
                    string name = (string)nameHeader.Value;
                    lvGrid.Columns.Add(name);
                }
            }
        }

        private void FillRows(JsonObject jsonObject)
        {
            string value;
            foreach (JsonObject row in jsonObject.Fields)
            {
                List<string> rowValues = new List<string>();
                foreach (JsonObject rowValue in row.Fields)
                {
                    if (rowValue.JsonType == JsonType.Value && rowValue.Value != null)
                    {
                        value = rowValue.Value.ToString();
                    }
                    else
                        value = String.Empty;
                    rowValues.Add(value);
                }
                ListViewItem rowItem = new ListViewItem(rowValues.ToArray());
                lvGrid.Items.Add(rowItem);
            }
        }

        string IJsonViewerPlugin.DisplayName
        {
            get { return "Grid"; }
        }

        bool IJsonViewerPlugin.CanVisualize(JsonObject jsonObject)
        {
            return jsonObject.ContainsField("headers", JsonType.Array) &&
                jsonObject.ContainsField("rows", JsonType.Array);
        }
    }
}
