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

        [Then(@"Rename the Image file with CurlImage partnumber and (.*) in the format PN-NewImageView")]
        public void ThenRenameTheImageFileWithCurlImagePartnumberAndLIFInTheFormatPN_NewImageView(string NewImageView)
        {
            string datetime = (string)ScenarioContext.Current["_datetime"];
            fileOp.RenameProperImage(NewImageView, datetime, result);
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
            string NormalImage = ConfigHelper.NormalImageNotPresent();
            Assert.IsTrue(rsult.NormalImageVerification(result2, ImproperImageView, NormalImage));
        }

        [Given(@"I execute query partNumber with (.*) and Primary View = 1")]
        public void GivenIExecuteQueryPartNumberWithFROAndPrimaryView(string CurlImageView)
        {
            result = dbhelper.GetPartNumberCurlImageView(CurlImageView);
        }
        
        [When(@"Verify the partnumber images are present in Image directory with (.*)")]
        public void WhenVerifyThePartnumberImagesArePresentInImageDirectoryWithFRO(string CurlImageView)
        {
            string NormalImage = ConfigHelper.NormalImagePresent();
            Assert.IsFalse(rsult.NormalImageVerification(result, CurlImageView, NormalImage));
        }


        [Then(@"Verify uploaded proper images are not present in the Image directory with (.*)")]
        public void ThenVerifyUploadedProperImagesAreNotPresentInTheImageDirectoryWithLIF(string ExpectedImageView)
        {
            string NormalImage = ConfigHelper.NormalImageNotPresent();
            Assert.IsTrue(rsult.NormalImageVerification(result, ExpectedImageView, NormalImage));
        }

        [Then(@"Verify the number of Primary views for the image using query")]
        public void ThenVerifyTheNumberOfPrimaryViewsForTheImageUsingQuery()
        {
            Assert.AreEqual(dbhelper.GetNoOfPrimaryViews(result), ConfigHelper.NoOfPrimaryImageViews());
        }

        [Then(@"Verify the expected Image view using query with (.*)")]
        public void ThenVerifyTheExpectedImageViewUsingQueryWithLIF(string ExpectedImageView)
        {
            dbe.VerifyExpectedViewImage(ExpectedImageView, result);
        }

        [Then(@"Verify the Other view for the image using query with (.*)")]
        public void ThenVerifyTheOtherViewForTheImageUsingQueryWithLIF(string ExpectedImageView)
        {
            dbe.VerifyOtherImage(ExpectedImageView, result);
        }

    }
}