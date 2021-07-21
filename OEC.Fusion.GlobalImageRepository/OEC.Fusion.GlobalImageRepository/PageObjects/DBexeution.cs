﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OEC.Fusion.GlobalImageRepository.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository.PageObjects
{
    public class DBexeution
    {
        private DBHelper dbhelper = new DBHelper();

        public void DirectoryToUploadTheZipFile()
        {
            string datetime = dbhelper.GetCurDateTime()[0];
            string deviceID = ConfigurationManager.AppSettings["QAServerPath"];
            string path = dbhelper.GetsFTPPath()[0];
            string[] storagePath = path.Split('\\');
            string lastPath = storagePath.Last().ToString();
            string gpath = ConfigHelper.GirPath();
            string[] girPath = gpath.Split("\\\\");
            string lastGirPath = girPath[3].ToString();
            Assert.AreEqual(lastPath, lastGirPath);
        }

        public void DirectoryToUploadTheZipFileDiffRegion()
        {
            string datetime = dbhelper.GetCurDateTime()[0];
            string deviceID = ConfigurationManager.AppSettings["QAServerPath"];
            string path = dbhelper.GetDifferentRegionsFTPPath()[0];
            string[] storagePath = path.Split('\\');
            string lastPath = storagePath.Last().ToString();
            string gpath = ConfigHelper.DifferentRegion();
            string[] girPath = gpath.Split("\\\\");
            string lastGirPath = girPath[3].ToString();
            Assert.AreEqual(lastPath, lastGirPath);
        }

        public void RunSpPRODDailyDownloadProcedure()
        {
            String sprocedure = "[GlobalImageRepository].[dbo].[spPRODDailyDownload]";
            dbhelper.SpPRODDailyDownload(sprocedure);
        }
    }
}
