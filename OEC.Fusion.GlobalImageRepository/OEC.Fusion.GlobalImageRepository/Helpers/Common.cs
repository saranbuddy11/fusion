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
        public Boolean ImageNotPresent(string result)
        {
            Console.WriteLine(result);
            bool rslt = false;
            for (int i = 1; i <= 24; i++)
            {

                string pathJPG = @"\\UQWDB023.qa.oec.local\Images\" + result + "-360-";
                string pathJPEG = @"\\UQWDB023.qa.oec.local\Images\" + result + "-360-";
                if (i < 10)
                {
                    //To change the integer from 1 to 01 upto 09
                    //1 => the number of precision
                    int decimalLength = i.ToString("D").Length + 1;
                    string prefix = i.ToString("D" + decimalLength.ToString());
                    pathJPG = pathJPG + prefix + ".jpg";
                    pathJPEG = pathJPEG + prefix + ".jpeg";

                    try
                    {
                        bool a = (!File.Exists(pathJPG) && !File.Exists(pathJPEG));
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
            //String path =dBHelper.getsFTPPath()[0];
            //String expVal;
            //string  = value.ToString;
            //int res;
            //String expectedValue = "MM1PZ16A550BA-360-02";
            string res = dBHelper.VerifyImagesPresentInFolder(result)[0];
            Assert.AreEqual(expectedValue, res);
            return output;
        }
    }
}



            /*res = dBHelper.VerifyImagesPresentInFolder();
            List<String> resVal = dBHelper.VerifyImagesPresentInFolder();
            for (int i=1; i<=24;i++)
            {
                {
                    partNum = dBHelper.GetPartNumber()[0];
            
                    //partNum = "MM1PZ16A550BA";
                    expectedValue = partNum + "-360";
                    if (i < 10)//1
                    {
                        int decimalLength = i.ToString("D").Length + 1;
                        String prefix = i.ToString("D" + decimalLength.ToString());
                        expVal = expectedValue + "-" + prefix; //01
                        res = resVal[i - 1]; //[0 - 01],
                        Assert.AreEqual(expVal, res);
                    }
                    else
                    {
                        expVal = expectedValue + "-" + i; // 10, i= 24
                        res = resVal[i - 1];// [9 - 10], [23 - 24]
                        try
                        {
                            Assert.AreEqual(expVal, res);
                            output = true;
                        }
                        catch (Exception e)
                        {
                            output = false;
                            break;
                        }
                        
                    }
                }
            }

            //String expectedValue = "MM1PZ16A550BA-360-02";
             //res=dBHelper.VerifyImagesPresentInFolder();
            //Assert.AreEqual(expectedValue, res);
            return output;*/
        

    

