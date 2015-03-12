namespace EPocalipse.Json.Viewer
{
    partial class JsonObjectVisualizer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pgJsonObject = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // pgJsonObject
            // 
            this.pgJsonObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgJsonObject.HelpVisible = false;
            this.pgJsonObject.Location = new System.Drawing.Point(0, 0);
            this.pgJsonObject.Name = "pgJsonObject";
            this.pgJsonObject.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.pgJsonObject.Size = new System.Drawing.Size(304, 467);
            this.pgJsonObject.TabIndex = 4;
            this.pgJsonObject.ToolbarVisible = false;
            // 
            // JsonObjectVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgJsonObject);
            this.Name = "JsonObjectVisualizer";
            this.Size = new System.Drawing.Size(304, 467);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgJsonObject;
    }
}
