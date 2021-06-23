
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using Application = Microsoft.Office.Interop.Outlook.Application;

namespace OEC.Fusion.GlobalImageRepository.Helpers
{
    public class Outlook
    {
        //Sub: Images Successfully loaded into the repository
        //From: Data Distribution <ford-datadstr@clifford-thames.com>
        //Body: Dear Ford Admin, 
        //Zip File Name = pa09062021172720.zip
        //Upload Username = GIR2CC
        //Number of images loaded = 24 
        //Region = NA
        //Thank you,
        //Ford Data Distribution Team.

        public string EmailFrom { get; set; }

        public string EmailSub { get; set; }

        public string EmailBody { get; set; }

        public List<Outlook> ReadMailItems()
        {
            /*Microsoft.Office.Interop.Outlook.Application outlookApplication = null;
            Namespace outlookNamespace = null;
            //Messaging application programming interface
            MAPIFolder inboxFolder = null;
            Items mailItems = null;
            List<Outlook> listEmailDetails = new List<Outlook>();
            Outlook emailDetails;*/

            Application outlookApplication = null;
            NameSpace outlookNamespace = null;
            MAPIFolder inboxFolder = null;
            Items mailItems = null;

            try
            {
                outlookApplication = new Application();
                outlookNamespace = outlookApplication.GetNamespace("MAPI");
                inboxFolder = outlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                mailItems = inboxFolder.Items;
                Console.WriteLine(mailItems.Count);

                foreach (MailItem item in mailItems)
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("From: " + item.SenderEmailAddress);
                    //stringBuilder.AppendLine("To: " + item.To);
                    //stringBuilder.AppendLine("CC: " + item.CC);
                    //stringBuilder.AppendLine("");
                    //stringBuilder.AppendLine("Subject: " + item.Subject);
                    //stringBuilder.AppendLine(item.Body);
                    Console.WriteLine(stringBuilder);

                    Marshal.ReleaseComObject(item);
                }
            }

            catch (System.Exception e)
            {

                Console.WriteLine("{0} Exception caught: ", e);
            }
            finally
            {
                ReleaseComObject(mailItems);
                ReleaseComObject(inboxFolder);
                ReleaseComObject(outlookNamespace);
                ReleaseComObject(outlookApplication);
            }
            Console.WriteLine("OK");
            Console.ReadKey();
            return ReadMailItems();
        }

        private static void ReleaseComObject(object obj)
        {
            if (obj != null)
            {
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                Marshal.ReleaseComObject(obj);
                obj = null;
            }

        }
    }
    }

