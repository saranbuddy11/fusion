using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using System.Threading.Tasks;
using System.Threading;

namespace OEC.Fusion.GlobalImageRepository.Specflow.Steps
{
    [Binding]
    public class GIRSteps
    {
        public string StoragePath { get; private set; }
        FileOperations fileOp = new FileOperations();
        private DBHelper dbhelper = new DBHelper();
        private DBexeution dbe = new DBexeution();
        private Common rsult = new Common();
        public string result = "";
        public string datetime = "";
        public string partNumAU = "";
        public string dateTimeUsedPN = "";
        public string NonexistPartNumber = "";


        [Given(@"When I connect to the database")]
        public void GivenWhenIConnectToTheDatabase()
        {
            string db = ConfigHelper.GetDefaultConnection();
            string test = ConfigHelper.TestAutomationPath();
        }

        [When(@"I execute query to get the non used partNumber")]
        public void WhenIExecuteQueryToGetTheNonUsedPartNumber()
        {
            result = dbhelper.GetPartNumber()[0];
        }

        [Then(@"I verify the partnumber images are not present in Image directory")]
        public void ThenIVerifyThePartnumberImagesAreNotPresentInImageDirectory()
        {
            Assert.IsFalse(Convert.ToBoolean(rsult.ImageNotPresent(result)));
        }

        [Then(@"Create a folder in local directory with format payyyy-mm-dd_hhmmss")]
        public void ThenCreateAFolderInLocal()
        {
            //Pulls the server current date and time in the format payyyy-mm-dd_hhmmss
            datetime = dbhelper.GetCurDateTime()[0];
            fileOp.CreateFolderWithCurrentDateTime(datetime);
        }

        [Then(@"Copy 24 image files from ImagesToUse folder to the newly created folder")]
        public void CopyFilesFromPartsToNewFolder()
        {
            fileOp.CopyImagesToUse(datetime);
        }

        [Then(@"Rename the files with the partnumber in the format PN-360-01")]
        public void RenameFilesWithPartNumber()
        {
            fileOp.RenamingFiles(datetime, result);
        }

        [Then(@"Zip created folder")]
        public void ZipCreatedFolder()
        {
            fileOp.ZipFolder(datetime);
        }

        [Then(@"Find sftp directory to upload the zip file")]
        public void ThenFindTheSftpDirectoryToUploadTheZipFile()
        {
            dbe.DirectoryToUploadTheZipFile();
        }

        [Then(@"Copy zip file to the ctsftp.gir2qc directory")]
        public void CopyZipFilesToGir2qcFolder()
        {
            fileOp.CopyZipFilesToGir2qc(datetime);
        }

        [Then(@"Run spPRODDailyDownload procedure to Upload zip file in Image directory")]
        public void ThenRunSpPRODDailyDownloadProcedureToUploadZipFileInImageDirectory()
        {
            dbe.RunSpPRODDailyDownloadProcedure();
        }

        [Then(@"Verify uploaded images are present in the Image directory")]
        public void VerifyUploadedImagesPresentInImageDirectory()
        {
            Assert.IsFalse(Convert.ToBoolean(rsult.ImagePresent(result)));
        }

        [Then(@"Verify uploaded images are present in the Image folder using query")]
        public void ThenVerifyTheUploadedImagesIsPresentInTheImageDirectoryUsingQuery()
        {
            Assert.IsTrue(Convert.ToBoolean(rsult.ImageVerification(result)));
        }

        [Then(@"Verify the created zip file is removed from sftp path")]
        public void ThenVerifyTheZipFileRemovedFromSftpPath()
        {
            Assert.IsTrue(rsult.FileNotPresentInSftp(datetime));

        }

        [When(@"I execute query to get already used partNumber")]
        public void IExecuteQueryToGetAlreadyUsedPartNumber()
        {
            partNumAU = dbhelper.GetPartNumberAlreadyUsed()[0];
        }

        [Then(@"I verify the partnumber image files are present in Image directory")]
        public void ThenIVerifyThePartnumberImageFilesArePresentInImageDirectory()
        {
            Assert.IsFalse(Convert.ToBoolean(rsult.ImagePresent(partNumAU)));
        }

        [Then(@"Get the latest date and time attribute of the images present in Image directory")]
        public void ThenGetTheLatestDateAndTimeAttributeOfTheImagesPresentInImageDirectory()
        {
            dateTimeUsedPN = dbhelper.GetDateTimeOfUsedPN(partNumAU)[0];
        }

        [Then(@"Rename the files with already used partnumber in the format PN-360-01")]
        public void RenameFilesWithAUPartNumber()
        {
            fileOp.RenamingFiles(datetime, partNumAU);
        }

        [Then(@"Verify re-uploaded images are present in the Image directory")]
        public void VerifyReUploadedImagesPresentInImageDirectory()
        {
            Assert.IsFalse(Convert.ToBoolean(rsult.ImagePresent(partNumAU)));
        }

        [Then(@"Verify re-uploaded images are present in the Image folder using query")]
        public void ThenVerifyTheReUploadedImagesIsPresentInTheImageDirectoryUsingQuery()
        {
            Assert.IsTrue(Convert.ToBoolean(rsult.ImageVerification(partNumAU)));
        }

        [Then(@"verify the Date and time attribute of the newly uploaded images is greater than the old images")]
        public void ThenVerifyTheDateAndTimeAttributeOfTheNewlyUploadedImagesIsGreaterThanTheOldImages()
        {

            Assert.IsTrue(rsult.DateTimeVerification(dateTimeUsedPN, partNumAU));
        }

        [When(@"I execute query with non-existing partnumber and verify the partnumber does not exist in Images folder")]
        public void WhenIExecuteQueryWithNon_ExistingPartnumberAndVerifyThePartnumberDoesNotExistInImagesFolder()
        {
            rsult.GetNonExistingPN();
        }

        [Then(@"Verify the partnumber images are not present in Image directory")]
        public void ThenVerifyThePartnumberImagesAreNotPresentInImageDirectory()
        {
            rsult.ImageNotPresent(rsult.NonexistPartNumber);
        }

        [Then(@"Rename the files with the non-existing partnumber in the format PN-360-01")]
        public void ThenRenameTheFilesWithTheNon_ExistingPartnumberInTheFormatPN()
        {

            fileOp.RenamingFilesNonExistingPN(datetime, rsult.NonexistPartNumber);
        }

        [Then(@"Verify the partnumber images are not present in Image directory using Query")]
        public void ThenVerifyThePartnumberImagesAreNotPresentInImageDirectoryUsingQuery()
        {
            Assert.IsFalse(rsult.ImageVerification(NonexistPartNumber));
        }

        [Then(@"Copy only one image file from ImagesToUse folder to the newly created folder")]
        public void ThenCopyOnlyOneImageFileFromImagesToUseFolderToTheNewlyCreatedFolder()
        {
            fileOp.CopyImageToUse(datetime);
        }

        [Then(@"verify the Date and time attribute of the newly uploaded images is not greater than the old images")]
        public void ThenVerifyTheDateAndTimeAttributeOfTheNewlyUploadedImagesIsNotGreaterThanTheOldImages()
        {
            Assert.IsFalse(rsult.DateTimeVerification(dateTimeUsedPN, partNumAU));
        }

        [Then(@"Verify the partnumber images are present in Image directory")]
        public void ThenVerifyThePartnumberImagesArePresentInImageDirectory()
        {
            Assert.IsFalse(Convert.ToBoolean(rsult.ImagePresent(partNumAU)));
        }

        [Then(@"Verify the partnumber images are present in Image directory using Query")]
        public void ThenVerifyThePartnumberImagesArePresentInImageDirectoryUsingQuery()
        {
            Assert.IsTrue(Convert.ToBoolean(rsult.ImageVerification(partNumAU)));
        }

    }
}
