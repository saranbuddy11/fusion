using Microsoft.VisualStudio.TestTools.UnitTesting;
using OEC.Fusion.GlobalImageRepository.Helpers;
using OEC.Fusion.GlobalImageRepository.Specflow.Steps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository
{
    public class Common
    {

        DBHelper dBHelper = new DBHelper();
        public string imageResult = "";
        public string partNum = "";
        public string result = "";
        public string datetime = "";

        public Boolean ImageNotPresent(string result)
        {
            bool rslt = false;
            for (int i = 1; i <= 24; i++)
            {
                //Path of JPG anf JPEG files in Image directory

                string pathJPG = @"\\UQWDB023.qa.oec.local\Images\" + result + "-360-";
                string pathJPEG = @"\\UQWDB023.qa.oec.local\Images\" + result + "-360-";

                if (i < 10)
                {

                    //add 1 to 9 with the path and to change the integer from 1 to 01 upto 09
                    //1 => the number of precision

                    int decimalLength = i.ToString("D").Length + 1;
                    string prefix = i.ToString("D" + decimalLength.ToString());
                    pathJPG = pathJPG + prefix + ".jpg";
                    pathJPEG = pathJPEG + prefix + ".jpeg";
                    
                    try
                    {
                        bool a = (!File.Exists(pathJPG) && !File.Exists(pathJPEG));

                        //Images are not present in the Image directory

                        Assert.IsTrue(!File.Exists(pathJPG) || !File.Exists(pathJPEG));
                        rslt = false;
                    }
                    catch (Exception e)
                    {
                        rslt = true;
                        break;
                    }
                }
                else
                {
                    pathJPG = pathJPG + i + ".jpg";
                    pathJPEG = pathJPEG + i + ".jpeg";
                    try
                    {
                        Assert.IsTrue(!File.Exists(pathJPG) || !File.Exists(pathJPEG));
                        rslt = false;
                    }
                    catch (Exception e)
                    {
                        rslt = true;
                        break;
                    }
                }
            }
            return rslt;
        }

        public Boolean ImagePresent(string result)
        {


            bool rslt = true;
            for (int i = 1; i <= 24; i++)
            {
                string path = @"\\UQWDB023.qa.oec.local\Images\" + result + "-360-";
                if (i < 10)
                {

                    //To change the integer from 1 to 01 upto 09
                    //1 => the number of precision

                    int decimalLength = i.ToString("D").Length + 1;
                    string prefix = i.ToString("D" + decimalLength.ToString());
                    path = path + prefix + ".jpg";
                    try
                    {
                        Assert.IsTrue(File.Exists(path));
                        rslt = true;
                    }
                    catch (Exception e)
                    {
                        rslt = false;
                        break;
                    }
                }
                else
                {
                    path = path + i + ".jpg";
                    try
                    {
                        Assert.IsTrue(File.Exists(path));
                        rslt = true;
                    }
                    catch (Exception e)
                    {
                        rslt = false;
                        break;
                    }
                }
            }
            return rslt;
        }

        public Boolean ImageVerification(String result)
        {
            bool output = true;
            string expectedValue = "24";
            string res = dBHelper.VerifyImagesPresentInFolder(result)[0];
            Assert.AreEqual(expectedValue, res);
            return output;
        }

        public Boolean FileNotPresentInSftp(string datetime)
        {
            bool notPresent;
            string path = @"\\UQWDB023.qa.oec.local\test\ctsftp.gir2qc\";
            if(path.Contains(datetime))
            {
                notPresent = false;
            }
            else
            {
                notPresent = true;
            }
            return notPresent;
        }
    }
}



            
        

    

