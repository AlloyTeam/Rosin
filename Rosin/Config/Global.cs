using System;
using System.Collections.Generic;
using System.Text;

namespace Rosin.Config
{
    static class Global
    {
        public const string sRosinDomain = "__rosin__.qq.com"; // 日志接收域名

        public const int iMaxFileNum = 25; // 每个页面保留的最多的文件数
		public const Int64 iExpiresTime = 3*24*60*60*1000; // 日志记录的保留天数，3天有效期

        public const string JSON_TAG = "Object";

        /**
         * 采用零宽空格包裹JSON
         */
        public const string JSON_SPLITER = "\uFEFF";

        /**
         * 显示到UI上的JSON字符串长度
         * 超过就截断打点
         */
        public const int JSON_OBJ_LENGTH = 30;

        public const string JSON_INDEX = "C4A0";
    }
}
