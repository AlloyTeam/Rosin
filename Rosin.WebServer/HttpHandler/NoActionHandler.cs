using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace AlloyTeam.Rosin.WebServer.HttpHandler
{
    public class NoActionHandler : IHttpHandler
    {
        void IHttpHandler.Process(HttpListenerContext ctx, Context serverContext)
        {
            throw new NotImplementedException();
        }
    }
}
