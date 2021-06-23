using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository.Helpers
{
    public static class ConfigHelper
    {
        public static string DBConnectionString;
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(System.AppContext.BaseDirectory).AddJsonFile("appSettings.json", optional: true, reloadOnChange: true); return builder.Build();
        }

        public static string GetUserName()
        {
        return GetConfig()["username"];
            
        //    get { return ConfigurationManager.ConnectionStrings["GlobalImageRepository"].ConnectionString; }
        }
        



        public static string GetConnectionString()
        {
            return GetConfig()["DefaultConnection"];
        }
    }
}
}









