﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AlloyTeam.Rosin.WebServer
{
    public class Context
    {
        public string VirtualDirectory { get; set; }
        public string RequestAction { get; set; }
        public string socketID { get; set; }
        public Channel Channel { get; set; }
    }
}
