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

        public static string GetEmail()
        {
        return GetConfig()["email"];
            
        //    get { return ConfigurationManager.ConnectionStrings["GlobalImageRepository"].ConnectionString; }
        }
        public static string GetPassword()
        {
            return GetConfig()["password"];
        }
            public static string GetDefaultConnection()
        {
            return GetConfig()["ConnectionStrings:DefaultConnection"];
        }

        public static string Getname()
        {
            return GetConfig()["ConnectionStrings:name"];
        }


    }
}










