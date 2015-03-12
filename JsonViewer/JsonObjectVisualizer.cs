using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EPocalipse.Json.Viewer
{
    public partial class JsonObjectVisualizer : UserControl, IJsonVisualizer
    {
        public JsonObjectVisualizer()
        {
            InitializeComponent();
        }

        string IJsonViewerPlugin.DisplayName
        {
            get { return "Property Grid"; }
        }

        Control IJsonVisualizer.GetControl(JsonObject jsonObject)
        {
            return this;
        }

        void IJsonVisualizer.Visualize(JsonObject jsonObject)
        {
            this.pgJsonObject.SelectedObject = jsonObject;
        }


        bool IJsonViewerPlugin.CanVisualize(JsonObject jsonObject)
        {
            return true;
        }
    }
}
