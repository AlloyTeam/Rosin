using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using log4net;

namespace AlloyTeam.Rosin.Base.Util.Logger
{
    public class Logger
    {
        private Logger()
        {

        }

        private static ILog GetLogger()
        {
            return LoggerFactory.GetLogger("Rosin_Debug");
        }

        private static readonly ILog logger = GetLogger();

        public static void Debug(object message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Debug(message);
            }
            else
            {
                logger.Debug(message, ex);
            }
        }

        public static void Info(object message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Info(message);
            }
            else
            {
                logger.Info(message, ex);
            }
        }

        public static void Warn(object message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Warn(message);
            }
            else
            {
                logger.Warn(message, ex);
            }
        }

        public static void Error(object message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Error(message);
            }
            else
            {
                logger.Error(message, ex);
            }
        }

        public static void Fatal(object message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Fatal(message);
            }
            else
            {
                logger.Fatal(message, ex);
            }
        }


    }
}
