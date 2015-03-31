namespace Rosin
{
    partial class ConfigControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel = new System.Windows.Forms.Panel();
            this.labelEnableAll = new System.Windows.Forms.Label();
            this.enableCheckBox = new System.Windows.Forms.CheckBox();
            this.configButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.logTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pageListBox = new System.Windows.Forms.ComboBox();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.logContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showJSONItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.logLevelComboBox = new System.Windows.Forms.ComboBox();
            this.fileListBox = new System.Windows.Forms.ComboBox();
            this.Time = new System.Windows.Forms.Label();
            this.logFindTextBox = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.aboutTextBox = new System.Windows.Forms.RichTextBox();
            this.enableCheckToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cellontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.logTableLayoutPanel.SuspendLayout();
            this.logContextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.cellontextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1208, 650);
            this.tabControl.TabIndex = 0;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1200, 624);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Rule";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1194, 618);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Enabled,
            this.Type,
            this.Rule,
            this.Order});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(3, 43);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(1188, 572);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseClick);
            this.dataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDoubleClick);
            this.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
            // 
            // Enabled
            // 
            this.Enabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Enabled.Frozen = true;
            this.Enabled.HeaderText = "Enabled";
            this.Enabled.Name = "Enabled";
            this.Enabled.ReadOnly = true;
            this.Enabled.Width = 60;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Type.FillWeight = 192.7007F;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 50;
            // 
            // Rule
            // 
            this.Rule.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rule.FillWeight = 7.29927F;
            this.Rule.HeaderText = "Rule";
            this.Rule.Name = "Rule";
            this.Rule.ReadOnly = true;
            // 
            // Order
            // 
            this.Order.HeaderText = "Order";
            this.Order.Name = "Order";
            this.Order.ReadOnly = true;
            this.Order.Visible = false;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.labelEnableAll);
            this.panel.Controls.Add(this.enableCheckBox);
            this.panel.Controls.Add(this.configButton);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1188, 34);
            this.panel.TabIndex = 0;
            // 
            // labelEnableAll
            // 
            this.labelEnableAll.AutoSize = true;
            this.labelEnableAll.Location = new System.Drawing.Point(35, 9);
            this.labelEnableAll.Name = "labelEnableAll";
            this.labelEnableAll.Size = new System.Drawing.Size(71, 12);
            this.labelEnableAll.TabIndex = 2;
            this.labelEnableAll.Text = "Enabled All";
            this.labelEnableAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelEnableAll.Click += new System.EventHandler(this.labelEnableAll_Click);
            // 
            // enableCheckBox
            // 
            this.enableCheckBox.AutoSize = true;
            this.enableCheckBox.Checked = true;
            this.enableCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableCheckBox.Location = new System.Drawing.Point(14, 10);
            this.enableCheckBox.Name = "enableCheckBox";
            this.enableCheckBox.Size = new System.Drawing.Size(15, 14);
            this.enableCheckBox.TabIndex = 1;
            this.enableCheckToolTip.SetToolTip(this.enableCheckBox, "Enable Rosin");
            this.enableCheckBox.UseVisualStyleBackColor = true;
            this.enableCheckBox.CheckedChanged += new System.EventHandler(this.enableCheckBox_CheckedChanged);
            // 
            // configButton
            // 
            this.configButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.configButton.Location = new System.Drawing.Point(122, 6);
            this.configButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.configButton.MaximumSize = new System.Drawing.Size(100, 30);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(80, 20);
            this.configButton.TabIndex = 0;
            this.configButton.Text = "Add Rule";
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.logTableLayoutPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1200, 624);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // logTableLayoutPanel
            // 
            this.logTableLayoutPanel.ColumnCount = 1;
            this.logTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.logTableLayoutPanel.Controls.Add(this.pageListBox, 0, 0);
            this.logTableLayoutPanel.Controls.Add(this.logRichTextBox, 0, 2);
            this.logTableLayoutPanel.Controls.Add(this.panel1, 0, 1);
            this.logTableLayoutPanel.Controls.Add(this.logFindTextBox, 0, 3);
            this.logTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.logTableLayoutPanel.Name = "logTableLayoutPanel";
            this.logTableLayoutPanel.RowCount = 4;
            this.logTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.logTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.logTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.logTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.logTableLayoutPanel.Size = new System.Drawing.Size(1194, 618);
            this.logTableLayoutPanel.TabIndex = 0;
            // 
            // pageListBox
            // 
            this.pageListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pageListBox.FormattingEnabled = true;
            this.pageListBox.Location = new System.Drawing.Point(3, 5);
            this.pageListBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pageListBox.Name = "pageListBox";
            this.pageListBox.Size = new System.Drawing.Size(1188, 20);
            this.pageListBox.TabIndex = 1;
            this.pageListBox.SelectedIndexChanged += new System.EventHandler(this.pageListBox_SelectedIndexChanged);
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.ContextMenuStrip = this.logContextMenuStrip;
            this.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logRichTextBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logRichTextBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.logRichTextBox.Location = new System.Drawing.Point(3, 83);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.ReadOnly = true;
            this.logRichTextBox.Size = new System.Drawing.Size(1188, 502);
            this.logRichTextBox.TabIndex = 0;
            this.logRichTextBox.Text = "";
            this.logRichTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.logRichTextBox_MouseClick);
            // 
            // logContextMenuStrip
            // 
            this.logContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showJSONItem});
            this.logContextMenuStrip.Name = "logContextMenuStrip";
            this.logContextMenuStrip.Size = new System.Drawing.Size(140, 26);
            this.logContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.logContextMenuStrip_Opening);
            // 
            // showJSONItem
            // 
            this.showJSONItem.Name = "showJSONItem";
            this.showJSONItem.Size = new System.Drawing.Size(139, 22);
            this.showJSONItem.Text = "JSON View";
            this.showJSONItem.Click += new System.EventHandler(this.showJSONItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonDelete);
            this.panel1.Controls.Add(this.buttonClear);
            this.panel1.Controls.Add(this.buttonExport);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.logLevelComboBox);
            this.panel1.Controls.Add(this.fileListBox);
            this.panel1.Controls.Add(this.Time);
            this.panel1.Location = new System.Drawing.Point(3, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(649, 49);
            this.panel1.TabIndex = 2;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(161, 26);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 6;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(82, 26);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 5;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(3, 26);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 4;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(309, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Log Level:";
            // 
            // logLevelComboBox
            // 
            this.logLevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logLevelComboBox.FormattingEnabled = true;
            this.logLevelComboBox.Location = new System.Drawing.Point(380, 1);
            this.logLevelComboBox.Name = "logLevelComboBox";
            this.logLevelComboBox.Size = new System.Drawing.Size(59, 20);
            this.logLevelComboBox.TabIndex = 2;
            this.logLevelComboBox.SelectedIndexChanged += new System.EventHandler(this.logLevelComboBox_SelectedIndexChanged);
            // 
            // fileListBox
            // 
            this.fileListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.Location = new System.Drawing.Point(82, 1);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.Size = new System.Drawing.Size(220, 20);
            this.fileListBox.TabIndex = 1;
            this.fileListBox.SelectedIndexChanged += new System.EventHandler(this.fileListBox_SelectedIndexChanged);
            // 
            // Time
            // 
            this.Time.AutoSize = true;
            this.Time.Location = new System.Drawing.Point(3, 7);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(77, 12);
            this.Time.TabIndex = 0;
            this.Time.Text = "Create Time:";
            // 
            // logFindTextBox
            // 
            this.logFindTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logFindTextBox.Location = new System.Drawing.Point(3, 594);
            this.logFindTextBox.Name = "logFindTextBox";
            this.logFindTextBox.Size = new System.Drawing.Size(1188, 21);
            this.logFindTextBox.TabIndex = 3;
            this.logFindTextBox.TextChanged += new System.EventHandler(this.logFindTextBox_TextChanged);
            this.logFindTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.logFindTextBox_KeyDown);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.aboutTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1200, 624);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "About";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // aboutTextBox
            // 
            this.aboutTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aboutTextBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.aboutTextBox.Location = new System.Drawing.Point(3, 3);
            this.aboutTextBox.Name = "aboutTextBox";
            this.aboutTextBox.Size = new System.Drawing.Size(1194, 618);
            this.aboutTextBox.TabIndex = 0;
            this.aboutTextBox.Text = "";
            // 
            // enableCheckToolTip
            // 
            this.enableCheckToolTip.AutomaticDelay = 200;
            // 
            // cellontextMenuStrip
            // 
            this.cellontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRuleToolStripMenuItem,
            this.editRuleToolStripMenuItem,
            this.deleteRuleToolStripMenuItem});
            this.cellontextMenuStrip.Name = "cellontextMenuStrip";
            this.cellontextMenuStrip.Size = new System.Drawing.Size(143, 70);
            // 
            // addRuleToolStripMenuItem
            // 
            this.addRuleToolStripMenuItem.Name = "addRuleToolStripMenuItem";
            this.addRuleToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.addRuleToolStripMenuItem.Text = "Add Rule";
            this.addRuleToolStripMenuItem.Click += new System.EventHandler(this.addRuleToolStripMenuItem_Click);
            // 
            // editRuleToolStripMenuItem
            // 
            this.editRuleToolStripMenuItem.Name = "editRuleToolStripMenuItem";
            this.editRuleToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.editRuleToolStripMenuItem.Text = "Edit Rule";
            this.editRuleToolStripMenuItem.Click += new System.EventHandler(this.editRuleToolStripMenuItem_Click);
            // 
            // deleteRuleToolStripMenuItem
            // 
            this.deleteRuleToolStripMenuItem.Name = "deleteRuleToolStripMenuItem";
            this.deleteRuleToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.deleteRuleToolStripMenuItem.Text = "Delete Rule";
            this.deleteRuleToolStripMenuItem.Click += new System.EventHandler(this.deleteRuleToolStripMenuItem_Click);
            // 
            // ConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tabControl);
            this.Name = "ConfigControl";
            this.Size = new System.Drawing.Size(1208, 650);
            this.Load += new System.EventHandler(this.ConfigControl_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.logTableLayoutPanel.ResumeLayout(false);
            this.logTableLayoutPanel.PerformLayout();
            this.logContextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.cellontextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox enableCheckBox;
        private System.Windows.Forms.ToolTip enableCheckToolTip;
        private System.Windows.Forms.ContextMenuStrip cellontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editRuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRuleToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.TableLayoutPanel logTableLayoutPanel;
        private System.Windows.Forms.ToolStripMenuItem addRuleToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Enabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rule;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox pageListBox;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.Label labelEnableAll;
        private System.Windows.Forms.TextBox logFindTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox fileListBox;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox logLevelComboBox;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ContextMenuStrip logContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showJSONItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox aboutTextBox;

    }
}
