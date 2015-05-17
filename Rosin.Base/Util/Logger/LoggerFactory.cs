using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace AlloyTeam.Rosin.Base.Util.Logger
{
    public class LoggerFactory
    {
        private static Dictionary<string, ILog> dicLoggers = new Dictionary<string,ILog>();

        public static ILog GetLogger(string loggerName)
        {
            if (!dicLoggers.ContainsKey(loggerName))
            {
                lock (dicLoggers)
                {
                    if (!dicLoggers.ContainsKey(loggerName))
                    {
                        dicLoggers.Add(loggerName, LogManager.GetLogger(loggerName));
                    }
                }
            }

            return dicLoggers[loggerName];
        }
    }
}
