using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework.Legacy;


namespace CloudQATestProject.Tests
{
    [TestFixture]
    public class FormFieldTests

    {
        private IWebDriver driver = null!;
        private PracticeFormPage formPage = null!;
        private WebDriverWait wait = null!;
        private const string BASE_URL = "https://app.cloudqa.io/home/AutomationPracticeForm";

        [SetUp]
        public void Setup()
        {

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
            chromeOptions.AddArgument("--disable-extensions");


            driver = new ChromeDriver(chromeOptions);


            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


            formPage = new PracticeFormPage(driver, wait);


            driver.Navigate().GoToUrl(BASE_URL);


            wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("form")));

            Console.WriteLine("Test setup completed - Browser opened and navigated to CloudQA practice form");
        }

        [Test]
        [Description("Test First Name field input and validation")]
        public void TestFirstNameField()
        {
            Console.WriteLine("Starting First Name field test...");

            try
            {

                string testFirstName = "John";


                Console.WriteLine("Step 1: Locating First Name field using robust selectors...");
                var firstNameField = formPage.GetFirstNameField();


                Console.WriteLine("Step 2: Clearing field and entering test data: " + testFirstName);
                firstNameField.Clear();
                firstNameField.SendKeys(testFirstName);


                Console.WriteLine("Step 3: Validating input...");
                string actualValue = firstNameField.GetAttribute("value");


                ClassicAssert.AreEqual(testFirstName, actualValue,
                    $"Expected first name to be '{testFirstName}', but found '{actualValue}'");

                Console.WriteLine($"First Name field test PASSED - Successfully entered and validated: '{actualValue}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"First Name field test FAILED: {ex.Message}");
                throw;
            }
        }

        [Test]
        [Description("Test Gender radio button selection")]
        public void TestGenderSelection()
        {
            Console.WriteLine("Starting Gender selection test...");

            try
            {

                string targetGender = "Female";


                Console.WriteLine($"Step 1: Locating {targetGender} radio button...");
                var genderRadioButton = formPage.GetGenderRadioButton(targetGender);


                Console.WriteLine($"Step 2: Selecting {targetGender} radio button...");
                if (!genderRadioButton.Selected)
                {
                    genderRadioButton.Click();
                }


                Console.WriteLine("Step 3: Validating radio button selection...");


                Thread.Sleep(500);


                genderRadioButton = formPage.GetGenderRadioButton(targetGender);


                ClassicAssert.IsTrue(genderRadioButton.Selected,
                    $"Expected {targetGender} radio button to be selected, but it was not");


                var maleRadioButton = formPage.GetGenderRadioButton("Male");
                ClassicAssert.IsFalse(maleRadioButton.Selected,
                    "Male radio button should not be selected when Female is selected");

                Console.WriteLine($"Gender selection test PASSED - Successfully selected: {targetGender}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gender selection test FAILED: {ex.Message}");
                throw;
            }
        }

        [Test]
        [Description("Test Hobbies checkbox selection")]
        public void TestHobbiesSelection()
        {
            Console.WriteLine("Starting Hobbies selection test...");

            try
            {

                string[] hobbiesToSelect = { "Reading", "Cricket" };

                Console.WriteLine($"Step 1: Selecting hobbies: {string.Join(", ", hobbiesToSelect)}");


                foreach (string hobby in hobbiesToSelect)
                {
                    Console.WriteLine($"  - Locating and selecting {hobby} checkbox...");
                    var hobbyCheckbox = formPage.GetHobbyCheckbox(hobby);

                    if (!hobbyCheckbox.Selected)
                    {
                        hobbyCheckbox.Click();
                        Console.WriteLine($"{hobby} checkbox selected");
                    }
                    else
                    {
                        Console.WriteLine($"{hobby} checkbox was already selected");
                    }
                }


                Console.WriteLine("Step 2: Validating hobby selections...");
                foreach (string hobby in hobbiesToSelect)
                {
                    var hobbyCheckbox = formPage.GetHobbyCheckbox(hobby);
                    ClassicAssert.IsTrue(hobbyCheckbox.Selected,
                        $"Expected {hobby} checkbox to be selected, but it was not");
                    Console.WriteLine($"{hobby} checkbox validation passed");
                }


                Console.WriteLine("Step 3: Validating unselected hobbies...");
                var danceCheckbox = formPage.GetHobbyCheckbox("Dance");
                ClassicAssert.IsFalse(danceCheckbox.Selected,
                    "Dance checkbox should not be selected in this test");
                Console.WriteLine("Dance checkbox correctly unselected");

                Console.WriteLine($"Hobbies selection test PASSED - Successfully selected: {string.Join(", ", hobbiesToSelect)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hobbies selection test FAILED: {ex.Message}");
                throw;
            }
        }

        [Test]
        [Description("Comprehensive form interaction test")]
        public void TestCompleteFormInteraction()
        {
            Console.WriteLine("Starting comprehensive form interaction test...");

            try
            {

                Console.WriteLine("Step 1: Testing First Name field...");
                var firstNameField = formPage.GetFirstNameField();
                firstNameField.Clear();
                firstNameField.SendKeys("Alice");
                ClassicAssert.AreEqual("Alice", firstNameField.GetAttribute("value"));
                Console.WriteLine("First Name field completed");


                Console.WriteLine("Step 2: Testing Gender selection...");
                var genderRadio = formPage.GetGenderRadioButton("Male");
                genderRadio.Click();

                ClassicAssert.IsTrue(formPage.GetGenderRadioButton("Male").Selected);
                Console.WriteLine("Gender selection completed");


                Console.WriteLine("Step 3: Testing Hobbies selection...");
                var readingCheckbox = formPage.GetHobbyCheckbox("Reading");
                var danceCheckbox = formPage.GetHobbyCheckbox("Dance");

                readingCheckbox.Click();
                danceCheckbox.Click();



                ClassicAssert.IsTrue(formPage.GetHobbyCheckbox("Reading").Selected);
                ClassicAssert.IsTrue(formPage.GetHobbyCheckbox("Dance").Selected);
                Console.WriteLine("Hobbies selection completed");

                Console.WriteLine("Comprehensive form interaction test PASSED - All fields tested successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Comprehensive form interaction test FAILED: {ex.Message}");
                throw;
            }
        }

        [TearDown]
        public void TearDown()
        {

            try
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                    Console.WriteLine("Browser closed and resources cleaned up");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during cleanup: {ex.Message}");
            }
        }
    }
}