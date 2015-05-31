using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace AlloyTeam.Rosin.Base.Configuration
{
    public class AppConfigReader
    {
        public static string getAppValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
