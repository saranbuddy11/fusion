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
        
        
        private static FindItemsResults<Item> email;
        private static PropertySet properties;
        private static EmailMessage item;

        public static void ReadEmail(string datetime)
        {
            ExchangeService _service;

            try
            {
                //Console.WriteLine("Registering Exchange connection");

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

            // Prepare seperate class for writing email to the database
            try
            {
                string sub = "";
                string body = "";
                //for (int i = 0; i <= 5; i++)
                //{
                //string subject = "Test Outlook Read Email";
                //ItemView view = new ItemView(5);

                //FindItemsResults<Item> findResults;
                //List<EmailMessage> emails = new List<EmailMessage>();
                //PropertySet properties = new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.TextBody);

                //findResults = _service.FindItems(WellKnownFolderName.Inbox, view);
                //view.PropertySet = properties;
                //_service.LoadPropertiesForItems(findResults, properties);
                //emails.Add((EmailMessage)item);
                //sub = item.ConversationTopic;

                //Console.WriteLine(sub);
                //if (subject.Equals(sub))
                //{
                //Console.WriteLine("yes");
                //}
                //sub = emails.ConversationTopic;
                //}
                foreach (EmailMessage email in _service.FindItems(WellKnownFolderName.Inbox, new ItemView(5)))
                {
                    email.Load(new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.TextBody));
                    //a = email.TextBody;
                    string subject = "Images Successfully loaded into the repository";
                    sub = email.ConversationTopic;

                    while (subject.Equals(sub))
                    {
                        try
                        {
                            Console.WriteLine(sub);
                            body = email.Body;
                            if (body.Contains(datetime + ".zip"))

                            {
                                Console.WriteLine("All 24 Images imported properly");
                            }
                            else if (body.Contains("Global Image Repository - Image Import Failed"))
                            {
                                Console.WriteLine("Image Import Failed");

                            }
                            break;
                        }

                        catch (Exception e)
                        {
                            Console.WriteLine(e);

                        }
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

