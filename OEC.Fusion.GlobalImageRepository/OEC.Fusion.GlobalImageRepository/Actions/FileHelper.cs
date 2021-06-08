using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository.Actions
{
    public class FileHelper
    {
        public static string subdir;
        //To get the current date and time
        public static string getCurDate() 
        {
          DateTime now = DateTime.Now;
          string datetime = now.ToString("ddMMyyyyHHmmss");
          return datetime;
        }
        
        //To create a folder with the current date and time name format
        public static string CreateFolderWithCurrentDate(string datetime)
        {
            string subdir = @"C:\Users\arameshbaabu\Desktop\GIR\Local\Aiswarya\pa[" + datetime + "]";
            return subdir;
        }

       

        /*public static void CopyFiles(string sourcePath, string targetPath)
        {
            DirectoryInfo di = new DirectoryInfo(sourcePath);

            foreach (FileInfo file in di.GetFiles())
            {
                var filePath = Path.Combine(targetPath, file.Name);
                file.CopyTo(filePath, true);

                if (!File.Exists(filePath))
                  //  Log.Error("File was not copied!");
            }
        
    }*/

    }
}
