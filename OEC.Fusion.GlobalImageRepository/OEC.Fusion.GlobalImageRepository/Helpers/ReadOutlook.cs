using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using System.Data.SqlClient;

namespace OEC.Fusion.GlobalImageRepository.Helpers
{
    public class ReadOutlook
    {
        
        public static void ReadEmail()
        {
            ExchangeService _service;

            try
            {
                Console.WriteLine("Registering Exchange connection");

                _service = new ExchangeService
                {
                    Credentials = new WebCredentials("arameshbaabu@oeconnection.com", "Thaangam@1")
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
                //Write2DB db = new Write2DB();

                Console.WriteLine("Reading mail");
                String a = "";
                // Read 100 mails
                foreach (EmailMessage email in _service.FindItems(WellKnownFolderName.Inbox, new ItemView(5)))
                {
                    //db.Save(email);
                    Console.WriteLine(email);
                    email.Load(new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.TextBody));
                    a = email.TextBody;
                }

                Console.WriteLine("Exiting");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occured. \n:" + e.Message);
                
            }
            
        }
        
    }
}
