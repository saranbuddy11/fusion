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
        public string NonexistPartNumber = "";

        public Boolean ImageVerification(string result, string image)
        {
            bool rslt = false;
            for (int i = 1; i <= 24; i++)
            {
                string pathJPG = ConfigHelper.ImagesPath() + result + "-360-";
                string pathJPEG = ConfigHelper.ImagesPath() + result + "-360-";
                if (i < 10)
                {
                    int decimalLength = i.ToString("D").Length + 1;
                    string prefix = i.ToString("D" + decimalLength.ToString());
                    pathJPG = pathJPG + prefix + ".jpg";
                    pathJPEG = pathJPEG + prefix + ".jpeg";
                    if (image.Equals("ImageNotPresent"))
                    {
                        Assert.IsTrue(!File.Exists(pathJPG) && !File.Exists(pathJPEG));
                        rslt = false;
                    }
                    else
                    {
                        Assert.IsTrue(File.Exists(pathJPG) || File.Exists(pathJPEG));
                        rslt = true;
                    }
                }
                else
                {
                    pathJPG = pathJPG + i + ".jpg";
                    pathJPEG = pathJPEG + i + ".jpeg";
                    if (image.Equals("ImageNotPresent"))
                    {
                        Assert.IsTrue(!File.Exists(pathJPG) && !File.Exists(pathJPEG));
                        rslt = false;
                    }
                    else
                    {
                        Assert.IsTrue(File.Exists(pathJPG) || File.Exists(pathJPEG));
                        rslt = true;
                    }
                }
            }
            return rslt;
        }

        public Boolean ImageVerificationQuery(String result)
        {
            bool output;
            string expectedValue = "24";
            string res = dBHelper.VerifyImagesPresentInFolder(result);
            if (expectedValue.Equals(res))
            {
                output = true;
            }
            else
                output = false;
            return output;
        }

        public Boolean ProperImageVerification(string result)
        {
            bool output;
            string expectedValue = "1";
            string res = dBHelper.VerifyProperImagesPresentInFolder(result);
            if (expectedValue.Equals(res))
            {
                output = true;
            }
            else
                output = false;
            return output;
        }

        public Boolean FileNotPresentInSftp(string datetime)
        {
            bool notPresent;
            string path = ConfigHelper.GirPath();
            if (path.Contains(datetime))
            {
                notPresent = false;
            }
            else
            {
                notPresent = true;
            }
            return notPresent;
        }

        public Boolean DateTimeVerification(string dateTimeUsedPN, string partNumAU)
        {
            bool dateTimeAttribute;
             String reUploadedDateTime = dBHelper.GetDateTimeOfUsedPN(partNumAU)[0];
            //return 1 if 1st value is greater than the second value

            if(string.Compare(reUploadedDateTime,dateTimeUsedPN) == 1)
            {
                dateTimeAttribute = true;
            }
            
            else
                dateTimeAttribute = false;
            return dateTimeAttribute;
        }

       /* public void GetNonExistingPN()
        {
            NonexistPartNumber = ConfigHelper.NonExistingPartNo();
            //Assert.IsTrue(dBHelper.VerifyNonExistingPN().Equals("0"));
        }*/

        public Boolean NormalImagePresent(string result, string properImageView)
        {
            bool rslt = false;
                string path = ConfigHelper.ImagesPath() + result + "-" + properImageView +".jpg";
                string pathJPEG = ConfigHelper.ImagesPath() + result + "-" + properImageView + ".jpeg";
                try {
                        Assert.IsTrue(File.Exists(path) || File.Exists(pathJPEG));
                        rslt = false;
                    }
                catch (Exception e)
                    {
                        rslt = true;
                    }
            return rslt;
        }

        public Boolean NormalImageNotPresent(string result, string properImageView)
        {
            bool rslt = false;
            string path = ConfigHelper.ImagesPath() + result + "-" + properImageView + ".jpg";
            string pathJPEG = ConfigHelper.ImagesPath() + result + "-" + properImageView + ".jpeg";
            try
            {
                Assert.IsTrue(!File.Exists(path) || !File.Exists(pathJPEG));
                rslt = false;
            }
            catch (Exception e)
            {
                rslt = true;
            }
            return rslt;
        }


        
    }
}




            
        

    

