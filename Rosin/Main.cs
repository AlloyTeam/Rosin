using Fiddler;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Rosin.Manager;
using Rosin.Util;

namespace Rosin
{
    /**
     * 入口类
     * 初始化环境、以及实例化各个功能模块
     * 
     * Date: 2014/11/13
     * */
    public class Main : IAutoTamper
    {
        Injection iInjection = null;
        Interceptor iInterceptor = null;
        LocalData iLocalData = null;
        
        /**
         * UI
         * */
        ConfigControl oConfigControl = null;


        public Main()
        {
            Config.FiddlerPath.InitPath();

            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            Debug.Log("Rosin running...");

            // 初始化相关文件路径
            if (!Directory.Exists(Config.FiddlerPath.RosinDir))
            {
                Directory.CreateDirectory(Config.FiddlerPath.RosinDir);
            }
            if (!Directory.Exists(Config.FiddlerPath.RosinLogDir))
            {
                Directory.CreateDirectory(Config.FiddlerPath.RosinLogDir);
            }

            if (!File.Exists(CONFIG.GetPath("Responses")))
            {
                string header = "HTTP/1.1 200 OK\r\nContent-Type: application/x-javascript\r\nConnection: close\r\nContent-Length: 0\r\n";
                header += "Access-Control-Allow-Credentials: true\r\nAccess-Control-Allow-Methods: GET,POST,OPTIONS\r\nAccess-Control-Allow-Origin:*\r\n\r\n";

                byte[] arrHeaders = System.Text.Encoding.ASCII.GetBytes(header);

                FileStream oFS = File.Create(CONFIG.GetPath("Responses") + "rosinpost.dat");
                oFS.Write(arrHeaders, 0, arrHeaders.Length);
                oFS.Close();
            }

            this.iInjection = new Injection(); // 实例化注入模块
            this.iInterceptor = new Interceptor(); // 实例化拦截模块
            this.iLocalData = new LocalData(); // 实例化本地日志存储模块

            this.oConfigControl = new ConfigControl(iInjection, iInterceptor, iLocalData);
        }

        private string GetFiddlerDir()
        {
            string fiddlerInstallPath = "";
            string fiddlerInstallDir = "";

            try
            {
                string softName = "Fiddler";
                string strKeyName = String.Empty;
                string softPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\";

                RegistryKey regKey = Registry.LocalMachine;
                RegistryKey regSubKey = regKey.OpenSubKey(softPath + softName + ".exe", false);

                object objResult = regSubKey.GetValue(strKeyName);

                RegistryValueKind regValueKind = regSubKey.GetValueKind(strKeyName);

                if (regValueKind == Microsoft.Win32.RegistryValueKind.String)
                {
                    fiddlerInstallPath = objResult.ToString();
                }

            }
            catch
            {
                fiddlerInstallPath = "";
            }

            if (fiddlerInstallPath != "")
            {
                fiddlerInstallPath = fiddlerInstallPath.Replace("\"", "");
                fiddlerInstallDir = Path.GetDirectoryName(fiddlerInstallPath);
            }

            return fiddlerInstallDir;
        }

        public void OnLoad()
        {
            //插件标签页
            TabPage oPage = new TabPage("Rosin");
            //TODO 图标再说
            oPage.ImageIndex = (int)Fiddler.SessionIcons.RPC;
            oPage.Controls.Add(this.oConfigControl);
            this.oConfigControl.Dock = DockStyle.Fill;
            //FiddlerApplication.UI.imglSessionIcons.Images.Add();


            int size = FiddlerApplication.UI.tabsViews.TabPages.Count;
            FiddlerApplication.UI.tabsViews.TabPages.Insert(size, oPage);
        }

        public void OnBeforeUnload()
        {
            InjectionRuleManager.Instance().SaveRule();
            InjectionListManager.Instance().SaveRecord();
        }

        public void AutoTamperRequestBefore(Session oSession) 
        {
            this.iInjection.FilterAndDisCache(oSession);
            this.iInterceptor.FilterAndRecord(oSession);
        }

        public void AutoTamperRequestAfter(Session oSession) { }

        public void AutoTamperResponseBefore(Session oSession)
        {
            this.iInjection.FilterAndInject(oSession);
        }

        public void AutoTamperResponseAfter(Session oSession) { }

        public void OnBeforeReturningError(Session oSession) { }

        public void Log(string text)
        {
            FiddlerApplication.Log.LogString(text);
        }
    }
}
