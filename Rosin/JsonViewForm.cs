using EPocalipse.Json.Viewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Rosin
{
    public partial class JsonViewForm : Form
    {
        string jsonStr;

        public JsonViewForm(string jsonStr)
        {
            this.jsonStr = jsonStr;
            InitializeComponent();
        }

        private void JsonViewForm_Load(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(this.jsonStr))
            {
                this.jsonViewer.ShowTab(Tabs.Text);
            }
            else
            {
                showJSONView(null);
            }
        }

        public void showJSONView(string jsonStr)
        {
            if (!String.IsNullOrEmpty(jsonStr))
            {
                this.jsonStr = jsonStr;
            }
            this.jsonViewer.ShowTab(Tabs.Viewer);
            this.jsonViewer.Json = this.jsonStr;
        }
    }
}
