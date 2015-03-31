using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Rosin
{
    public partial class RuleEditorForm : Form
    {
        private ConfigControl myOwner = null;
        public RuleEditorForm(ConfigControl oOwner)
        {
            myOwner = oOwner;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        /**
         * 确认按钮
         */
        private void editorOkButton_Click(object sender, EventArgs e)
        {
            String rule = this.ruleTextBox.Text;
            String type = this.typeComboBox.SelectedItem.ToString();

            if(type == null || "".Equals(type))
            {
                MessageBox.Show("Please select the type");
                return;
            }

            int editIndex = this.myOwner.getEditIndex();
            if(editIndex  == -1)
            {
                //数据添加到grid里
                myOwner.addItem(type, rule);
            }
            else
            {
                //数据修改
                myOwner.editItem(editIndex, type, rule);
            }

            this.Close();
        }

        /**
         * 取消按钮
         */
        private void editorCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetRuleSelectTip()
        {
            string type = this.typeComboBox.SelectedItem.ToString();
            string tip = "";

            // 先写在这里吧，后面弄成databing的形式
            switch (type)
            { 
                case "Host":
                    tip = "域名匹配，如 qq.com";
                    break;
                case "Path":
                    tip = "路径或具体页面匹配，如 http://web.p.qq.com/coupon/";
                    break;
                case "Regex":
                    tip = @"正则表达式匹配，如 ^http:\/\/web\.p\.qq\.com";
                    break;
            }

            this.labelRuleTip.Text = tip;
        }

        public void setRuleText(String rule)
        {
            this.ruleTextBox.Text = rule;
        }

        public void setType(String type)
        {
            int index = 0;

            for (int i = 0; i < this.typeComboBox.Items.Count; i++)
            {
                if(this.typeComboBox.Items[i].ToString().Equals(type))
                {
                    index = i;
                }
            }
            this.typeComboBox.SelectedIndex = index;
        }

        private void RuleEditorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.editorOkButton_Click(sender, e);
            }
        }

        private void ruleTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.editorOkButton_Click(sender, e);
            }
        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetRuleSelectTip();
        }

    }
}
