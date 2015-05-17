using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using AlloyTeam.Rosin.Base.Util.Logger;


namespace AlloyTeam.Rosin.WebServer
{
    public class Server
    {
        private HttpListener listener;

        public string Prefix { get; private set; }

        public Server(string prefix)
        {
            listener = new HttpListener();
            Prefix = prefix;
            listener.Prefixes.Add(Prefix);
        }

        public void Start()
        {
            listener.Start();
            Logger.Debug("server start at " + Prefix);
        }

        public void Stop()
        {

        }
    }
}
