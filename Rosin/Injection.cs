using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

using Fiddler;

using Rosin.Config;
using Rosin.Item;
using Rosin.Manager;
using Rosin.Util;

namespace Rosin
{
    /**
     * 脚本注入模块，包括以下功能
     * 1、匹配html请求是否符合用户配置的规则，支持host、path、正则匹配
     * 2、向符合规则的html请求响应中插入script代码
     * 3、提供增、删、改、查、启用、禁用规则接口供外部调用
     * 
     * Date: 2014/11/12
     * */

    public class Injection
    {
        public const string TYPE_HOST = "host";
        public const string TYPE_PATH = "path";
        public const string TYPE_REGEX = "regex";

        //TODO 未来持久化
        public bool bGlobalEnabled = true; // 全局设置的是否打印日志，默认开启，且不永久保存该设置

        string sScriptText = "";

        public Injection()
        {
            FileStream aFile = new FileStream(FiddlerPath.ScriptFilePath, FileMode.Open);
            StreamReader sReader = new StreamReader(aFile);
            
            sScriptText = sReader.ReadToEnd();

            sReader.Close();
            sReader = null;
            aFile = null;
        }

        public void FilterAndDisCache(Session oSession)
        {
            if (bGlobalEnabled)
            {
                // request url is match the user's config rules
                if (this.MatchRule(oSession))
                {
                    oSession.oRequest.headers["If-Modified-Since"] = "0";
                }
            }
        }

        public void FilterAndInject(Session oSession)
        {
            Debug.Log("FilterAndInject: MatchRule check!" + oSession.fullUrl);
            // response content type is text/html
            if (bGlobalEnabled && oSession.oResponse.headers.ExistsAndContains("Content-Type", "text/html"))
            {
                Debug.Log("FilterAndInject: MatchRule check!");
                // request url is match the user's config rules
                if(this.MatchRule(oSession))
                {
                    oSession.utilDecodeResponse();
                    oSession.utilReplaceOnceInResponse(@"<head>", @"<head><script>" + sScriptText + "</script>", false);

                    // script tag add crossorigin 
                    oSession.utilReplaceInResponse(@"<script", @"<script crossorigin ");

                    oSession.oResponse.headers["Cache-Control"] = "no-cache";
                    oSession.oResponse.headers["Content-Length"] = oSession.responseBodyBytes.Length.ToString();
                }
            }

           // javascript request, add cross domain header
           if (oSession.fullUrl.Contains(".js"))
           {
               if (oSession.oResponse.headers["Access-Control-Allow-Origin"] == "")
               {
                   oSession.oResponse.headers["Access-Control-Allow-Origin"] = "*";
               }
           }
        }

        #region injection rule manager module

        private bool MatchRule(Session oSession)
        {
            return InjectionRuleManager.Instance().IsMatch(oSession);
        }

        public string addRule(string type, string match)
        {
            return InjectionRuleManager.Instance().AddRule(type, match);
        }

        public void delRule(string order)
        {
            InjectionRuleManager.Instance().DelRule(order);
        }

        public void modifyRule(string order, string type, string match)
        {
            InjectionRuleManager.Instance().ModifyRule(order, type, match);
        }

        public List<RuleItem> queryRule()
        {
            return InjectionRuleManager.Instance().RuleList;
        }

        public void enableRule(string order)
        {
            InjectionRuleManager.Instance().EnableRule(order);
        }

        public void disableRule(string order)
        {
            InjectionRuleManager.Instance().DisableRule(order);
        }

        #endregion
    }
}
