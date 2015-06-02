using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace AlloyTeam.Rosin.WebServer.HttpHandler
{
    public class NoActionHandler : IHttpHandler
    {
        void IHttpHandler.Process(HttpListenerContext ctx, Context serverContext)
        {
            HttpListenerResponse res = ctx.Response;
            
            using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
            {
                res.StatusCode = 404;
                writer.Flush();
            }
        }
    }
}
