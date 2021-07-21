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

        public static string GirPath()
        {
            return GetConfig()["gir2qcPath"];
        }
        public static string ImagesPath()
        {
            return GetConfig()["ImagesPath"];
        }
        public static string NonExistingPartNo()
        {
            return GetConfig()["NonExistingPartNo"];
        }
        public static string TestAutomationPath()
        {
            return GetConfig()["TestAutomationPath"];
        }
        public static string ImagesToUsePath()
        {
            return GetConfig()["ImagesToUsePath"];
        }
        public static string LastImage()
        {
            return GetConfig()["LastImage"];
        }

        public static string DifferentRegion()
        {
            return GetConfig()["DifferentRegion"];
        }
    }
}










