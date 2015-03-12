using System;
using System.Collections.Generic;
using System.Text;

namespace Rosin.Event
{
    public class LogEvent
    {
        // 写日志的事件
        public delegate void Write_EventHandler(object serder, EventArgs e);
        public delegate void Create_EventHandler(object serder, EventArgs e);

        public static event Write_EventHandler Write;
        public static event Create_EventHandler Create;
    }
}
