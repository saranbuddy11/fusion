using OEC.Fusion.GlobalImageRepository.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository.PageObjects
{
    public class FileOperations : DBHelper
    {
        private DBHelper dbhelper = new DBHelper();
        public string datetime = "";
        private string targetDir;

        public void CreateFolderWithCurrentDateTime(string dateTime)
        {            
            String dir = ConfigHelper.TestAutomationPath();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = ConfigHelper.TestAutomationPath();
            string subdir1 = path + dateTime;
            Directory.CreateDirectory(subdir1);
        }

        public void CopyImagesToUse(string datetime)
        {
            String source = ConfigHelper.ImagesToUsePath();
            String target = ConfigHelper.TestAutomationPath() + datetime +"\\";
            String[] files = Directory.GetFiles(source);
            foreach (string fil in files)
            {
                string fileName = Path.GetFileName(fil);
                File.Copy(fil, target + fileName, true);
            }
        }

        public void CopyImageToUse(string datetime)
        {
            String source = ConfigHelper.ImagesToUsePath();
            string target = @""+ ConfigHelper.TestAutomationPath() + datetime + "\\";
            String[] files = Directory.GetFiles(source);
            int i;
            for (i = 1; i <= 1; i++)
            {

                string filePath = Path.GetFullPath(files[0]);
                string fileName = Path.GetFileName(files[0]);
                File.Copy(filePath, target + fileName, true);
            }
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
            DirectoryInfo d = new DirectoryInfo(ConfigHelper.TestAutomationPath()+ datetime + "\\");
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

        public void ReplacingWithUsedPartNumber(string datetime, string resultAU)
        {
            DirectoryInfo d = new DirectoryInfo(ConfigHelper.TestAutomationPath() + datetime + "\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.Replace(ConfigHelper.LastImage(), resultAU + "-360-25"));
            }
        }

        public void ZipFolder(string datetime)
        {
            ZipFile.CreateFromDirectory(ConfigHelper.TestAutomationPath() + datetime + "", ConfigHelper.TestAutomationPath() + datetime + ".zip");

        }

        public void CopyZipFilesToGir2qc(string datetime)
        {
            string sourceFile = ConfigHelper.TestAutomationPath() + datetime + ".zip";
            string targetDir = ConfigHelper.GirPath() + datetime + ".zip";
            string tgtDirectory = Path.GetDirectoryName(targetDir);
            if (File.Exists(sourceFile) && Directory.Exists(tgtDirectory))
            {
                File.Copy(sourceFile, targetDir);
            }
        }

        public void CopyZipFilesTosftpPath(string datetime)
        {
            string sourceFile = ConfigHelper.TestAutomationPath() + datetime + ".zip";
            string targetDir = ConfigHelper.DifferentRegion() + datetime + ".zip";
            string tgtDirectory = Path.GetDirectoryName(targetDir);
            if (File.Exists(sourceFile) && Directory.Exists(tgtDirectory))
            {
                File.Copy(sourceFile, targetDir);
            }
        }

        public void ChangeExtentionTobmp(string datetime)
        {
            DirectoryInfo d = new DirectoryInfo(ConfigHelper.TestAutomationPath() + datetime + "\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.Replace(".jpg", ".bmp"));
            }
        }
    }
}
