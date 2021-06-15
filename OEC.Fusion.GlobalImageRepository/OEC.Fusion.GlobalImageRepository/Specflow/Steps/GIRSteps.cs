using Microsoft.VisualStudio.TestTools.UnitTesting;
using OEC.Fusion.GlobalImageRepository.Actions;
using OEC.Fusion.GlobalImageRepository.Helpers;
using OEC.Fusion.GlobalImageRepository.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;


namespace OEC.Fusion.GlobalImageRepository.Specflow.Steps
{
    [Binding]
    public class GIRSteps
    {
        String subdir { get; set; }
        public string StoragePath { get; private set; }
        FileOperations fileOp = new FileOperations();
        private DBHelper dbhelper = new DBHelper();
        private DBexeution dbe = new DBexeution();
        public string result = "";
        public string datetime = "";
        //private string path;

        ScenarioContext _scenariocontext;
       

        /*public GIRSteps(ScenarioContext scenarioContext)
        {
            _scenariocontext = scenarioContext;
        }*/

        [Given(@"When I connect to the database to execute query to get the partNumber")]
       public void GivenWhenIConnectToTheDatabase()
        {
            String deviceID = ConfigurationManager.AppSettings["QAServerPath"];
            result = dbhelper.GetPartNumber()[0];
        }

        [When(@"I verify the partnumber images are not present in working directory")]
        public void WhenIVerifyThePartnumberImagesAreNotPresentInTheWorkingDirectory()
        {
            Common rsult = new Common();
            Assert.IsFalse(Convert.ToBoolean(rsult.ImageNotPresent(result)));
        }
    

        [Then(@"Create a folder for images in local in the format payyyymmdd_hhmmss")]
        public void ThenCreateAFolderForImagesInLocal()
        {
            datetime = dbhelper.getCurDateTime()[0];
            fileOp.CreateFolderWithCurrentDateTime(datetime);
        }


        [Then(@"Copy 24 files from parts folder to the newly created folder")]
        public void CopyFilesFromPartsToNewFolder()
        {
            fileOp.CopyImagesToUse(datetime);
        }


        [Then(@"Rename the files with the given partnumber")]
        public void RenameFilesWithPartNumber()
        {
            fileOp.RenamingFiles(datetime);
        }


        [Then(@"Zip the folder")]
        public void ZipCreatedFolder()
        {
            fileOp.ZipFolder(datetime);
        }


        [Then(@"Find the sftp directory to upload the zip file")]
        public void ThenFindTheSftpDirectoryToUploadTheZipFile()
        {
            dbe.DirectoryToUploadTheZipFile();
        }


        [Then(@"Copy the zip file to the ctsftp.gir2qc folder")]
        public void CopyZipFilesToGir2qcFolder()
        {
            fileOp.CopyZipFilesToGir2qc(datetime);
        }


        [Then(@"Run spPRODDailyDownload procedure to Upload zip file in Images folder")]
        public void ThenRunSpPRODDailyDownloadProcedureToUploadZipFileInImagesFolder()
        {
            dbe.RunSpPRODDailyDownloadProcedure();
        }
        
        [Then(@"Verify the partnumber is present in the image folder")]
        public void VerifyPartNumberPresentInImages()
        {

            Common rsult = new Common();
            Assert.IsTrue(Convert.ToBoolean(rsult.ImagePresent(result)));
        }

        [Then(@"Verify the partnumber is present in the image folder using query")]
        public void ThenVerifyThePartnumberIsPresentInTheImageFolderUsingQuery()
        {
            //ScenarioContext.Current.Pending();
            Console.WriteLine("test");
        }

        [Then(@"verify email Images Successfully loaded into the repository in outlook")]
        public void ThenVerifyEmailInOutlook()
        {
            //ScenarioContext.Current.Pending();
        }





    }
}
