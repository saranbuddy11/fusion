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
            rsult.NormalImagePresent(result1, properImageView);
        }

        [Then(@"Verify uploaded proper images are present in the Image folder using query with (.*)")]
        public void ThenVerifyUploadedProperImagesArePresentInTheImageFolderUsingQueryWithLIF(string properImageView)
        {
            string result1 = (string)ScenarioContext.Current["_result"];
            string result2 = result1 + "-" + properImageView;
            Assert.IsTrue(rsult.ProperImageVerification(result2));
        }

        [When(@"I execute query to get the partNumber with (.*)")]
        public void WhenIExecuteQueryToGetThePartNumberWith(string CurlImageView)
        {
            string result = dbhelper.GetProperImageNumber(CurlImageView);
            ScenarioContext.Current["_result"] = result;
            Console.WriteLine(result);
        }

        [Then(@"Rename the Image file with the partnumber and (.*) in the format PN-NewImageView")]
        public void ThenRenameTheImageFileWithThePartnumberAndInTheFormatPN_NewImageView(string NewImageView)
        {
            String result = (string)ScenarioContext.Current["_result"];
            string datetime = (string)ScenarioContext.Current["_datetime"];
            fileOp.RenameProperImage(NewImageView,datetime,result);
        }

        [Then(@"Verify uploaded proper image is present in the Image directory with (.*)")]
        public void ThenVerifyUploadedProperImageIsPresentInTheImageDirectoryWith(string ExpectedImageView)
        {
            string result = (string)ScenarioContext.Current["_result"];
            rsult.NormalImagePresent(result,ExpectedImageView);
        }

        [Then(@"Verify image with (.*) has '1' in a PrimaryView column in the tblIMGImageDetails")]
        public void ThenVerifyImageWithLIFHasInAPrimaryViewColumnInTheTblIMGImageDetails(string ExpectedImageView)
        {
            string result = (string)ScenarioContext.Current["_result"];
            dbe.VerifyExpectedViewImage(ExpectedImageView, result);
        }

        [Then(@"Verify the other has '(.*)' in a PrimaryView column in the tblIMGImageDetails")]
        public void ThenVerifyTheOtherHasInAPrimaryViewColumnInTheTblIMGImageDetails(string ExpectedImageView)
        {
            string result = (string)ScenarioContext.Current["_result"];
            dbe.VerifyOtherImage(ExpectedImageView, result);
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
            rsult.NormalImageNotPresent(result, ImproperImageView);
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