using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository.Helpers
{
    public class Outlook
    {
        //Sub: Global Image Repository - Upload Part Attribute(s) data not available.
        //From: ford-datadstr@clifford-thames.com
        //body:Dear Ford Admin,
        //Part attribute data is not available for part numbers listed in the attached CSV file.
        //Ford Image Repository Team


        //Sub: Images Successfully loaded into the repository
        //From: Data Distribution <ford-datadstr@clifford-thames.com>
        //Body: Dear Ford Admin, 
        /*Zip File Name = pa09062021172720.zip
        Upload Username = GIR2CC
        Number of images loaded = 24 
        Region = NA
        Thank you,
        Ford Data Distribution Team.*/

        public string EmailFrom { get; set; }

        public string EmailSub { get; set; }

        public string EmailBody { get; set; }

      /*  public List<Outlook> ReadMailItems()
        {
            Application outlookApplication = null;
            NameSpace outlookNamespace = null;
            //Messaging application programming interface
            MAPIFolder inboxFolder = null;

            Items mailItems = null;
            List<Outlook> listEmailDetails = new List<Outlook>();
            Outlook emailDetails;

            try
            {
                outlookApplication = new Application();
                outlookNamespace = outlookApplication.GetNamespace("MAPI");
                inboxFolder = outlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                mailItems = inboxFolder.Items;

                 foreach (MailItem item in collection)
                 {

                 }
             }
             catch (System.Exception ex)
             {

                 Console.WriteLine(ex.Message);
             }
             */



            
        
    }
}
