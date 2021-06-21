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

            String dir = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string subdir = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\" + dateTime + "";
            Directory.CreateDirectory(subdir);
        }

        public void CopyImagesToUse(string datetime)
        {   
            String source = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\ImagesToUse";
            String target = @"\\UQWDB023.qa.oec.local\\test\\ctsftp.gir2qc\\aiswarya\\"+ datetime +"\\";
            String[] files = Directory.GetFiles(source);
            foreach (string fil in files)
            {
                string fileName = Path.GetFileName(fil);
                File.Copy(fil, target + fileName, true);
            }
        }

        public void RenamingFiles(string datetime)
        {
            //string datetime = dbhelper.getCurDateTime()[0];
            String result = dbhelper.GetPartNumber()[0];
            Console.WriteLine(result);
            DirectoryInfo d = new DirectoryInfo(@"\\UQWDB023.qa.oec.local\\test\\ctsftp.gir2qc\\aiswarya\\"+datetime+"\\");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.Replace("MN1Z5863805AA", result));
            }
        }
        public void ZipFolder(string datetime)
        {
            //string datetime = dbhelper.getCurDateTime()[0];
            ZipFile.CreateFromDirectory(@"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\" + datetime + "", @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\" + datetime + ".zip");
        }

        public void CopyZipFilesToGir2qc(string datetime)
        {
            //string datetime = dbhelper.getCurDateTime()[0];
            String src = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\aiswarya\" + datetime + ".zip";
            String tgt = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\" + datetime + ".zip";

            string tgtDirectory = Path.GetDirectoryName(tgt);
            if (File.Exists(src) && Directory.Exists(tgtDirectory))
            {
                File.Copy(src, tgt);
            }
        }


    }
}
