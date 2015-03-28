using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Rosin.Util;

namespace Rosin.Item
{
    class FileStreamItem
    {
        public String sKey { get; set; }
        public String sFileDir { get; set; }

        FileStream sFile = null;
        StreamWriter sStream = null;
        Boolean isWriting = false; // 写操作状态
        List<string> contentList = new List<string>();

        private void RealWrite()
        {
            if (this.isWriting)
            {
                return;
            }

            if (this.sFile == null || this.sStream == null)
            {
                this.Create();
            }

            this.isWriting = true;

            while (this.contentList.Count > 0)
            {
                this.sStream.WriteLine(this.contentList[0]);
                Debug.Log("real write: content list count:" + this.contentList.Count);
                Debug.Log("real write: " + this.contentList[0]);

                if (this.contentList.Count > 0)
                {
                    this.contentList.RemoveAt(0);
                }
                else
                {
                    Debug.Log("there must be some bug, why the count less than 0.");
                }
                Debug.Log("real write: end flag!");
                
            }

            
            this.sStream.Close();
            this.Clear();

            this.isWriting = false;
        }

        public void Create()
        {
            this.sFile = new FileStream(this.sFileDir, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            this.sStream = new StreamWriter(this.sFile);
        }

        public void Write(string content)
        {
            // 几乎不会出现的异常，即插入了相同的内容
            // 因为日志内容至少时间上都会不一样，所以最多是由其他异常引发
            Debug.Log("write content:" + content);
            try
            {
                this.contentList.Add(content);
            }
            catch (Exception err)
            {
                Debug.Log("write content error:" + err.ToString());
            }

            this.RealWrite();
        }

        public void Clear() 
        {
            this.sFile = null;
            this.sStream = null;
        }
    }
}
