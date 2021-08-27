using OEC.Fusion.GlobalImageRepository.Helpers;
using OEC.Fusion.GlobalImageRepository.PageObjects;
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
        public string result = "";
        public string datetime = "";
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
            fileOp.RenameProperImage(properImageView, datetime, result);
        }

    }
}
