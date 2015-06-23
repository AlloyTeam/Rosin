using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AlloyTeam.Rosin.WebServer.Channel
{
    public interface IChannel
    {
        AutoResetEvent WaitingFlag { get; set; }

        string GetOutput();
    }
}
