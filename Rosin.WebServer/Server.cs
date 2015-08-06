using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using AlloyTeam.Rosin.Base.Util.Logger;
using AlloyTeam.Rosin.WebServer.HttpHandler;
using AlloyTeam.Rosin.WebServer.Channel;

namespace AlloyTeam.Rosin.WebServer
{
    public class Server
    {
        private HttpListener listener;
        private delegate void delegate_HttpHandler(HttpListenerContext ctx, Context serverContext);
        private const string CHANNELPREVFIX = "/_Rosin_Channel/";
        private const string REQUESTPREVFIX = "/_Rosin_Request/";
        private const string RESOURCEPREVFIX = "/_Rosin_Resource/";
        private const string LIVERELOADPREFIX = "/Live_Reload/";

        public List<string> Prefixs { get; private set; }

        public KeyValuePair<string, string> ResourcePath { get; set; }
        public Dictionary<string, Context> ContextPool = new Dictionary<string,Context>();

        public Server(List<string> prefixs)
        {
            string curPath = AppDomain.CurrentDomain.BaseDirectory + @"\Scripts\Rosin\Web\";
            ResourcePath = new KeyValuePair<string, string>(RESOURCEPREVFIX, curPath);
            Prefixs = prefixs;
            listener = new HttpListener();

            Prefixs.ForEach(p => listener.Prefixes.Add(p));

            //listener.Prefixes.Add(Prefix);
            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
        }

        public void Start()
        {
            Prefixs.ForEach(p => Logger.Debug("Server start at " + p));
            listener.Start();
            listener.BeginGetContext(new AsyncCallback(GetContextCallBack), listener);
        }

        public void Stop()
        {
            try
            {
                listener.Stop();
            }
            catch (ObjectDisposedException ex)
            {
                Logger.Debug("Server closed with error!", ex);
            }

            Logger.Debug("Server closed!");
        }

        public void SendChannelMsg(string msg)
        {
            return;
        }

        private void GetContextCallBack(IAsyncResult ar)
        {
            try
            {
                HttpListenerContext ctx = listener.EndGetContext(ar);
                delegate_HttpHandler d = new delegate_HttpHandler(HandleRequest);
                Context serverContext = new Context();

                Logger.Debug("Got client!");
                d.BeginInvoke(ctx, serverContext, null, null);

                listener.BeginGetContext(new AsyncCallback(GetContextCallBack), listener);
                Logger.Debug("waiting for more client...");
            }
            catch (HttpListenerException ex)
            {
                Logger.Error("Got HttpListenerException!", ex);
            }
        }

        private void HandleRequest(HttpListenerContext ctx, Context serverContext)
        {
            string vPath = ctx.Request.Url.AbsolutePath;
            string socketId = ctx.Request.RemoteEndPoint.Address.ToString() + ":" + ctx.Request.RemoteEndPoint.Port.ToString();

            Logger.Debug("Request URL:" + ctx.Request.Url);

            //上下文池
            if (!ContextPool.ContainsKey(socketId))
            {
                ContextPool.Add(socketId, serverContext);
            }
            else
            {
                ContextPool[socketId] = serverContext;
            }
            
            IHttpHandler handler = null;

            if (vPath.StartsWith(ResourcePath.Key))
            {
                //使用静态服务器处理
                serverContext.VirtualDirectory = ResourcePath.Value;
                serverContext.RequestAction = vPath.Replace(ResourcePath.Key, "/");
                handler = new StaticFileHandler();
            }
            else if (vPath.StartsWith(CHANNELPREVFIX))
            {
                //live-reload处理
                if (vPath.IndexOf(LIVERELOADPREFIX) > 0)
                {
                    serverContext.Channel = new LiveReloadChannel();
                }

                //Channel请求处理
                if (serverContext.Channel != null)
                {
                    handler = new ChannelHandler(serverContext.Channel);
                }
                else
                {
                    handler = new NoActionHandler();
                }
            }
            else if (vPath.StartsWith(REQUESTPREVFIX))
            {
                //动态请求处理
                handler = new RequestHandler();
            }
            else
            {
                handler = new NoActionHandler();
            }

            if (handler != null)
            {
                handler.Process(ctx, serverContext);
            }

            Logger.Debug("Processing Finished!");
        }
    }
}
