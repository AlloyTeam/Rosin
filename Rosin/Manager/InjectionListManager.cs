using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;

using Rosin.Config;
using Rosin.Item;
using Rosin.Util;

namespace Rosin.Manager
{
    /**
     * 管理整个记录的injection list列表
     * */
    class InjectionListManager
    {
        static private InjectionListManager iInjectionListInstance = null;

        static public InjectionListManager Instance()
        {
            if (iInjectionListInstance == null)
            {
                iInjectionListInstance = new InjectionListManager();
            }

            return iInjectionListInstance;
        }

        public List<PageInfoItem> PageList = null;

        public InjectionListManager()
        {
            if (iInjectionListInstance != null)
            {
                throw new ArgumentException();
            }

            this.LoadList();
        }

        private void LoadList()
        {
            this.PageList = new List<PageInfoItem>();

            XmlDocument document = new XmlDocument();
            document.Load(FiddlerPath.RecordFilePath);

            XmlElement root = document.DocumentElement;
            XmlNodeList pageNodes = root.SelectNodes("//InjectionLists/Page");

            string currentDate = TimeFormat.GetTimeStamp();

            // 判断页面是否已经有记录
            if (pageNodes != null && pageNodes.Count > 0)
            {
                foreach (XmlNode page in pageNodes)
                {
                    PageInfoItem pageItem = new PageInfoItem();
                    pageItem.Url = page.Attributes["Url"].Value;
                    pageItem.CreateDate = page.Attributes["CreateDate"].Value;

                    XmlNodeList fileNodes = (page as XmlElement).ChildNodes;
                    if (fileNodes != null && fileNodes.Count > 0)
                    {
                        foreach (XmlNode file in fileNodes)
                        {
                            string Key = file.Attributes["Key"].Value;
                            string CreateDate = file.Attributes["CreateDate"].Value;
                            string Url = file.Attributes["Url"].Value;
                            string Order = file.Attributes["Order"].Value;

                            // 自动清理逻辑，每次启动初始化的时候执行
                            // 在有效期内的数据才记录，否则删除本地文件
                            if (Convert.ToUInt64(currentDate)*1000 - Convert.ToUInt64(CreateDate) * 1000 < Global.iExpiresTime)
                            {
                                pageItem.AddFileItem(Key, CreateDate, Order, Key);
                            }
                            else
                            {
                                LogFileManager.DeleteFile(Key);   
                            }
                        }
                    }

                    // 如果该页面已经没有记录了，不再记录该页面
                    if (pageItem.FileItemList != null && pageItem.FileItemList.Count > 0)
                    {
                        this.PageList.Add(pageItem);
                    }
                }
            }
        }

        public void AddRecord(string url, string key, string createDate)
        {
            PageInfoItem pageItem = null;

            foreach (PageInfoItem page in this.PageList)
            {
                if (page.Url == url)
                {
                    pageItem = page;
                    break;
                }
            }

            if (pageItem == null)
            {
                pageItem = new PageInfoItem();
                pageItem.Url = url;

                this.PageList.Add(pageItem);
            }

            string Key = key;
            string CreateDate = createDate;
            string Url = url;

            pageItem.AddFileItem(Key, CreateDate, "0", Key);
        }

        public void DelRecord(string key)
        {
            Boolean find = false;

            for (int i = 0, l = this.PageList.Count; i < l; i++)
            {
                if(find)
                {
                    break;
                }

                for (int j = 0, m = this.PageList[i].FileItemList.Count; j < m; j++)
                {
                    if (key == this.PageList[i].FileItemList[j].Key)
                    {
                        this.PageList[i].FileItemList.Remove(this.PageList[i].FileItemList[j]);

                        find = true;
                        break;
                    }

                }

            }

            LogFileManager.DeleteFile(key);
        }

        public void SaveRecord()
        {
            XmlDocument document = new XmlDocument();
            document.Load(FiddlerPath.RecordFilePath);

            XmlElement root = document.DocumentElement;
            XmlElement newPageNode = null;
            XmlElement newFileNode = null;

            root.FirstChild.RemoveAll();

            for (int i = 0, l = this.PageList.Count; i < l; i++)
            {
                newPageNode = document.CreateElement("Page");
                newPageNode.SetAttribute("Url", this.PageList[i].Url);
                newPageNode.SetAttribute("CreateDate", this.PageList[i].CreateDate);

                for (int j = this.PageList[i].FileItemList.Count - 1; j >= 0; j--)
                {
                    newFileNode = document.CreateElement("File");
                    newFileNode.SetAttribute("Key", this.PageList[i].FileItemList[j].Key);
                    newFileNode.SetAttribute("Url", "");
                    newFileNode.SetAttribute("Order", this.PageList[i].FileItemList[j].Order);
                    newFileNode.SetAttribute("CreateDate", this.PageList[i].FileItemList[j].CreateDate);

                    newPageNode.AppendChild(newFileNode);
                }

                root.FirstChild.AppendChild(newPageNode);
            }

            document.Save(FiddlerPath.RecordFilePath);
        }
    }
}
