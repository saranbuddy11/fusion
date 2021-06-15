﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OEC.Fusion.GlobalImageRepository.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository.PageObjects
{
    public class DBexeution
    {
        private DBHelper dbhelper = new DBHelper();

        public void DirectoryToUploadTheZipFile()
        {
            string datetime = dbhelper.getCurDateTime()[0];
            string deviceID = ConfigurationManager.AppSettings["QAServerPath"];
            string path = dbhelper.getsFTPPath()[0];
            String StoragePath = @path;
            Assert.IsTrue(true, StoragePath, @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc");
        }

        public void RunSpPRODDailyDownloadProcedure()
        {
            String sprocedure = "[GlobalImageRepository].[dbo].[spPRODDailyDownload]";
            dbhelper.spPRODDailyDownload(sprocedure);
        }
    }
}
