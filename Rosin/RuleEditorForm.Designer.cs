namespace Rosin
{
    partial class RuleEditorForm
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
            this.matchLabel = new System.Windows.Forms.Label();
            this.ruleTextBox = new System.Windows.Forms.TextBox();
            this.editorOkButton = new System.Windows.Forms.Button();
            this.editorCancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.labelRuleTip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // matchLabel
            // 
            this.matchLabel.AutoSize = true;
            this.matchLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.matchLabel.Location = new System.Drawing.Point(26, 50);
            this.matchLabel.Name = "matchLabel";
            this.matchLabel.Size = new System.Drawing.Size(35, 12);
            this.matchLabel.TabIndex = 0;
            this.matchLabel.Text = "Rule:";
            // 
            // ruleTextBox
            // 
            this.ruleTextBox.Location = new System.Drawing.Point(74, 48);
            this.ruleTextBox.Name = "ruleTextBox";
            this.ruleTextBox.Size = new System.Drawing.Size(381, 21);
            this.ruleTextBox.TabIndex = 1;
            this.ruleTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ruleTextBox_KeyDown);
            // 
            // editorOkButton
            // 
            this.editorOkButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.editorOkButton.Location = new System.Drawing.Point(277, 75);
            this.editorOkButton.Name = "editorOkButton";
            this.editorOkButton.Size = new System.Drawing.Size(75, 23);
            this.editorOkButton.TabIndex = 2;
            this.editorOkButton.Text = "OK";
            this.editorOkButton.UseVisualStyleBackColor = true;
            this.editorOkButton.Click += new System.EventHandler(this.editorOkButton_Click);
            // 
            // editorCancelButton
            // 
            this.editorCancelButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.editorCancelButton.Location = new System.Drawing.Point(370, 75);
            this.editorCancelButton.Name = "editorCancelButton";
            this.editorCancelButton.Size = new System.Drawing.Size(75, 23);
            this.editorCancelButton.TabIndex = 3;
            this.editorCancelButton.Text = "Cancel";
            this.editorCancelButton.UseVisualStyleBackColor = true;
            this.editorCancelButton.Click += new System.EventHandler(this.editorCancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(27, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Type:";
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "Host",
            "Path",
            "Regex"});
            this.typeComboBox.Location = new System.Drawing.Point(74, 16);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(79, 20);
            this.typeComboBox.TabIndex = 5;
            this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.typeComboBox_SelectedIndexChanged);
            // 
            // labelRuleTip
            // 
            this.labelRuleTip.AutoSize = true;
            this.labelRuleTip.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelRuleTip.Location = new System.Drawing.Point(160, 19);
            this.labelRuleTip.Name = "labelRuleTip";
            this.labelRuleTip.Size = new System.Drawing.Size(0, 12);
            this.labelRuleTip.TabIndex = 6;
            // 
            // RuleEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 110);
            this.Controls.Add(this.labelRuleTip);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editorCancelButton);
            this.Controls.Add(this.editorOkButton);
            this.Controls.Add(this.ruleTextBox);
            this.Controls.Add(this.matchLabel);
            this.Name = "RuleEditorForm";
            this.Text = "Rule Editor";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RuleEditorForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label matchLabel;
        private System.Windows.Forms.TextBox ruleTextBox;
        private System.Windows.Forms.Button editorOkButton;
        private System.Windows.Forms.Button editorCancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Label labelRuleTip;
    }
}