using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace OEC.Fusion.GlobalImageRepository.Specflow.Steps
{
    [Binding]
    public class GIRSteps
    {
        [Given(@"When I connect to the database")]
        public void GivenWhenIConnectToTheDatabase()
        {
            //Leaving it future development
        }
        
        [Given(@"I execute the query to get the partnumber")]
        public void GivenIExecuteTheQueryToGetThePartnumber()
        {
            String partnumber = "";
        }
        
        [When(@"I verify the partnumber images are present in server location")]
        public void WhenIVerifyThePartnumberImagesArePresentInServerLocation()
        {
            Console.WriteLine("Test");
        }
        
        [Then(@"Create a folder for images in local")]
        public void ThenCreateAFolderForImagesInLocal()
        {
            Console.WriteLine("Test");
        }

        [Then(@"Test i need")]
        public void ThenTestINeed()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
