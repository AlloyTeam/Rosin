using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace AlloyTeam.Rosin.Base.Configuration
{
    public class AppConfigReader
    {
        private static readonly System.Configuration.Configuration config = getConfiguration();

        private static System.Configuration.Configuration getConfiguration()
        {
            System.Configuration.Configuration ret;
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + @"\Rosin.config";
            ret = ConfigurationManager.OpenMappedExeConfiguration(map,ConfigurationUserLevel.None);
            return ret;
        }

        public static string getAppValue(string key)
        {
            return config.AppSettings.Settings[key].Value;
        }
    }
}
