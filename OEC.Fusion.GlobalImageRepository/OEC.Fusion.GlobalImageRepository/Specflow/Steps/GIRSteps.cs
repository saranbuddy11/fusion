using Microsoft.VisualStudio.TestTools.UnitTesting;
using OEC.Fusion.GlobalImageRepository.Actions;
using OEC.Fusion.GlobalImageRepository.Helpers;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using TechTalk.SpecFlow;


namespace OEC.Fusion.GlobalImageRepository.Specflow.Steps
{
    [Binding]
    public class GIRSteps : FileHelper
    {
        String subdir { get; set; }
        static string datetime = getCurDate();

        /*private DBHelper dbhelper = new DBHelper();
        public DAL dl = new DAL(sql);
        private static string sql;*/

        [Given(@"When I connect to the database")]
        public void GivenWhenIConnectToTheDatabase()
        {
            Console.WriteLine("Test");
        }

        [Given(@"I execute the query to get the partnumber")]
        public void GivenIExecuteTheQueryToGetThePartnumber()
        {
            Console.WriteLine("Test");
        }

        [When(@"I verify the partnumber images are not present in working directory")]
        public void WhenIVerifyThePartnumberImagesAreNotPresentInTheWorkingDirectory()
        {
            Console.WriteLine("Test");
        }

        [Then(@"Create a folder for images in local in the format payyyymmdd_hhmmss")]
        public static void ThenCreateAFolderForImagesInLocal()
        {
            String dir = @"C:\Users\arameshbaabu\Desktop\GIR\Local\Aiswarya";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

        Directory.CreateDirectory(CreateFolderWithCurrentDate(datetime));
            
        }

        [Then(@"Copy 24 files from parts folder to the newly created folder")]
        public void CopyFilesFromPartsToNewFolder()
        {

            
            String source = @"C:\Users\arameshbaabu\Desktop\GIR\Local\Aiswarya\ImagesToUse";
            String target = @"C:\\Users\\arameshbaabu\\Desktop\\GIR\\Local\\Aiswarya\\pa[" + datetime + "]\\";
            
            
            String[] files = Directory.GetFiles(source);
            foreach (string fil in files)
            {
                string fileName = Path.GetFileName(fil);
                File.Copy(fil, target + fileName, true);
            }
            Console.WriteLine("test6");
        }

        [Then(@"Rename the files with the given partnumber")]
        public void RenameFilesWithPartNumber()
        {
            String partnumber = "MM1PZ16A550BA";
            DirectoryInfo d = new DirectoryInfo(@"C:\\Users\\arameshbaabu\\Desktop\\GIR\\Local\\Aiswarya\\pa[" + datetime + "]\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.Replace("MN1Z5863805AA", partnumber));
            }
        }

        [Then(@"Zip the folder")]
        public void ZipCreatedFolder()
        {
            ZipFile.CreateFromDirectory(@"C:\Users\arameshbaabu\Desktop\GIR\Local\Aiswarya\pa[" + datetime + "]", @"C:\Users\arameshbaabu\Desktop\GIR\Local\Aiswarya\pa[" + datetime + "].zip");
        }

        [Then(@"Copy the zip file to the Images folder")]
        public void CopyZipFilesToImagesFolder()
        {
            String src = @"C:\Users\arameshbaabu\Desktop\GIR\Local\Aiswarya\pa[" + datetime + "].zip";
            String tgt = @"C:\Users\arameshbaabu\Desktop\GIR\Images\pa[" + datetime + "].zip";
            string tgtDirectory = Path.GetDirectoryName(tgt);
            if (File.Exists(src) && Directory.Exists(tgtDirectory))
            {
                File.Copy(src, tgt);
            }
                      
            
        }

        [Then(@"Unzip the copied folder in the Images")]
        public void UnzipFolder()
        {
            Console.WriteLine("Test2");
        }

        [Then(@"Verify the partnumber is present in the image folder")]
        public void VerifyPartNumberPresentInImages()
        {
            Console.WriteLine("Test2");
        }



    }
}
