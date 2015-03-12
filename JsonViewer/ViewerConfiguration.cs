using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace EPocalipse.Json.Viewer
{
    public class ViewerConfiguration: ConfigurationSection
    {
        [ConfigurationProperty("plugins")]
        public KeyValueConfigurationCollection Plugins
        {
            get
            {
                return (KeyValueConfigurationCollection)base["plugins"];
            }
        }
    }
}
