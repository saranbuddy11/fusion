using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository.Helpers
{
    public static class ConfigHelper
    {
        public static string DBConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["GlobalImageRepository"].ConnectionString; }
        }
    }
}
