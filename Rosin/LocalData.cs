using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Fiddler;
using Rosin.Config;

namespace Rosin
{
    /**
     * 获取本地日志数据
     * 
     * Date: 2014/11/13
     * */
    public class LocalData
    {
        private string sKey = "";

        private FileStream sFile = null;
        private StreamReader sr = null;

        public LocalData()
        {

        }

        private void InitStream()
        {
            if (sr != null)
            {
                sr.Close();
                sFile = null;
                sr = null;
            }

            string sFilePath = FiddlerPath.RosinLogDir + @"\" + this.sKey + ".txt";

            this.Log("create stream, key is " + this.sKey + ", path is " + sFilePath);

            if (File.Exists(sFilePath))
            {
                sFile = new FileStream(sFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                sr = new StreamReader(sFile);
            }

        }

        public void SetKey(string key)
        {
            this.sKey = key;
            this.InitStream();
        }

        public string GetAll()
        {
            if (this.sr != null)
            {
                return this.sr.ReadToEnd();
            }
            else
            {
                return "";
            }
        }

        public string GetNew()
        {
            string returnText = "";
            string lineText = "";

            if (this.sr != null)
            {
                lineText = this.sr.ReadLine();
                
                while ( lineText != null)
                {
                    returnText += lineText + "\r\n";

                    lineText = this.sr.ReadLine();
                }
            }

            return returnText;
        }

        public void Clear()
        {
            if (sr != null)
            {
                sr.Close();
                sr = null;
            }

            if (sFile != null)
            {
                sFile.Close();
                sFile = null;
            }
        }

        public void Log(string text)
        {
            FiddlerApplication.Log.LogString(">>>>Rosin Log: " + text);
        }
    }
}
