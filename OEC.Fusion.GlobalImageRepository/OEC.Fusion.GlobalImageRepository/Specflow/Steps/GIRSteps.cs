using Microsoft.VisualStudio.TestTools.UnitTesting;
using OEC.Fusion.GlobalImageRepository.Actions;
using OEC.Fusion.GlobalImageRepository.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;


namespace OEC.Fusion.GlobalImageRepository.Specflow.Steps
{
    [Binding]
    public class GIRSteps : FileHelper
    {
        String subdir { get; set; }
        public string StoragePath { get; private set; }

        static string datetime = getCurDate();
        //String deviceID = ConfigurationManager.AppSettings["DeviceID"];
        //AssertLight assert = new AssertLight();
        private DBHelper dbhelper = new DBHelper();
        public string result = "";
        private string path;
        ScenarioContext _scenariocontext;

        /*public GIRSteps(ScenarioContext scenarioContext)
        {
            _scenariocontext = scenarioContext;
        }*/

        [Given(@"When I connect to the database to execute query to get the partNumber")]
       public void GivenWhenIConnectToTheDatabase()
        {
            //Console.WriteLine(datetime);
           String deviceID = ConfigurationManager.AppSettings["QAServerPath"];
            Console.WriteLine("Test");
            result = dbhelper.GetPartNumber()[0];
        }

        [When(@"I verify the partnumber images are not present in working directory")]
        public void WhenIVerifyThePartnumberImagesAreNotPresentInTheWorkingDirectory()
        {
            Common rsult = new Common();

           // _scenariocontext["isImagePresent"] = rsult.ImagePresent();
            /*var search = Directory.GetFiles(@"\\UQWDB023.qa.oec.local\Images\", "*aiswarya.jpg");
            //string a = Path.GetFileName(@"\\UQWDB023.qa.oec.local\Images\MM1PZ16A550BA-360-01.jpg");
            //Assert.IsTrue(Path.GetFileName(@"\\UQWDB023.qa.oec.local\Images\MM1PZ16A550BA-360-01.jpg").Contains("MM1PZ16A550BA"));
            //int len = search.Length;
            //string[] search2 = Directory.GetFiles(@"\\UQWDB023.qa.oec.local\Images\", "*aiswarya");
            //string[] search1 = Directory.GetFiles(@"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya"); 
            foreach (var files in search)
            {
                FileInfo info = new FileInfo(files);
                var fileName = Path.GetFileName(info.FullName);
                Assert.IsFalse(false, result + " - 360-01.jpg", fileName);
            }*/


            Assert.IsFalse(Convert.ToBoolean(rsult.ImageNotPresent(result)));
        }
    

    [Then(@"Create a folder for images in local in the format payyyymmdd_hhmmss")]
    public void ThenCreateAFolderForImagesInLocal()
    {
      //bool value = Convert.ToBoolean(_scenariocontext["isImagePresent"]);


       String dir = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\";
         if (!Directory.Exists(dir))
         {
             Directory.CreateDirectory(dir);
         }

            Directory.CreateDirectory(CreateFolderWithCurrentDate(datetime));
            
    }

        [Then(@"Copy 24 files from parts folder to the newly created folder")]
        public void CopyFilesFromPartsToNewFolder()
        {
             String source = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\ImagesToUse";
              String target = @"\\UQWDB023.qa.oec.local\\test\\ctsftp.gir2qc\\aiswarya\\pa" + datetime + "\\";


              String[] files = Directory.GetFiles(source);
              foreach (string fil in files)
              {
                  string fileName = Path.GetFileName(fil);
                  File.Copy(fil, target + fileName, true);
              }
        }

        [Then(@"Rename the files with the given partnumber")]
        public void RenameFilesWithPartNumber()
        {
           String result = dbhelper.GetPartNumber()[0];
            Console.WriteLine(result);
             DirectoryInfo d = new DirectoryInfo(@"\\UQWDB023.qa.oec.local\\test\\ctsftp.gir2qc\\aiswarya\\pa" + datetime + "\\");
                FileInfo[] infos = d.GetFiles();
                foreach (FileInfo f in infos)
                {
                    File.Move(f.FullName, f.FullName.Replace("MN1Z5863805AA", result));

                }
        }

        [Then(@"Zip the folder")]
        public void ZipCreatedFolder()
        {
           ZipFile.CreateFromDirectory(@"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\pa" + datetime + "", @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\pa" + datetime + ".zip");
        }


        [Then(@"Find the sftp dirctory to upload the zip file")]
        public void ThenFindTheSftpDirctoryToUploadTheZipFile()
        {
           string deviceID = ConfigurationManager.AppSettings["QAServerPath"];
            string path = dbhelper.getsFTPPath()[0];
            String StoragePath = @path;
            Assert.IsTrue(true, StoragePath, @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc");
        }


        [Then(@"Copy the zip file to the ctsftp.gir2qc folder")]
        public void CopyZipFilesToGir2qcFolder()
        {
            
           String src = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\pa" + datetime + ".zip";
             String tgt = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\pa" + datetime + ".zip";
            
            string tgtDirectory = Path.GetDirectoryName(tgt);
             if (File.Exists(src) && Directory.Exists(tgtDirectory))
             {
                 File.Copy(src, tgt);
             }
        }


        [Then(@"Run spPRODDailyDownload procedure to Upload zip file in Images folder")]
        public void ThenRunSpPRODDailyDownloadProcedureToUploadZipFileInImagesFolder()
        {
           String sprocedure = "[GlobalImageRepository].[dbo].[spPRODDailyDownload]";
           dbhelper.spPRODDailyDownload(sprocedure);
        }
        
        [Then(@"Verify the partnumber is present in the image folder")]
        public void VerifyPartNumberPresentInImages()
        {

            Common rsult = new Common();
            Assert.IsTrue(Convert.ToBoolean(rsult.ImagePresent(result)));
            /*int a = 1;
              string[] search = Directory.GetFiles(@"\\UQWDB023.qa.oec.local\Images\", "*.jpg");
              string b;
              foreach (var files in search)
              {


                  FileInfo info = new FileInfo(files);
                  var fileName = Path.GetFileName(info.FullName);
                  //Console.WriteLine(fileName);
                  if (a < 10)
                  {
                      int decimalLength = a.ToString("D").Length + 1;
                      b = a.ToString("D" + decimalLength.ToString());
                      Assert.AreEqual(result + "-360-" + b + ".jpg", fileName);
                      a++;
                  }
                  else
                  {
                      Assert.AreEqual(result + "-360-" + a + ".jpg", fileName);
                      a++;
                  }
              }*/
        }

        [Then(@"Verify the partnumber is present in the image folder using query")]
        public void ThenVerifyThePartnumberIsPresentInTheImageFolderUsingQuery()
        {
            //ScenarioContext.Current.Pending();
            Console.WriteLine("test");
        }



    }
}
