using System;
using System.Collections.Generic;
using System.Text;
using AlloyTeam.Rosin.WebServer.Channel;

namespace AlloyTeam.Rosin.WebServer
{
    public class Context
    {
        public string VirtualDirectory { get; set; }
        public string RequestAction { get; set; }
        public string socketID { get; set; }
        public IChannel Channel { get; set; }
    }
}
