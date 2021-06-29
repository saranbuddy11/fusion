using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OEC.Fusion.GlobalImageRepository.Helpers
{
    public class ReadOutlook
    {
        public string datetime = "";

        public static void ReadEmail(string datetime)
        {
            ExchangeService _service;
            try
            {
                 _service = new ExchangeService
                {
                    Credentials = new WebCredentials(ConfigHelper.GetEmail(), ConfigHelper.GetPassword())
                };
            }
            catch
            {
                Console.WriteLine("new ExchangeService failed. Press enter to exit:");
                return;
            }

            // This is the office365 webservice URL
            _service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");

            //Verifying email subject and content

            try
            {
                string sub = "";
                string body = "";
                int x = 0;
                foreach (EmailMessage email in _service.FindItems(WellKnownFolderName.Inbox, new ItemView(5)))
                {
                    email.Load(new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.TextBody));
                    string subject = "Images Successfully loaded into the repository";
                    sub = email.ConversationTopic;
                    
                    if(subject.Equals(sub))
                    {
                        body = email.Body;
                        if (body.Contains(datetime + ".zip"))
                        {
                            Assert.IsTrue(body.Contains("Upload Username = GIR2CC"));
                            Assert.IsTrue(body.Contains("Number of images loaded = 24"));
                            Assert.IsTrue(body.Contains("Region = NA"));                            
                            break;
                        }
                    }
                    else
                    {
                        if (subject.Contains("Global Image Repository - Image Import Failed"))
                        {
                            if (body.Contains("Unable to upload image(s)"))
                            Assert.Fail("Images upload failed");                            
                        }
                        x++;
                        if(x==5)
                        Assert.Fail("No email received after upload");                        
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        } 
    }
}

