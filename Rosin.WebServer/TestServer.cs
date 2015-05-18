using System;
using System.Collections.Generic;
using System.Text;
using AlloyTeam.Rosin.Base.Util.Logger;

namespace AlloyTeam.Rosin.WebServer
{
    public class TestServer
    {
        public static void Main(string[] args)
        {
            Server s = new Server("http://localhost:9527/");
            s.Start();
            Console.Read();
        }
    }
}
