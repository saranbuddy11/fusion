using OEC.Fusion.GlobalImageRepository.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository.PageObjects
{
    public class FileOperations : DBHelper
    {
        private DBHelper dbhelper = new DBHelper();
        public string datetime = "";

        public void CreateFolderWithCurrentDateTime(string dateTime)
        {            
            String dir = ConfigHelper.TestAutomationPath();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string subdir = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\TestAutomation\"+ dateTime;
            //string path = ConfigHelper.TestAutomationPath();
            //string subdir1 = path + dateTime + "test";
            
            Directory.CreateDirectory(subdir);
           // Directory.CreateDirectory(subdir1);
        }

        public void CopyImagesToUse(string datetime)
        {
            String source = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\TestAutomation\ImagesToUse";
            //String sourc = ConfigHelper.ImagesToUsePath();
            String target = ConfigHelper.TestAutomationPath() + datetime +"\\";
            //String source = Path.GetFullPath(sourc).Replace(@"\\", @"\");
            String[] files = Directory.GetFiles(source);
            foreach (string fil in files)
            {
                string fileName = Path.GetFileName(fil);
                File.Copy(fil, target + fileName, true);
            }
        }

        public void CopyImageToUse(string datetime)
        {
            String source = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\TestAutomation\ImagesToUse";
            //String sourc = ConfigHelper.ImagesToUsePath();
            //String target = @""+ ConfigHelper.TestAutomationPath() + datetime + "\\";
            //String source = Path.GetFullPath(sourc).Replace(@"\\", @"\");
            String target = @"\\UQWDB023.qa.oec.local\\test\\ctsftp.gir2qc\\TestAutomation\\"+datetime+"\\";
            String[] files = Directory.GetFiles(source);
            int i;
            for (i = 1; i <= 1; i++)
            {

                //string fileName = Path.GetFileName(files[0]);
                string filePath = Path.GetFullPath(files[0]);
                string fileName = Path.GetFileName(files[0]);
                File.Copy(filePath, target + fileName, true);
                //File.Copy(fil, target + fileName, true);
            }
            //foreach (string fil in files)
            //{
            //    string fileName = Path.GetFileName(fil);
            //    File.Copy(fil, target + fileName, true);
            //}
        }

        public void RenamingFiles(string datetime,string result)
        {
            DirectoryInfo d = new DirectoryInfo(ConfigHelper.TestAutomationPath() + datetime+"\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.Replace("MN1Z5863805AA", result));
            }
        }

        public void RenamingFilesNonExistingPN(string datetime, string NonexistPartNumber)
        {
            //DirectoryInfo d = new DirectoryInfo(ConfigHelper.TestAutomationPath()+ datetime + "\\");
            DirectoryInfo d = new DirectoryInfo(@"\\UQWDB023.qa.oec.local\\test\\ctsftp.gir2qc\\TestAutomation\\"+ datetime+"\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.Replace("MN1Z5863805AA", NonexistPartNumber));
            }
        }

        public void RenamingFilesWithUsedPN(string datetime)
        {
            String resultAU = dbhelper.GetPartNumberAlreadyUsed()[0];
            DirectoryInfo d = new DirectoryInfo(ConfigHelper.TestAutomationPath() + datetime + "\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.Replace("MN1Z5863805AA", resultAU));
            }
        }

        public void ZipFolder(string datetime)
        {
            //ZipFile.CreateFromDirectory(ConfigHelper.TestAutomationPath() + datetime + "", ConfigHelper.TestAutomationPath() + datetime + ".zip");
            ZipFile.CreateFromDirectory(@"\\UQWDB023.qa.oec.local\\test\\ctsftp.gir2qc\\TestAutomation\\"+ datetime +"", @"\\UQWDB023.qa.oec.local\\test\\ctsftp.gir2qc\\TestAutomation\\"+ datetime + ".zip");

        }

        public void CopyZipFilesToGir2qc(string datetime)
        {
            //string sourceFile = ConfigHelper.TestAutomationPath() + datetime + ".zip";
            //string targetDir = ConfigHelper.GirPath() + datetime + ".zip";
            string sourceFile = @"\\UQWDB023.qa.oec.local\\test\\ctsftp.gir2qc\\TestAutomation\\"+datetime+".zip";
            string targetDir = @"\\UQWDB023.qa.oec.local\\test\\ctsftp.gir2qc\\"+ datetime+".zip";
            string tgtDirectory = Path.GetDirectoryName(targetDir);
            if (File.Exists(sourceFile) && Directory.Exists(tgtDirectory))
            {
                File.Copy(sourceFile, targetDir);
            }
        }

        

    }
}
