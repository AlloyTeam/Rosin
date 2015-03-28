using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Fiddler;
using Rosin.Config;
using Rosin.Item;
using Rosin.Manager;
using Rosin.Util;
using Rosin.Event;

namespace Rosin
{
    /**
     * 日志拦截，包括以下功能：
     * 1、拦截约定domain的日志http请求，解析请求数据并存储在rosin目录下，同时隐藏该次请求
     * 
     * Date: 2014/11/11
     * */
    public class Interceptor
    {
        // 写日志的事件
        public delegate void RosinWrite_EventHandler(object serder, EventArgs e);
        public delegate void RosinCreate_EventHandler(object serder, EventArgs e);
        public event RosinWrite_EventHandler RosinWrite;
        public event RosinCreate_EventHandler RosinCreate;

        public Interceptor()
        {
            
        }

        private string GetRealUrl(string url)
        {
            int searchIndex = url.IndexOf("?");
            if (searchIndex > 0)
            {
                url = url.Remove(searchIndex);
            }
            return url;
        }

        private void RecordPageUrl(string pageID, Session oSession)
        {
            string Key = pageID;
            string Url = this.GetRealUrl(oSession.oRequest.headers["Referer"]);
            string CreateDate = TimeFormat.GetTimeStamp();

            InjectionListManager.Instance().AddRecord(Url, Key, CreateDate);

            // dispath create new html log event
            RosinCreate(this, new EventArgs());
        }


        public void FilterAndRecord(Session oSession)
        {
            if (oSession.host.ToLower() == Global.sRosinDomain)
            {
                string sRequestBodyString = oSession.GetRequestBodyAsString();

                if(sRequestBodyString != "")
                {
                    List<LogItem> logList = JsonConvert.DeserializeObject<List<LogItem>>(sRequestBodyString);

                    if (logList.Count > 0)
                    {
                        bool isNew = false;
                        string sFileName = logList[0].key + ".txt";
                        string sFileDir = FiddlerPath.RosinLogDir + @"\" + sFileName;
                        string sContent = "";

                        if (!File.Exists(sFileDir))
                        {
                            isNew = true;
                        }

                        if (isNew)
                        {
                            sContent += "Page URL: " + oSession.oRequest.headers["Referer"] + "\r\n";
                            sContent += "Create Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
                            sContent += "\r\n";
                        }

                        foreach (LogItem item in logList)
                        {
                            sContent += "[" + TimeFormat.GetTime(item.time).ToString("yyyy-MM-dd HH:mm:ss") + "] [" + item.level + "]" + item.content.ToString() + "\r\n";
                        }

                        FileStreamManager.Instance().Write(logList[0].key, sFileDir, sContent);

                        // 先写日志，在去记录，避免出现读数据空的情况
                        if (isNew)
                        {
                            this.RecordPageUrl(logList[0].key, oSession);
                        }
                        
                        // dispatch event
                        RosinWrite(this, new EventArgs());
                    }
                }

                oSession["x-replywithfile"] = "rosinpost.dat";
                oSession["ui-hide"] = "true";

                // 这个接口在低版本没有，会报错
                // Fiddler Web Debugger (v2.4.5.0) Built: 2013年8月15日
                // oSession.Ignore(); // 忽略该次请求，fiddler中将看不到这个请求，但是实际上该次请求还是发出去了，所以要自己模拟一个响应
                return;
            }
        }
    }
}
