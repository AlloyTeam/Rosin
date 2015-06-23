using System;
using System.Collections.Generic;
using System.Text;
using AlloyTeam.Rosin.Base.Util.Logger;
using AlloyTeam.Rosin.Base.Util;
using System.IO;

namespace AlloyTeam.Rosin.WebServer
{
    public class TestServer
    {
        public static void Main(string[] args)
        {
            List<string> hosts = new List<string>();
            hosts.Add("http://rosin_debug.com/");

            Server s = new Server(hosts);

            s.Start();
            Console.Read();
            //TestReadFile();
        }

        public static void TestReadFile()
        {
            string staticFilePath = @"d:\GitHub\Rosin\Rosin.WebServer\Scripts\Rosin\Web\core.js";
            string path = @"d:\GitHub\Rosin\Rosin.WebServer\Scripts\Rosin\Web\core2.js";
            StreamReader sr;
            FileInfo fi = new FileInfo(staticFilePath);
            long fileLength = fi.Length;
            int i = 0, count = 0, size = 1024, bomlength = 0;
            char[] buf = new char[size];

            //使用Writer输出http响应代码
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                sr = new StreamReader(staticFilePath, Encoding.UTF8);

                bomlength = Encoding.UTF8.GetPreamble().Length;
                //fileLength = fileLength - bomlength;

                do
                {
                    if (fileLength - i * size >= size)
                    {
                        count = size;
                    }
                    else
                    {
                        count = (int)(fileLength - i * size);
                    }

                    sr.Read(buf, 0, count);
                    writer.Write(buf, 0, count);
                    i++;
                    writer.Flush();
                } while (!sr.EndOfStream);
            }
        }
    }
}
