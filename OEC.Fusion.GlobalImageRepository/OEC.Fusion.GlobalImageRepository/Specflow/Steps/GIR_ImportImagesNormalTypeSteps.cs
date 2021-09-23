using Microsoft.VisualStudio.TestTools.UnitTesting;
using OEC.Fusion.GlobalImageRepository.Helpers;
using OEC.Fusion.GlobalImageRepository.PageObjects;
using OEC.Fusion.GlobalImageRepository.Specflow.Steps;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OEC.Fusion.GlobalImageRepository.Specflow.Steps
{
    [Binding]
    public class GIR_ImportImagesNormalTypeSteps 
    {
        FileOperations fileOp;
        private DBHelper dbhelper;
        private DBexeution dbe;
        private Common rsult;
        public string result;
        public string result1;
        public string datetime;
        public string partNumAU = "";
        public string dateTimeUsedPN = "";
        public string NonexistPartNumber = "";
        string NormalImage = "";
        private byte[] data = { 1, 2, 3, 5 };
        ScenarioContext _scenarioContext;

        public GIR_ImportImagesNormalTypeSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            dbhelper = new DBHelper();
            dbe = new DBexeution();
            rsult = new Common();
            fileOp = new FileOperations();
        }


        [Then(@"Rename the Image file with the non used partnumber and (.*) in the format PN-ProperImageView")]
        public void ThenRenameTheImageFileWithTheNonUsedPartnumberAndInTheFormatPN_ProperImageView(string properImageView)
        {
            string result1 =(string)ScenarioContext.Current["_result"];
            string datetime = (string)ScenarioContext.Current["_datetime"];
            fileOp.RenameProperImage(properImageView, datetime, result1);
        }

        [Then(@"Verify uploaded proper images are present in the Image directory with (.*)")]
        public void ThenVerifyUploadedProperImagesArePresentInTheImageDirectoryWith(string properImageView)
        {
            string result1 = (string)ScenarioContext.Current["_result"];
            string NormalImage = ConfigHelper.NormalImagePresent();
            rsult.NormalImageVerification(result1, properImageView, NormalImage);
        }

        [Then(@"Verify uploaded proper images are present in the Image folder using query with (.*)")]
        public void ThenVerifyUploadedProperImagesArePresentInTheImageFolderUsingQueryWithLIF(string properImageView)
        {
            string result1 = (string)ScenarioContext.Current["_result"];
            Assert.IsTrue(rsult.ProperImageVerification(result1));
        }


        [Then(@"Rename the Image file with the partnumber and (.*) in the format PN-NewImageView")]
        public void ThenRenameTheImageFileWithThePartnumberAndInTheFormatPN_NewImageView(string NewImageView)
        {
            string result = (string)ScenarioContext.Current["_result"];
            string datetime = (string)ScenarioContext.Current["_datetime"];
            fileOp.RenameProperImage(NewImageView,datetime,result);
        }

        [Then(@"Rename the Image file with the non-used partnumber and (.*) in the format PN-NewImageView")]
        public void ThenRenameTheImageFileWithTheNon_UsedPartnumberAndLIInTheFormatPN_NewImageView(string ImproperImageView)
        {
            string result = (string)ScenarioContext.Current["_result"];
            string datetime = (string)ScenarioContext.Current["_datetime"];
            fileOp.RenameProperImage(ImproperImageView, datetime, result);
        }

        [Then(@"Verify uploaded Improper images are not present in the Image directory with (.*)")]
        public void ThenVerifyUploadedImproperImagesAreNotPresentInTheImageDirectoryWithLI(string ImproperImageView)
        {
            string result = (string)ScenarioContext.Current["_result"];
            string NormalImage = ConfigHelper.NormalImageNotPresent();
            rsult.NormalImageVerification(result, ImproperImageView, NormalImage);
        }

        [Then(@"Verify uploaded Improper images are not present in the Image folder using query")]
        public void ThenVerifyUploadedImproperImagesAreNotPresentInTheImageFolderUsingQuery()
        {
            string result = (string)ScenarioContext.Current["_result"];
            dbhelper.VerifyProperImagesPresentInFolder(result);
        }

        [Then(@"Verify uploaded Improper images are not present in the Image folder using query with (.*)")]
        public void ThenVerifyUploadedImproperImagesAreNotPresentInTheImageFolderUsingQueryWithLI(string ImproperImageView)
        {
            string result = (string)ScenarioContext.Current["_result"];
            string result2 = result + "-" + ImproperImageView;
            Assert.IsFalse(rsult.ProperImageVerification(result2));
        }
    }
}