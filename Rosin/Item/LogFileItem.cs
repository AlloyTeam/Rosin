using System.Collections.Generic;
using Rosin.Item;

namespace Rosin
{
    /**
     * 日志文件的实体Bean
     */
    class LogFileItem
    {
        public string key = "";

        public string pageUrl = "";

        public string createDate = "";

        public List<LogItem> logList = new List<LogItem>();

        public List<JsonItem> jsonList = new List<JsonItem>();

        public void Clear()
        {
            this.key = string.Empty;
            this.pageUrl = string.Empty;
            this.createDate = string.Empty;
            this.logList = new List<LogItem>();
            this.jsonList = new List<JsonItem>();
        }

        public void ClearList()
        {
            this.logList.RemoveRange(0, this.logList.Count);
        }
    }
}
