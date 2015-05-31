using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
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
            //FileStream fs = new FileStream(staticFilePath, FileMode.Open);


            staticFilePath = serverContext.VirtualDirectory + (serverContext.RequestAction).Replace("/", "\\").Replace(@"\\", @"\");
            sr = new StreamReader(staticFilePath);
            //br = new BinaryReader(sr.r);


            //serverContext.VirtualDirectory
            ctx.Response.StatusCode = 200;

            //使用Writer输出http响应代码
            using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
            {
                //fs.w
            }
        }
    }
}
