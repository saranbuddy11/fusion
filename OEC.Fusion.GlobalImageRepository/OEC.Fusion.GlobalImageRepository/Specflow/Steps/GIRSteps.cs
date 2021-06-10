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
        static string datetime = getCurDate();
        //String deviceID = ConfigurationManager.AppSettings["DeviceID"];
        //AssertLight assert = new AssertLight();
        private DBHelper dbhelper = new DBHelper();
        private string result;

        [Given(@"When I connect to the database to execute query to get the partNumber")]
        public void GivenWhenIConnectToTheDatabase()
        {
            Console.WriteLine("Test");
            Console.WriteLine(datetime);
           /* String deviceID = ConfigurationManager.AppSettings["QAServerPath"];
            Console.WriteLine("Test");
            String result = dbhelper.GetPartNumber()[0];
            Console.WriteLine(result);*/
        }

        [When(@"I verify the partnumber images are not present in working directory")]
        public void WhenIVerifyThePartnumberImagesAreNotPresentInTheWorkingDirectory()
        {
            Console.WriteLine("Test");
           // Assert.IsFalse();
                    
        }
    

    [Then(@"Create a folder for images in local in the format payyyymmdd_hhmmss")]
    public static void ThenCreateAFolderForImagesInLocal()
    {
        /* String dir = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\";
         if (!Directory.Exists(dir))
         {
             Directory.CreateDirectory(dir);
         }

         Directory.CreateDirectory(CreateFolderWithCurrentDate(datetime));
         */
        Console.WriteLine("Test");

    }

        [Then(@"Copy 24 files from parts folder to the newly created folder")]
        public void CopyFilesFromPartsToNewFolder()
        {
           /*String source = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\ImagesToUse";
            String target = @"\\UQWDB023.qa.oec.local\\test\\ctsftp.gir2qc\\aiswarya\\pa" + datetime + "\\";


            String[] files = Directory.GetFiles(source);
            foreach (string fil in files)
            {
                string fileName = Path.GetFileName(fil);
                File.Copy(fil, target + fileName, true);
            }*/
            Console.WriteLine("test6");
        }

        [Then(@"Rename the files with the given partnumber")]
        public void RenameFilesWithPartNumber()
        {
            /*String partnumber = "MM1PZ16A55";
            DirectoryInfo d = new DirectoryInfo(@"\\UQWDB023.qa.oec.local\\test\\ctsftp.gir2qc\\aiswarya\\pa" + datetime + "\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.Replace("MN1Z5863805AA", partnumber));
            }*/
            Console.WriteLine("Test");
        }

        [Then(@"Zip the folder")]
        public void ZipCreatedFolder()
        {
            //ZipFile.CreateFromDirectory(@"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\pa" + datetime + "", @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\pa" + datetime + ".zip");
            Console.WriteLine("Test");
        }

        [Then(@"Copy the zip file to the Images folder")]
        public void CopyZipFilesToImagesFolder()
        {
            /*String src = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\pa" + datetime + ".zip";
            String tgt = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\pa" + datetime + ".zip";
            string tgtDirectory = Path.GetDirectoryName(tgt);
            if (File.Exists(src) && Directory.Exists(tgtDirectory))
            {
                File.Copy(src, tgt);
            }*/
            Console.WriteLine("Test");


        }

        [Then(@"Unzip the copied folder in the Images")]
        public void UnzipFolder()
        {
            /*string zipPath = @"C:\Users\arameshbaabu\Desktop\GIR\Images\pa[" + datetime + "].zip";
            string extractPath = @"C:\Users\arameshbaabu\Desktop\GIR\Images";

            ZipFile.ExtractToDirectory(zipPath, extractPath);
            Console.WriteLine("Unzipfolder");*/
        }
        
        [Then(@"Verify the partnumber is present in the image folder")]
        public void VerifyPartNumberPresentInImages()
        {
            int a = 1;
            string[] search = Directory.GetFiles(@"C:\Users\arameshbaabu\Desktop\GIR\Local\Aiswarya\ImagesToUse\", "*.jpg");
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
            }
        }

    }
}
