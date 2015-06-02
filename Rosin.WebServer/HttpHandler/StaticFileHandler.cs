﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using AlloyTeam.Rosin.Base.Util;
using AlloyTeam.Rosin.Base.Util.Logger;

namespace AlloyTeam.Rosin.WebServer.HttpHandler
{
    public class StaticFileHandler : IHttpHandler
    {
        public void Process(HttpListenerContext ctx, Context serverContext)
        {
            string vPath = ctx.Request.Url.AbsolutePath;
            string staticFilePath;
            StreamReader sr;
            string ext;
            string mime;
            HttpListenerResponse res = ctx.Response;

            staticFilePath = serverContext.VirtualDirectory + (serverContext.RequestAction).Replace("/", "\\").Replace(@"\\", @"\");
            ext = Path.GetExtension(staticFilePath);
            mime = MIMEHelper.getMIMEType(ext);

            //使用Writer输出http响应代码
            using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
            {
                //输出Header
                res.StatusCode = 200;
                res.Headers.Add(string.Format("Content-Type: {0}", mime));

                sr = new StreamReader(staticFilePath, EncodingHelper.detectTextEncoding(staticFilePath));

                writer.Flush();
            }
        }
    }
}
