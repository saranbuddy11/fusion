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
        FileOperations fileOp;
        private DBHelper dbhelper;
        private DBexeution dbe;
        private Common rsult;
        public string result = "";
        public string datetime = "";
        public string partNumAU = "";
        public string dateTimeUsedPN = "";
        public string NonexistPartNumber = "";
        ScenarioContext _scenarioContext;

        public GIRSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            dbhelper = new DBHelper();
            dbe = new DBexeution();
            rsult = new Common();
            fileOp = new FileOperations();
        }


        [Given(@"When I connect to the database")]
        public void GivenWhenIConnectToTheDatabase()
        {
            string db = ConfigHelper.GetDefaultConnection();
        }

        [When(@"I execute query to get the non used partNumber")]
        public void WhenIExecuteQueryToGetTheNonUsedPartNumber()
        {
            result = dbhelper.GetPartNumber();
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
            fileOp.CopyImagesToUse(datetime,24);
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
            partNumAU = dbhelper.GetPartNumberAlreadyUsed();
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

        [Then(@"Copy 25 image files from ImagesToUse folder to the newly created folder")]
        public void ThenCopyImageFilesFromImagesToUseFolderToTheNewlyCreatedFolder()
        {
            fileOp.CopyImagesToUse(datetime, 24);
            fileOp.RenamingFilesWithUsedPN(datetime);
            fileOp.CopyImageToUse(datetime);
        }

        [Then(@"Rename the files with already used partnumber in the format PN-360-01 to PN-360-25")]
        public void ThenRenameTheFilesWithAlreadyUsedPartnumberInTheFormatPNToPN()
        {
            fileOp.ReplacingWithUsedPartNumber(datetime, partNumAU);
        }

        [Then(@"Change the file extension from \.jpg to(.*)")]
        public void ThenChangeTheFileExtensionFrom_JpgTo_Bmp(string extension)
        {
            fileOp.ChangeExtention(datetime, extension,24);
        }

        [Then(@"Find sftp directory with different region to upload the zip file")]
        public void ThenFindSftpDirectoryWithDifferentRegionToUploadTheZipFile()
        {
            dbe.DirectoryToUploadTheZipFileDiffRegion();
        }

        [Then(@"Copy zip file to different region directory")]
        public void ThenCopyZipFileToDifferentRegionDirectory()
        {
            fileOp.CopyZipFilesTosftpPath(datetime);
        }
        [Then(@"Remove the file extension from .jpg to null")]
        public void ThenRemoveTheFileExtensionFrom_JpgToNull()
        {
            fileOp.RemovingExtensionFromjpg(datetime, 24);
        }

        [Then(@"Remove the file extension from .jpg to null only for one image file")]
        public void ThenRemoveTheFileExtensionFrom_JpgToNullOnlyForOneImageFile()
        {
            fileOp.RemovingExtensionFromjpg(datetime, 1);
        }

        [Then(@"Rename the files with non used partnumber in the format PN-360-01 to PN-360-25")]
        public void ThenRenameTheFilesWithNonUsedPartnumberInTheFormatPNToPN()
        {
            fileOp.ReplacingWithUsedPartNumber(datetime, result);
        }

        [Then(@"Copy 25 image files from ImagesToUse folder to the newly created folder with new partNumber")]
        public void ThenCopyImageFilesFromImagesToUseFolderToTheNewlyCreatedFolderWithNewPartNumber()
        {
            fileOp.CopyImagesToUse(datetime,24);
            fileOp.RenamingFiles(datetime, result);
            fileOp.CopyImageToUse(datetime);
        }
        [Then(@"Change the file extension for only one file from \.jpg to (.*)")]
        public void ThenChangeTheFileExtensionForOnlyOneFileFrom_JpgTo(string extension)
        {
            fileOp.ChangeExtention(datetime,extension, 1);
        }

        [Then(@"Copy 23 image files from ImagesToUse folder to the newly created folder")]
        public void ThenCopy23ImageFilesFromImagesToUseFolderToTheNewlyCreatedFolder()
        {
            fileOp.CopyImagesToUse(datetime,23);
        }

        [Then(@"Rename the files with already used partnumber in the format PN-360-01 to PN-360-23")]
        public void ThenRename23FilesWithAlreadyUsedPartnumberInTheFormatPNToPN()
        {
            fileOp.RenamingFilesWithUsedPN(datetime);
        }

    }
}
