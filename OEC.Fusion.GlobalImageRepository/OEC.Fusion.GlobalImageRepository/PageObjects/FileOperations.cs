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

        public void CopyImagesToUse(string datetime, int count)
        {
            String source = ConfigHelper.ImagesToUsePath();
            String target = ConfigHelper.TestAutomationPath() + datetime + "\\";
            String[] files = Directory.GetFiles(source);
            foreach (string fil in files)
            {
                string fileName = Path.GetFileName(fil);
                if (count == 23)
                {
                    File.Copy(fil, target + fileName, true);
                    string last = fil.Substring(fil.Length - 6);
                    if (last.Equals("23.jpg"))
                    { break; }
                }
                else
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

        public void RenamingFiles(string datetime, string result)
        {
            DirectoryInfo d = new DirectoryInfo(ConfigHelper.TestAutomationPath() + datetime + "\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.Replace(ConfigHelper.ExistingFile(), result));
            }
        }

        public void RenamingFilesNonExistingPN(string datetime, string NonexistPartNumber)
        {
            DirectoryInfo d = new DirectoryInfo(ConfigHelper.TestAutomationPath() + datetime + "\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.Replace(ConfigHelper.ExistingFile(), NonexistPartNumber));
            }
        }

        public void RenamingFilesWithUsedPN(string datetime)
        {
            String resultAU = dbhelper.GetPartNumberAlreadyUsed();
            DirectoryInfo d = new DirectoryInfo(ConfigHelper.TestAutomationPath() + datetime + "\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.Replace(ConfigHelper.ExistingFile(), resultAU));
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

        public void ChangeExtention(string datetime, string extension, int count)
        {
            DirectoryInfo d = new DirectoryInfo(ConfigHelper.TestAutomationPath() + datetime + "\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                if (count == 1)
                {
                    var f1 = infos[0];
                    File.Move(f1.FullName, f1.FullName.Replace(".jpg", extension));
                    break;
                }
                File.Move(f.FullName, f.FullName.Replace(".jpg", extension));
            }
        }

        public void RemovingExtensionFromjpg(string datetime, int count)
        {
            DirectoryInfo d = new DirectoryInfo(ConfigHelper.TestAutomationPath() + datetime + "\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                if (count == 1)
                {
                    var f1 = infos[0];
                    File.Move(f1.FullName, f1.FullName.Replace(".jpg", null));
                    break;
                }
                File.Move(f.FullName, f.FullName.Replace(".jpg", null));
            }
        }

        public void ReplaceDataByte(string filename,int position, byte[] data)
        {
            using Stream stream = File.Open(filename, FileMode.Open);
            stream.Position = position;
            stream.Write(data, 0, data.Length);
        }
    }
}
