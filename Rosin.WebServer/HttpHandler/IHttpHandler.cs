using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace AlloyTeam.Rosin.WebServer.HttpHandler
{
    public interface IHttpHandler
    {
        void Process(HttpListenerContext ctx, Context serverContext);
    }
}
