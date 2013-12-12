﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTPMonitor.Model
{
    struct QueryParameter
    {
        public double eastLon;
        public double westLon;
        public double southLat;
        public double northLat;
        public string photoTime;
        public string createTime;
        public bool hasDelete;
        public bool isToday;
    }
}
