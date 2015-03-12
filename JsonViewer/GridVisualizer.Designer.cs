namespace EPocalipse.Json.Viewer
{
    partial class GridVisualizer
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
            this.lvGrid = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvGrid
            // 
            this.lvGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGrid.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvGrid.Location = new System.Drawing.Point(0, 0);
            this.lvGrid.Name = "lvGrid";
            this.lvGrid.Size = new System.Drawing.Size(727, 301);
            this.lvGrid.TabIndex = 0;
            this.lvGrid.UseCompatibleStateImageBehavior = false;
            this.lvGrid.View = System.Windows.Forms.View.Details;
            // 
            // GridVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvGrid);
            this.Name = "GridVisualizer";
            this.Size = new System.Drawing.Size(727, 301);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvGrid;
    }
}
