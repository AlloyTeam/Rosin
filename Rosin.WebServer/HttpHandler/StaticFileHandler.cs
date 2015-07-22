using System;
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
            int size = 5;
            char[] buf = new char[size];
            long fileLength = 0;
            FileInfo fi;
            int count = 0, i = 0, j = 0, k = 0, tmp = -1;

            staticFilePath = serverContext.VirtualDirectory + (serverContext.RequestAction).Replace("/", "\\").Replace(@"\\", @"\");
            ext = Path.GetExtension(staticFilePath);
            mime = MIMEHelper.getMIMEType(ext);

            try
            {
                fi = new FileInfo(staticFilePath);
                fileLength = fi.Length;
                res.StatusCode = 200;
            }
            catch (FileNotFoundException ex)
            {
                Logger.Error(ex);
                res.StatusCode = 404;
            }

            //使用Writer输出http响应代码
            using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
            {
                //输出Header
                if (res.StatusCode == 200)
                {
                    res.Headers.Add(string.Format("Content-Type: {0}", mime));
                    Encoding encoding = EncodingHelper.detectTextEncoding(staticFilePath);

                    using (sr = new StreamReader(staticFilePath, encoding))
                    {
                        do
                        {
                            j = 0;
                            k = 0;

                            if (fileLength - i * size >= size)
                            {
                                count = size;
                            }
                            else
                            {
                                count = (int)(fileLength - i * size);
                            }

                            // 解决部分html文档大小跟实际内容不一致问题。
                            while (!sr.EndOfStream && j < count)
                            {
                                tmp = sr.Read();

                                if (tmp >= 0)
                                {
                                    buf[k] = (char)tmp;
                                    k++;
                                }

                                j++;
                            }

                            writer.Write(buf, 0, k);
                            i++;
                            writer.Flush();
                        } while (!sr.EndOfStream);
                    }
                }
            }
        }
    }
}
