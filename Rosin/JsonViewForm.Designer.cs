namespace Rosin
{
    partial class JsonViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.jsonViewer = new EPocalipse.Json.Viewer.JsonViewer();
            this.SuspendLayout();
            // 
            // jsonViewer
            // 
            this.jsonViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jsonViewer.Json = null;
            this.jsonViewer.Location = new System.Drawing.Point(0, 0);
            this.jsonViewer.Name = "jsonViewer";
            this.jsonViewer.Size = new System.Drawing.Size(833, 522);
            this.jsonViewer.TabIndex = 0;
            // 
            // JsonViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 522);
            this.Controls.Add(this.jsonViewer);
            this.Name = "JsonViewForm";
            this.Text = "JSON Viewer";
            this.Load += new System.EventHandler(this.JsonViewForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private EPocalipse.Json.Viewer.JsonViewer jsonViewer;
    }
}