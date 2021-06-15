﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OEC.Fusion.GlobalImageRepository.Specflow.Steps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository
{
    public class Common
    {   
        public Boolean ImageNotPresent(string result)
        {
            Console.WriteLine(result);
            bool rslt = false;
            for (int i = 1; i <= 24; i++)
            {
                
                string path = @"\\UQWDB023.qa.oec.local\Images\" + result + " -360-";
                if (i < 10)
                {
                    //To change the integer from 1 to 01 upto 09
                    //1 => the number of precision
                    int decimalLength = i.ToString("D").Length + 1;
                    string prefix = i.ToString("D" + decimalLength.ToString());
                    path = path + prefix + ".jpg";
                    try
                    {
                        Assert.IsTrue(!File.Exists(path));
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
                    path = path + i + ".jpg";
                    try
                    {
                        Assert.IsTrue(!File.Exists(path));
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
                string path = @"\\UQWDB023.qa.oec.local\Images\" + result + " -360-";
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

    }
}
