using System;
using System.Collections.Generic;
using System.Text;

namespace Rosin.Item
{
    public class LogItem
    {
        public string key { get; set; }
        public string level { get; set; }
        public string time { get; set; }
        public string content { get; set; }

        public override string ToString()
        {
            return "key=" + this.key + ";level=" + this.level + ";time=" + this.time + ";content=" + this.content;
        }
    }
}
