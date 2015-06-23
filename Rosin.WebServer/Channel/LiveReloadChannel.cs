using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AlloyTeam.Rosin.WebServer.Channel
{
    public class LiveReloadChannel : IChannel
    {
        public AutoResetEvent WaitingFlag { get; set; }

        public LiveReloadChannel()
        {
            this.WaitingFlag = new AutoResetEvent(false);
        }

        public string GetOutput()
        {
            return string.Empty;
        }
    }
}
