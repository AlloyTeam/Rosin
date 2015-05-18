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
        private delegate void delegate_HttpHandler(HttpListenerContext ctx, Context serverContext);

        public string Prefix { get; private set; }

        public Server(string prefix)
        {
            listener = new HttpListener();
            Prefix = prefix;
            listener.Prefixes.Add(Prefix);
            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
        }

        public void Start()
        {
            Logger.Debug("Server start at " + Prefix);
            listener.Start();
            listener.BeginGetContext(new AsyncCallback(GetContextCallBack), null);
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
            Logger.Debug("Processing Finished!");
        }
    }
}
