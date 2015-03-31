using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;

using Fiddler;

using Rosin.Config;
using Rosin.Item;

namespace Rosin.Manager
{
    /**
     * 管理页面匹配规则文件，由本类统一管理
     * Fiddler启动时读取xml内容并创建规则list，Fiddler关闭时将规则list重新生成xml保存
     * */
    class InjectionRuleManager
    {
        static private InjectionRuleManager iInjectionRuleInstance = null;

        static public InjectionRuleManager Instance()
        {
            if (iInjectionRuleInstance == null)
            {
                iInjectionRuleInstance = new InjectionRuleManager();
            }

            return iInjectionRuleInstance;
        }

        public List<RuleItem> RuleList = null;

        public InjectionRuleManager()
        {
            if (iInjectionRuleInstance != null)
            {
                throw new ArgumentException();
            }

            this.LoadRuleList();
        }

        private void LoadRuleList()
        {
            this.RuleList = new List<RuleItem>();

            XmlDocument document = new XmlDocument();
            document.Load(FiddlerPath.RuleFilePath);

            XmlNodeList ruleNodes = document.DocumentElement.SelectNodes("//InjectionRules/Rule");

            RuleItem rule = null;

            for (int i = 0, l = ruleNodes.Count; i < l; i++ )
            {
                rule = new RuleItem();
                rule.Type = ruleNodes[i].Attributes["Type"].Value;
                rule.Match = ruleNodes[i].Attributes["Match"].Value;
                rule.Enabled = ruleNodes[i].Attributes["Enabled"].Value;
                rule.Order = ruleNodes[i].Attributes["Order"].Value;

                this.RuleList.Add(rule);
            }
        }

        private RuleItem FindRuleByOrder(string order)
        {
            RuleItem result = null;

            foreach (RuleItem item in this.RuleList)
            {
                if (item.Order == order)
                {
                    result = item;
                    break;
                }
            }

            return result;
        }

        private int FindIndexByOrder(string order)
        {
            int result = -1;

            for (int i = 0, l = this.RuleList.Count; i < l; i++ )
            {
                if (this.RuleList[i].Order == order)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public bool IsMatch(Session oSession)
        {
            bool isMatch = false;

            if (this.RuleList != null && this.RuleList.Count > 0)
            {
                string sRuleType = "";
                string sRuleMatch = "";
                string sRuleEnabled = "";

                foreach (RuleItem rule in this.RuleList)
                {
                    sRuleType = rule.Type;
                    sRuleMatch = rule.Match;
                    sRuleEnabled = rule.Enabled;

                    if (sRuleEnabled == "true")
                    {
                        if (sRuleType == Injection.TYPE_HOST)
                        {
                            if (oSession.host == sRuleMatch)
                            {
                                isMatch = true;
                                break;
                            }
                        }
                        else if (sRuleType == Injection.TYPE_PATH)
                        {
                            if (oSession.fullUrl.StartsWith(sRuleMatch))
                            {
                                isMatch = true;
                                break;
                            }
                        }
                        else if (sRuleType == Injection.TYPE_REGEX)
                        {
                            Regex regex = new Regex(sRuleMatch);
                            if (regex.IsMatch(oSession.fullUrl))
                            {
                                isMatch = true;
                                break;
                            }
                        }
                    }
                }

            }

            return isMatch;
        }

        public string AddRule(string type, string match)
        {
            int newOrder = 1;

            if (this.RuleList.Count > 0)
            {
                newOrder = int.Parse(this.RuleList[this.RuleList.Count - 1].Order) + 1;
            }

            RuleItem newRule = new RuleItem();
            newRule.Type = type;
            newRule.Match = match;
            newRule.Order = newOrder.ToString();
            newRule.Enabled = "true";

            this.RuleList.Add(newRule);

            return newRule.Order;
        }

        public void DelRule(string order)
        {
            int index = this.FindIndexByOrder(order);

            if (index >= 0)
            {
                this.RuleList.RemoveAt(index);
            }
        }

        public void ModifyRule(string order, string type, string match)
        {
            RuleItem rule = this.FindRuleByOrder(order);

            if (rule != null)
            {
                rule.Type = type;
                rule.Match = match;
            }
        }

        public void DisableRule(string order)
        {
            RuleItem rule = this.FindRuleByOrder(order);

            if (rule != null)
            {
                rule.Enabled = "false";
            }
        }

        public void EnableRule(string order)
        {
            RuleItem rule = this.FindRuleByOrder(order);

            if (rule != null)
            {
                rule.Enabled = "true";
            }
        }

        public void SaveRule()
        {
            XmlDocument document = new XmlDocument();
            document.Load(FiddlerPath.RuleFilePath);

            XmlElement root = document.DocumentElement;
            XmlElement newRuleNode = null;

            root.FirstChild.RemoveAll();

            for (int i = 0, l = this.RuleList.Count; i < l; i++)
            {
                newRuleNode = document.CreateElement("Rule");
                newRuleNode.SetAttribute("Enabled", this.RuleList[i].Enabled);
                newRuleNode.SetAttribute("Type", this.RuleList[i].Type);
                newRuleNode.SetAttribute("Order", this.RuleList[i].Order);
                newRuleNode.SetAttribute("Match", this.RuleList[i].Match);

                root.FirstChild.AppendChild(newRuleNode);
            }

            document.Save(FiddlerPath.RuleFilePath);
        }
    }
}
