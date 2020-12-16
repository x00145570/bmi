
// MSTest
using Microsoft.VisualStudio.TestTools.UnitTesting;

// NuGet install Selenium WebDriver package and Support Classes
using OpenQA.Selenium;

// NuGet install PhantomJS driver (or Chrome or Firefox...)
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace UserAcceptanceTests
{
    [TestClass]
    public class Selenium
    {
        // .runsettings file contains test run parameters
        // e.g. URI for app
        // test context for this run

        private TestContext testContextInstance;

        // test harness uses this property to initliase test context
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        // URI for web app being tested
        private String webAppUri;

        // .runsettings property overriden in vsts test runner
        // release task
        [TestInitialize]                // run before each unit test
        public void Setup()
        {
            this.webAppUri = testContextInstance.Properties["webAppUri"].ToString();
       
        }



        [TestMethod]
        public void TestBMI()
        {
            //This is what the pipeline needs
            //using (IWebDriver driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver")))
            //This is what Visual Studios needs
            using (IWebDriver driver = new ChromeDriver())
            {
                // any exception below result in a test fail

                // navigate to URI for temperature converter
                // web app running on IIS express
                driver.Navigate().GoToUrl(webAppUri);

                // get weight in stone element
                IWebElement weightInStoneElement = driver.FindElement(By.Id("BMI_WeightStones"));
                // enter 10 in element
                weightInStoneElement.SendKeys("10");

                // get weight in stone element
                IWebElement weightInPoundsElement = driver.FindElement(By.Id("BMI_WeightPounds"));
                // enter 10 in element
                weightInPoundsElement.SendKeys("10");

                // get weight in stone element
                IWebElement heightFeetElement = driver.FindElement(By.Id("BMI_HeightFeet"));
                // enter 10 in element
                heightFeetElement.SendKeys("5");

                // get weight in stone element
                IWebElement heightInchesElement = driver.FindElement(By.Id("BMI_HeightInches"));
                // enter 10 in element
                heightInchesElement.SendKeys("5");

                // submit the form
                driver.FindElement(By.Id("convertForm")).Submit();

                // explictly wait for result with "BMIValue" item
                IWebElement BMIValueElement = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                    .Until(c => c.FindElement(By.Id("bmiVal")));


                // item comes back like "BMIValue: 24.96"
                String bmi = BMIValueElement.Text.ToString();

                // 10 Celsius = 50 Fahrenheit, assert it
                StringAssert.Contains(bmi, "Your BMI is 24.96");

                driver.Quit();
            }
        }
    }
}
