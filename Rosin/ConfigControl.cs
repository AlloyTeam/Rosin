using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Fiddler;
using System.IO;
using EPocalipse.Json.Viewer;

using Rosin.Item;
using Rosin.Manager;
using System.Text.RegularExpressions;
using Rosin.Config;
using System.Collections;
using Rosin.Util;

namespace Rosin
{
    /**
     * Rosin UI逻辑全都在这个类
     * 名字起的不好。。。
     */
    public partial class ConfigControl : UserControl
    {
        //匹配规则弹窗
        private RuleEditorForm oRuleEditorForm = null;
        //JsonView弹窗
        private JsonViewForm oJsonViewForm = null;
        private Injection oInjection = null;
        private Interceptor oInterceptor = null;
        private LocalData oLocalData = null;

        //当前在编辑的行，-1表示新增
        private int iEditIndex = -1;

        //当前查找的起始位置
        private int iFindStart = 0;
        //查找到的总数
        private int iFindSum = 0;

        //Rosin是否启用
        internal bool bEnabled = false;

        // 当前日志输出面板选择的页面的key
        private string sCurrentPageKey = "";

        // 日志页面列表BindingSource
        private BindingSource pageListBindingSource = new BindingSource();
        // 日志列表BindingSource
        private BindingSource fileListBindingSource = new BindingSource();
        // 日志级别列表BindingSource
        private BindingSource logLevelBindingSource = new BindingSource();

        //日志级别定义
        private string[] logLevel =new string[]{"ALL", "DEBUG", "LOG", "INFO", "WARN", "ERROR"};

        //日志文件
        private LogFileItem logFileItem = new LogFileItem();

        private string selectedJSONStr = String.Empty;

        public ConfigControl(Injection oInjection, Interceptor oInterceptor, LocalData oLocalData)
        {
            this.oInjection = oInjection;
            this.oInterceptor = oInterceptor;
            this.oLocalData = oLocalData;
            InitializeComponent();

            // 绑定自定义事件
            this.oInterceptor.RosinWrite += RosinWriteHandler; // 写日志事件
            this.oInterceptor.RosinCreate += RosinCreateHandler; // 创建日志事件

            // 初始化日志页面列表
            this.InitPageList();
        }

        public void OnBeforeUnload()
        {
            if (null != oRuleEditorForm)
            {
                oRuleEditorForm.Close();
            }

            if (null != oJsonViewForm)
            {
                oJsonViewForm.Close();
            }
        }

        #region 自定义事件
        private void RosinWriteHandler(object serder, EventArgs e)
        {
            this.UpdateLogText();
        }

        private void RosinCreateHandler(object serder, EventArgs e)
        {
            // 刷新页面列表
            pageListBindingSource.ResetBindings(true);
            fileListBindingSource.ResetBindings(true);
            logLevelBindingSource.ResetBindings(true);

            this.UpdateFileList();
        }
        #endregion

        #region 配置按钮
        private void configButton_Click(object sender, EventArgs e)
        {
            this.iEditIndex = -1;

            if(this.oRuleEditorForm == null || this.oRuleEditorForm.IsDisposed)
            {
                this.oRuleEditorForm = new RuleEditorForm(this);
            }

            this.oRuleEditorForm.setType("Host");
            this.oRuleEditorForm.Show();
        }
        #endregion

        #region 全局Enable
        private void enableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.bEnabled = this.enableCheckBox.Checked;
            //TODO 关闭后图标熄灭

            this.oInjection.bGlobalEnabled = this.bEnabled;
        }

        private void labelEnableAll_Click(object sender, EventArgs e)
        {
            this.enableCheckBox.Checked = !this.enableCheckBox.Checked;

            this.bEnabled = this.enableCheckBox.Checked;
            //TODO 关闭后图标熄灭

            this.oInjection.bGlobalEnabled = this.bEnabled;
        }
        #endregion

        #region 表格相关

        /**
         * 控件Load时就去读列表数据
         */
        private void ConfigControl_Load(object sender, EventArgs e)
        {
            List<RuleItem> ruleList = this.oInjection.queryRule();

            this.dataGridView.Rows.Clear();

            foreach (var oRule in ruleList)
            {
                DataGridViewRow dgvr = new DataGridViewRow();
                dgvr.CreateCells(this.dataGridView);
                dgvr.Cells[0].Value = Convert.ToBoolean(oRule.Enabled);
                dgvr.Cells[1].Value = oRule.Type;
                dgvr.Cells[2].Value = oRule.Match;
                dgvr.Cells[3].Value = oRule.Order;
                this.dataGridView.Rows.Add(dgvr);
            }
        }

        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            
             Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                this.dataGridView.RowHeadersWidth - 4,
                e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), 
                this.dataGridView.RowHeadersDefaultCellStyle.Font,
                rectangle,
                this.dataGridView.RowHeadersDefaultCellStyle.ForeColor, 
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
             
        }

        /**
         * 双击单元格弹出编辑窗口
         */
        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int currentIndex = e.RowIndex;
            if(currentIndex < 0)
            {
                return;
            }

            string sType = this.dataGridView.Rows[currentIndex].Cells[1].Value.ToString();
            string sVal = this.dataGridView.Rows[currentIndex].Cells[2].Value.ToString();
            this.iEditIndex = currentIndex;

            if (null == this.oRuleEditorForm || this.oRuleEditorForm.IsDisposed)
            {
                this.oRuleEditorForm = new RuleEditorForm(this);
            }

            this.oRuleEditorForm.setType(sType);
            this.oRuleEditorForm.setRuleText(sVal);

            oRuleEditorForm.Show();
        }

        /**
         * 右击单元格显示菜单
         */
        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex > -1 && e.RowIndex < (sender as DataGridView).Rows.Count)
                {
                    if (e.ColumnIndex > -1 && e.ColumnIndex < (sender as DataGridView).Rows[e.RowIndex].Cells.Count)
                    {
                        DataGridViewCell clickedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];
                        this.dataGridView.CurrentCell = clickedCell;
                        var relativeMousePosition = dataGridView.PointToClient(Cursor.Position);
                        this.cellontextMenuStrip.Show(dataGridView, relativeMousePosition);
                    }
                }
            }
        }

        /**
         * 单元格右键添加Rule
         */
        private void addRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iCurrentIndex = this.dataGridView.CurrentCell.RowIndex;
            if(iCurrentIndex < 0)
            {
                return;
            }

            String sType = this.dataGridView.Rows[iCurrentIndex].Cells[1].Value.ToString();
            String sRule = this.dataGridView.Rows[iCurrentIndex].Cells[2].Value.ToString();
            this.iEditIndex = -1;

            if (null == this.oRuleEditorForm || this.oRuleEditorForm.IsDisposed)
            {
                this.oRuleEditorForm = new RuleEditorForm(this);
            }

            this.oRuleEditorForm.setType(sType);
            this.oRuleEditorForm.setRuleText(sRule);

            oRuleEditorForm.Show();
        }

        /**
         * 单元格右键修改Rule
         */
        private void editRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iCurrentIndex = this.dataGridView.CurrentCell.RowIndex;
            String sType = this.dataGridView.Rows[this.dataGridView.CurrentCell.RowIndex].Cells[1].Value.ToString();
            String sVal = this.dataGridView.Rows[this.dataGridView.CurrentCell.RowIndex].Cells[2].Value.ToString();
            this.iEditIndex = iCurrentIndex;

            if (null == this.oRuleEditorForm || this.oRuleEditorForm.IsDisposed)
            {
                this.oRuleEditorForm = new RuleEditorForm(this);
            }

            this.oRuleEditorForm.setType(sType);
            this.oRuleEditorForm.setRuleText(sVal);

            oRuleEditorForm.Show();
        }

        /**
         * 单元格右键删除Rule
         */
        private void deleteRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = this.dataGridView.CurrentCell.RowIndex;
            string order = this.dataGridView.Rows[this.dataGridView.CurrentCell.RowIndex].Cells[3].Value.ToString();
            this.dataGridView.Rows.Remove(this.dataGridView.Rows[index]);

            //删除XML的数据
            this.oInjection.delRule(order);
        }

        /**
         * 列表中添加数据 
         */
        public void addItem(string type, string rule)
        {
            //写入XML
            string order = this.oInjection.addRule(type.ToLower(), rule);

            DataGridViewRow dr = new DataGridViewRow();
            dr.CreateCells(this.dataGridView);
            dr.Cells[0].Value = true;
            dr.Cells[1].Value = type;
            dr.Cells[2].Value = rule;
            dr.Cells[3].Value = order;
            this.dataGridView.Rows.Add(dr);
        }

        /**
         * 编辑列表数据
         */
        public void editItem(int index, string type, string rule)
        {
            DataGridViewCellCollection cells = this.dataGridView.Rows[index].Cells;
            cells[1].Value = type;
            cells[2].Value = rule;
            

            //编辑XML
            this.oInjection.modifyRule(cells[3].Value.ToString(), type.ToLower(), rule);
        }

        /**
         * 点击enable事件
         */
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView.CurrentCell.ColumnIndex == 0)
            {
                int currentIndex = this.dataGridView.CurrentCell.RowIndex;
                //获取DataGridView中CheckBox的Cell
                DataGridViewCheckBoxCell dgvCheck = (DataGridViewCheckBoxCell)(this.dataGridView.Rows[currentIndex].Cells[0]);
                DataGridViewRow currentRow = this.dataGridView.Rows[currentIndex];
                string order = currentRow.Cells[3].Value.ToString();

                if (Convert.ToBoolean(dgvCheck.EditedFormattedValue) == false)
                {
                    dgvCheck.Value = true;
                     //编辑XML
                    this.oInjection.enableRule(order);
                }
                else
                {
                    dgvCheck.Value = false;
                    //编辑XML
                    this.oInjection.disableRule(order);
                }
            }
        }

        public int getEditIndex()
        {
            return this.iEditIndex;
        }

        #endregion


        #region 日志输出面板
        private void InitPageList()
        {
            this.pageListBindingSource.DataSource = InjectionListManager.Instance().PageList;
            
            this.pageListBox.DisplayMember = "Url";
            this.pageListBox.ValueMember = "Url";
            this.pageListBox.DataSource = this.pageListBindingSource;

            //日志级别
            this.logLevelBindingSource.DataSource = this.logLevel;
            this.logLevelComboBox.DataSource = this.logLevelBindingSource;

            this.UpdateFileList();
        }

        private void UpdateFileList()
        {
            int index = this.pageListBox.SelectedIndex;

            if (index >= 0 && index < InjectionListManager.Instance().PageList.Count)
            {
                this.fileListBindingSource.DataSource = InjectionListManager.Instance().PageList[index].FileItemList;
                this.fileListBox.DisplayMember = "CreateDateString";
                this.fileListBox.ValueMember = "Key";
                this.fileListBox.DataSource = this.fileListBindingSource;
            }
        }

        /**
         * 加载日志的全部文本
         */
        private void LoadNewLogText()
        {
            int index = this.pageListBox.SelectedIndex;

            if (index < 0 || index >= InjectionListManager.Instance().PageList.Count)
            {
                return;
            }

            int fileIndex = this.fileListBox.SelectedIndex;

            if (fileIndex < 0)
            {
                this.sCurrentPageKey = "";
                return;
            }

            this.sCurrentPageKey = this.fileListBox.SelectedValue.ToString();

            if (this.sCurrentPageKey == "")
            {
                return;
            }

            //初始化数据
            this.logFileItem.Clear();
            this.oLocalData.SetKey(this.sCurrentPageKey);
            this.logFileItem.key = this.sCurrentPageKey;
            string logStr = this.oLocalData.GetAll();

            //将当前显示的日志反序列化到内存中
            DeserializeLog(logStr, false);

            if (this.logLevelComboBox.SelectedValue == null)
            {
                showLog(String.Empty);
            }
            else
            {
                showLog(this.logLevelComboBox.SelectedValue.ToString());
            }
        }

        /**
         * 拼接实时更新的Log内容
         */
        private void UpdateLogText()
        {
            string sFileKey = this.fileListBox.SelectedValue.ToString();
            Debug.Log("1");
            if (sFileKey != this.sCurrentPageKey)
            {
                this.sCurrentPageKey = sFileKey;
                this.LoadNewLogText();
                return;
            }
            Debug.Log("2");
            string logText = this.oLocalData.GetNew();
            DeserializeLog(logText, true);
            Debug.Log("3");
            if (this.logLevelComboBox.SelectedValue == null)
            {
                showLog(String.Empty);
            }
            else
            {
                showLog(this.logLevelComboBox.SelectedValue.ToString());
            }
            Debug.Log("4");
        }

        /**
         * 解析日志文件
         * 将字符串转化为实体Bean
         * 每行日志是一条数据
         */
        private void DeserializeLog(string logStr, bool isAddText)
        {
            if(logStr == null || "".Equals(logStr))
            {
                return;
            }

            string[] logStrArr = logStr.Split(new string[]{ "\r\n" }, StringSplitOptions.None);

            if(logStrArr == null || logStrArr.Length == 0)
            {
                return;
            }

            Debug.Log("logStrArr.Length: " + logStrArr.Length);

            List<string> logStrList = new List<string>(logStrArr);

            // 非内容追加，即全量加载才做这个过滤
            if (!isAddText)
            {
                this.logFileItem.pageUrl = logStrList[0].ToString();
                this.logFileItem.createDate = logStrList[1].ToString();
                //移除日志头部的URL和创建时间
                logStrList.RemoveAt(0);
                logStrList.RemoveAt(0);
            }

            //正则匹配日志，后向引用选中的文本
            //这个正则写的不够精准（比如时间的匹配）
            //但是够用了，懒得写那么复杂。。
            string pattern = @"^(?<time>\[\d{4}-\d{2}-\d{2}\s(\d{2}:){2}\d{2}\])\s?(?<level>\[\w+\])(?<content>.*)";
            Regex regex = new Regex(pattern);

            foreach(string aLogStr in logStrList)
            {
                if(aLogStr == null || "".Equals(aLogStr) || Environment.NewLine.Equals(aLogStr))
                {
                    continue;
                }

                //this.Log("循环的aLogStr=" + aLogStr);

                LogItem aLogJson = new LogItem();
                Match match = regex.Match(aLogStr);
                string time = match.Groups["time"].ToString();
                string level = match.Groups["level"].ToString();
                string content = match.Groups["content"].ToString();

                if(time != null && !"".Equals(time))
                {
                    //去掉前后中括号
                    time = time.Substring(1, time.Length - 2);
                    aLogJson.time = time;
                }
                if (level != null && !"".Equals(level))
                {
                    //去掉前后中括号
                    level = level.Substring(1, level.Length - 2);
                    aLogJson.level = level;
                }
                if (!String.IsNullOrEmpty(content))
                {
                    aLogJson.content = content;
                }
                //this.Log("aLogJson.ToString: " + aLogJson.ToString());
                this.logFileItem.logList.Add(aLogJson);
            }
        }

        /**
         * UI上展示日志
         */
        private void showLog(string level)
        {
            if(this.logFileItem == null || this.logFileItem.key == null || "".Equals(this.logFileItem.key))
            {
                return;
            }

            this.logRichTextBox.Clear();
            this.logRichTextBox.AppendText(this.logFileItem.pageUrl);
            this.logRichTextBox.AppendText(Environment.NewLine);
            this.logRichTextBox.AppendText(this.logFileItem.createDate);
            this.logRichTextBox.AppendText(Environment.NewLine);
            this.logRichTextBox.AppendText(Environment.NewLine);

            if (this.logFileItem.logList == null || this.logFileItem.logList.Count < 1)
            {
                return;
            }

            //是否根据日志级别过滤显示
            bool isFilter = false;
            level = level.Trim();

            if(level != null && !"".Equals(level) && !"ALL".Equals(level))
            {
                isFilter = true;
            }

            foreach (LogItem aLog in this.logFileItem.logList)
            {
                if (isFilter && !level.Equals(aLog.level))
                {
                    continue;
                }

                this.logRichTextBox.AppendText("[" + aLog.time + "]");
                if ("ERROR".Equals(aLog.level))
                {
                    this.logRichTextBox.AppendText("[" + aLog.level + "]", Color.Red);
                }
                else if ("WARN".Equals(aLog.level))
                {
                    this.logRichTextBox.AppendText("[" + aLog.level + "]", Color.Orange);
                }
                else if ("DEBUG".Equals(aLog.level))
                {
                    this.logRichTextBox.AppendText("[" + aLog.level + "]", Color.Blue);
                }
                else if ("INFO".Equals(aLog.level))
                {
                    this.logRichTextBox.AppendText("[" + aLog.level + "]", Color.Blue);
                }
                else
                {
                    this.logRichTextBox.AppendText("[" + aLog.level + "]");
                }

                //this.Log("aLog.content:" + aLog.content);
                /**
                 * 匹配日志内容的JSONString
                 * 如果有JSON，就替换为Object{}的精简形式在UI上展示
                 * JSONString前后用零宽空格包裹
                 */
                string jsonPattern = @"\uFFFE.*?\uFEFF";
                Regex jsonRegex = new Regex(jsonPattern);
                //需要匹配JSON，并且替换
                //注意有可能有多个JSON
                MatchCollection matchRes = jsonRegex.Matches(aLog.content);

                //如果没有匹配到JSON
                if (matchRes == null || matchRes.Count == 0)
                {
                    this.logRichTextBox.AppendText(" " + aLog.content);
                    this.logRichTextBox.AppendText(Environment.NewLine);
                }
                else
                {
                    List<JsonItem> tmpJsonList = new List<JsonItem>();
                    foreach (Match match in matchRes)
                    {
                        if (!match.Success)
                        {
                            continue;
                        }

                        //把替换的JSON存到list里
                        JsonItem aJsonItem = new JsonItem();
                        aJsonItem.Content = match.ToString();
                        aJsonItem.Content = replaceZeroWidthSpace(aJsonItem.Content);
                        tmpJsonList.Add(aJsonItem);
                    }

                    if (tmpJsonList != null && tmpJsonList.Count > 0)
                    {
                        for (int i = 0; i < tmpJsonList.Count; i++)
                        {
                            tmpJsonList[i].Index = this.logFileItem.jsonList.Count + i;
                            aLog.content = replaceJSON(aLog.content, tmpJsonList[i].Content, tmpJsonList[i].Index);
                        }

                        this.logFileItem.jsonList.AddRange(tmpJsonList);
                    }
                    this.logRichTextBox.AppendText(" " + aLog.content);
                    this.logRichTextBox.AppendText(Environment.NewLine);
                }
            }
        }

        /**
         * 去除零宽空格
         */
        private string replaceZeroWidthSpace(string content)
        {
            if(String.IsNullOrEmpty(content))
            {
                return content;
            }

            return content.Replace("\uFFFE", string.Empty).Replace("\uFEFF", string.Empty);
        }

        /**
         * 如果JSONStr长度过长，就将其截断打点
         */
        private string replaceJSON(string content, string JSONStr, int index)
        {
            if(String.IsNullOrEmpty(content) || String.IsNullOrEmpty(JSONStr))
            {
                return content;
            }

            //在UI上显示的string
            string showJSONStr = String.Empty;
            if(JSONStr.Length > Global.JSON_OBJ_LENGTH)
            {
                showJSONStr = JSONStr.Substring(0, Global.JSON_OBJ_LENGTH);
                if (!showJSONStr.EndsWith("}"))
                {
                    showJSONStr += "...}";
                }
            }
            else
            {
                showJSONStr = content;
            }
            
            showJSONStr = Global.JSON_TAG + encodeJSONIndex(index) + showJSONStr;
            content = content.Replace(JSONStr, showJSONStr);
            return replaceZeroWidthSpace(content);
        }

        /**
         * 将index增加一个基础偏移值，转换为16进制返回
         */
        private string encodeJSONIndex(int index)
        {
            int hexValue = Convert.ToInt32(Global.JSON_INDEX, 16);
            return Convert.ToString((hexValue + index), 16).ToUpper(); ;
        }

        /**
         * 将16进制编码减去一个基础偏移值，返回十进制的index
         */
        private int decodeJSONIndex(string hexStr)
        {
            int decValue = Convert.ToInt32(hexStr, 16);
            int decIndex = Convert.ToInt32(Global.JSON_INDEX, 16);
            return (decValue - decIndex);
        }

        private void pageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateFileList();
            this.LoadNewLogText();
        }

        private void fileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadNewLogText();
        }

        /**
         * 日志文本框鼠标右键事件
         */
        private void logRichTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.logContextMenuStrip.Show(this, new Point(e.X, e.Y));
            }
        }

        /**
         * 日志级别选择
         */
        private void logLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<RuleItem> ruleList = this.oInjection.queryRule();
            //this.Log("this.logLevelComboBox.SelectedText:" + this.logLevelComboBox.SelectedValue.ToString());

            this.showLog(this.logLevelComboBox.SelectedValue.ToString());
        }
        #endregion


        #region 日志搜索
        private void logFindTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                //搜索关键字
                string keyword = (sender as TextBox).Text;

                if (keyword == null || "".Equals(keyword))
                {
                    return;
                }

                int index = this.findText(this.iFindStart, keyword);

                if (this.iFindSum != 0)
                {
                    this.logFindTextBox.BackColor = Color.LightGreen;
                }
                else
                {
                    this.logFindTextBox.BackColor = Color.Pink;
                }
            }
        }

        /**
         * 更改查找到的keyword背景色
         */
        private int findText(int start, string keyword)
        {
            int index = -1;
            if (keyword == null || "".Equals(keyword))
            {
                return index;
            }

            this.logRichTextBox.SelectionBackColor = SystemColors.Control;
            int rBoxTextLen = this.logRichTextBox.Text.Length;

            if(start < rBoxTextLen)
            {
                index = this.logRichTextBox.Find(keyword, start, RichTextBoxFinds.None);
                if (index > -1)
                {
                    this.iFindSum++;
                    this.iFindStart = index + keyword.Length;
                    //this.logRichTextBox.SelectionStart = index;
                    //this.logRichTextBox.SelectionLength = keyword.Length;
                    //this.logRichTextBox.Select(index, keyword.Length);
                    this.logRichTextBox.SelectionBackColor = Color.LightBlue;
                }
            }
            else
            {
                index = this.logRichTextBox.Find(keyword, 0, RichTextBoxFinds.None);
                if (index > -1)
                {
                    this.iFindSum++;
                    this.iFindStart = index + keyword.Length;
                    //this.logRichTextBox.SelectionStart = index;
                    //this.logRichTextBox.SelectionLength = keyword.Length;
                    //this.logRichTextBox.Select(index, keyword.Length);
                    this.logRichTextBox.SelectionBackColor = Color.LightBlue;
                }
            }
            //this.Log("index=" + index.ToString() + " start=" + this.iFindStart + " sum=" + this.iFindSum);
            return index;
        }

        private void logFindTextBox_TextChanged(object sender, EventArgs e)
        {
            //搜索关键字
            string keyword = (sender as TextBox).Text;
            this.iFindStart = 0;
            this.iFindSum = 0;

            //人不猥琐枉少年。。
            this.logRichTextBox.SelectAll();
            this.logRichTextBox.SelectionBackColor = SystemColors.Control;
            this.logRichTextBox.DeselectAll();

            if (keyword == null || "".Equals(keyword))
            {
                this.logFindTextBox.BackColor = SystemColors.Window;

                return;
            }

            int index = this.findText(this.iFindStart, keyword);
            bool hasFound = index > -1 ? true : false;
            if (this.iFindSum != 0)
            {
                this.logFindTextBox.BackColor = Color.LightGreen;
            }
            else
            {
                this.logFindTextBox.BackColor = Color.Pink;
            }
        }
        #endregion


        private void Log(string text)
        {
            FiddlerApplication.Log.LogString(">>>>Rosin Log: " + text);
        }


        #region JSON Viewer
        /**
         * 打开JSON View
         */
        private void showJSONItem_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(this.selectedJSONStr))
            {
                return;
            }

            string hexStr = this.selectedJSONStr.Replace(Global.JSON_TAG, String.Empty);
            int index = decodeJSONIndex(hexStr.Trim());
            JsonItem jsonItem = this.logFileItem.jsonList[index];
            if (jsonItem == null)
            {
                return;
            }

            if (null == this.oJsonViewForm || this.oJsonViewForm.IsDisposed)
            {
                this.oJsonViewForm = new JsonViewForm(jsonItem.Content);
            }

            this.oJsonViewForm.showJSONView(jsonItem.Content);
            this.oJsonViewForm.StartPosition = FormStartPosition.CenterScreen;
            this.oJsonViewForm.Show();
        }
        #endregion

        private void logContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            /**
             * 菜单项是否置灰
             * 只有选中的是JSON才显示菜单项
             */
            this.showJSONItem.Enabled = hasSelectedJSON(this.logRichTextBox.SelectedText);
        }


        /**
         * 判断选定的字符串是否是 ObjectC4A0 的形式
         * 如果是，则是用来占位的JSON标记
         */
        private bool hasSelectedJSON(string selectedStr)
        {
            selectedStr = selectedStr.Trim();
            string pattern = Global.JSON_TAG + @"[\dA-F]{4}";
            Match match = Regex.Match(selectedStr, pattern, RegexOptions.IgnoreCase);
            if(match.Success)
            {
                selectedJSONStr = match.ToString();
                return true;
            }

            return false;
            
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (this.fileListBox.SelectedValue == null)
            {
                return;
            }

            this.sCurrentPageKey = this.fileListBox.SelectedValue.ToString();

            if (this.sCurrentPageKey == "")
            {
                return;
            }

            LogFileManager.ExportFile(this.sCurrentPageKey);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.logFileItem.ClearList();
            this.logRichTextBox.Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (this.sCurrentPageKey == "")
            {
                return;
            }

            this.logRichTextBox.Clear();
            this.oLocalData.Clear();

            InjectionListManager.Instance().DelRecord(this.sCurrentPageKey);

            this.fileListBindingSource.ResetBindings(true);
            this.LoadNewLogText();
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if(this.tabControl.SelectedTab == this.tabPage3)
            {
                this.aboutTextBox.Text = AboutWording.WORDING_1;
            }
        }
    }

    /**
     * 扩展RichTextBox
     */
    public static class RichTextBoxExtention
    {
        /**
         * 添加有色文本
         */
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = text.Length;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        /**
         * 添加自定义文本，支持颜色、加粗、下划线等
         */
        public static void AppendText(this RichTextBox box, string text, Color color, FontStyle fontStyle)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = text.Length;

            box.SelectionColor = color;
            box.SelectionFont = new Font(box.SelectionFont.FontFamily, box.SelectionFont.Size, fontStyle);
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
