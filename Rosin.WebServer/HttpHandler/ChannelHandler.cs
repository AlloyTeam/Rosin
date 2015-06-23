using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using AlloyTeam.Rosin.WebServer.Channel;

namespace AlloyTeam.Rosin.WebServer.HttpHandler
{
    public class ChannelHandler : IHttpHandler
    {
        private IChannel _channel;

        public ChannelHandler(IChannel channel)
        {
            _channel = channel;
        }

        public void Process(HttpListenerContext ctx, Context serverContext)
        {
            HttpListenerResponse res = ctx.Response;

            using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
            {
                res.StatusCode = 200;
                res.Headers.Add(string.Format("Content-Type: .js; application/x-javascript"));

                _channel.WaitingFlag.WaitOne();
                writer.Write(_channel.GetOutput());
                writer.Flush();
            }
        }
    }
}
