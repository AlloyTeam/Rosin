using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Rosin.Manager
{
    class LogDataManager
    {
        private static LogDataManager iLogDataManager = null;

        static public LogDataManager Instance()
        {
            if (iLogDataManager == null)
            {
                iLogDataManager = new LogDataManager();
            }
            return iLogDataManager;
        }

        public LogDataManager()
        {
            if (iLogDataManager != null)
            {
                throw new ArgumentException();
            }
        }

        private string sKey = "";

        private FileStream sFile = null;
        private StreamReader sr = null;

        private void InitStream()
        {
            this.Clear();

            string sFilePath = Config.FiddlerPath.RosinLogDir + @"\" + this.sKey + ".txt";

            if (File.Exists(sFilePath))
            {
                this.sFile = new FileStream(sFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                this.sr = new StreamReader(sFile);
            }
        }

        public void SetKey(string key)
        {
            this.sKey = key;
            this.InitStream();
        }

        public string GetKey()
        {
            return this.sKey;
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

        /**
         * 关闭清空当前的日志文件读取连接进程
         * */
        public void Clear()
        {
            if (this.sr != null)
            {
                this.sr.Close();
                this.sr = null;
            }

            if (this.sFile != null)
            {
                this.sFile.Close();
                this.sFile = null;
            }
        }
    }
}
