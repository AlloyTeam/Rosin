using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using AlloyTeam.Rosin.Base.Configuration;

namespace AlloyTeam.Rosin.WebServer
{
    public class MIMEHelper
    {
        private static Dictionary<string, string> MIMEMap = new Dictionary<string, string>(); 

        public static string getMIMEType(string extName, string MIME)
        {
            string mimeConfigString;
            string[] items;
            string itemKey;

            if (MIMEMap.Count == 0)
            {
                mimeConfigString = AppConfigReader.getAppValue("MIMEType");
                items = mimeConfigString.Split(',');

                for (int i = 0; i < items.Length; i++)
                {
                    itemKey = items[i].Split(';')[0].Trim();
                    MIMEMap.Add(itemKey, items[i].Trim());
                }
            }

            if (MIME.Contains(extName))
            {
                return MIMEMap[extName];
            }

            return "";
        }
    }
}
