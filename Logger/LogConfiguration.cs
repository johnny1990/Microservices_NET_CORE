﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Logger
{
    public class LogConfiguration
    {
        public static string LogDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["LogDirectory"];
            }
        }
    }
}
