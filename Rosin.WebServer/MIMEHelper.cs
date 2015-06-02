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

        public static string getMIMEType(string extName)
        {
            string mimeConfigString;
            string[] items;
            string itemKey;
            string itemValue;

            if (MIMEMap.Count == 0)
            {
                mimeConfigString = AppConfigReader.getAppValue("MIMEType");
                items = mimeConfigString.Split(',');

                for (int i = 0; i < items.Length; i++)
                {
                    itemKey = items[i].Split(';')[0].Trim();
                    itemValue = items[i].Split(';')[1].Trim();
                    MIMEMap.Add(itemKey, itemValue);
                }
            }

            if (MIMEMap.ContainsKey(extName))
            {
                return MIMEMap[extName];
            }

            return "";
        }
    }
}
