using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace AlloyTeam.Rosin.WebServer.HttpHandler
{
    public class RequestHandler : IHttpHandler
    {
        HttpListenerRequest _request;
        HttpListenerResponse _response;
        Context _context;

        public void Process(System.Net.HttpListenerContext ctx, Context serverContext)
        {
            string action;

            _request = ctx.Request;
            _context = serverContext;
            _response = ctx.Response;
            action = GetAction();

            //使用Writer输出http响应代码
            using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
            {
                if (action == "getVersion")
                {
                    _response.StatusCode = 200;
                    _response.Headers.Add("Content-Type: .js; application/x-javascript");
                    writer.Write("versioncb(12);");
                    writer.Flush();
                }
                else
                {
                    _response.StatusCode = 404;
                    writer.Flush();
                }
            }
        }

        private string GetAction()
        {
            string ret = _request.QueryString["action"];
            return ret;
        }
    }
}
