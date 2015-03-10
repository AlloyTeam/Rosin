using System;
using System.Collections.Generic;
using System.Text;

using Rosin.Config;
using Rosin.Util;

namespace Rosin.Item
{
    public class PageInfoItem
    {
        public string Url { get; set; }
        public string CreateDate { get; set; }
        public List<FileInfoItem> FileItemList { get; set; }

        public void AddFileItem(string url, string createDate, string order, string key)
        {
            Int16 iOrderMax = 0;

            if (this.FileItemList == null)
            {
                this.FileItemList = new List<FileInfoItem>();
            }

            bool isFileItemExist = false;

            foreach (FileInfoItem itemFile in this.FileItemList)
            {
                if (itemFile.Key == key)
                {
                    isFileItemExist = true;
                    break;
                }

                if (Convert.ToInt16(itemFile.Order) >= iOrderMax)
                {
                    iOrderMax = Convert.ToInt16(itemFile.Order);
                }
            }

            if (!isFileItemExist)
            {
                FileInfoItem file = new FileInfoItem();
                file.Url = url;
                file.Key = key;

                if (Convert.ToInt16(order) == 0)
                {
                    file.Order = (iOrderMax + 1).ToString();
                }
                else
                {
                    file.Order = order;
                }
                
                file.CreateDate = createDate;
                file.CreateDateString = "[ " + file.Order + " ] " + TimeFormat.GetTime((Convert.ToUInt64(createDate) * 1000).ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                this.FileItemList.Insert(0, file);
            }

            this.FilterMaxItem(Global.iMaxFileNum);
        }

        public void FilterMaxItem(int maxNum)
        {
            while (this.FileItemList.Count > maxNum)
            {
                this.FileItemList.RemoveAt(this.FileItemList.Count-1);
            }
        }

        public override string ToString()
        {
            return this.Url;
        }
    }
}
