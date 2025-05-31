using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CloudQATestProject
{
    public class PracticeFormPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public PracticeFormPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        #region First Name Field Locators and Methods
        public IWebElement GetFirstNameField()
        {
            Console.WriteLine("Attempting to locate First Name field...");
            
            try
            {
                var element = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//input[@placeholder='First Name' or contains(@placeholder, 'first')]")));
                Console.WriteLine("First Name field found using placeholder attribute");
                return element;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Strategy 1 failed: {ex.Message}");
            }

            try
            {
                var element = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//label[contains(text(), 'First Name')]/following::input[1] | //input[preceding::label[contains(text(), 'First Name')]]")));
                Console.WriteLine("First Name field found using label association");
                return element;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Strategy 2 failed: {ex.Message}");
            }


            try
            {
                var element = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//form//input[@type='text'][1] | //input[@type='text' and position()=1]")));
                Console.WriteLine("First Name field found using position-based selector");
                return element;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Strategy 3 failed: {ex.Message}");
            }

            try
            {
                var elements = driver.FindElements(By.XPath("//input[@type='text']"));
                if (elements.Count > 0)
                {
                    Console.WriteLine("First Name field found using broad text input search");
                    return elements[0]; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Strategy 4 failed: {ex.Message}");
            }

            throw new NoSuchElementException("Could not locate First Name field using any strategy");
        }

        #endregion

        #region Gender Radio Button Locators and Methods
        public IWebElement GetGenderRadioButton(string genderText)
        {
            Console.WriteLine($"Attempting to locate {genderText} gender radio button...");
            
            try
            {
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath($"//input[@type='radio' and (@value='{genderText}' or @value='{genderText.ToLower()}' or @value='{genderText.Substring(0, 1).ToLower()}')]")));
                Console.WriteLine($"{genderText} radio button found using value attribute");
                return element;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Strategy 1 failed: {ex.Message}");
            }

            try
            {
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath($"//label[contains(text(), '{genderText}')]/input[@type='radio'] | //input[@type='radio'][following-sibling::text()[contains(., '{genderText}')] or preceding-sibling::text()[contains(., '{genderText}')]]")));
                Console.WriteLine($"{genderText} radio button found using label association");
                return element;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Strategy 2 failed: {ex.Message}");
            }


            try
            {
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath($"//input[@type='radio'][contains(following::text()[1], '{genderText}') or contains(preceding::text()[1], '{genderText}')]")));
                Console.WriteLine($"{genderText} radio button found using nearby text");
                return element;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Strategy 3 failed: {ex.Message}");
            }

            try
            {
                var radioButtons = driver.FindElements(By.XPath("//input[@type='radio']"));
                int index = genderText.ToLower() switch
                {
                    "male" => 0,
                    "female" => 1,
                    "transgender" => 2,
                    _ => -1
                };
                
                if (index >= 0 && index < radioButtons.Count)
                {
                    Console.WriteLine($"{genderText} radio button found using index-based selection");
                    return radioButtons[index];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Strategy 4 failed: {ex.Message}");
            }

            throw new NoSuchElementException($"Could not locate {genderText} radio button using any strategy");
        }

        #endregion

        #region Hobbies Checkbox Locators and Methods


        public IWebElement GetHobbyCheckbox(string hobbyText)
        {
            Console.WriteLine($"Attempting to locate {hobbyText} hobby checkbox...");
            

            try
            {
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath($"//input[@type='checkbox' and (@value='{hobbyText}' or @value='{hobbyText.ToLower()}')]")));
                Console.WriteLine($"{hobbyText} checkbox found using value attribute");
                return element;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Strategy 1 failed: {ex.Message}");
            }


            try
            {
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath($"//label[contains(text(), '{hobbyText}')]/input[@type='checkbox'] | //input[@type='checkbox'][following-sibling::text()[contains(., '{hobbyText}')] or preceding-sibling::text()[contains(., '{hobbyText}')]]")));
                Console.WriteLine($"{hobbyText} checkbox found using label association");
                return element;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Strategy 2 failed: {ex.Message}");
            }


            try
            {
                var element = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath($"//input[@type='checkbox'][contains(following::text()[1], '{hobbyText}') or contains(preceding::text()[1], '{hobbyText}')]")));
                Console.WriteLine($"{hobbyText} checkbox found using nearby text");
                return element;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Strategy 3 failed: {ex.Message}");
            }


            try
            {
                var checkboxes = driver.FindElements(By.XPath("//input[@type='checkbox']"));
                int index = hobbyText.ToLower() switch
                {
                    "dance" => 0,
                    "reading" => 1,
                    "cricket" => 2,
                    _ => -1
                };
                
                if (index >= 0 && index < checkboxes.Count)
                {
                    Console.WriteLine($"{hobbyText} checkbox found using index-based selection");
                    return checkboxes[index];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Strategy 4 failed: {ex.Message}");
            }

            throw new NoSuchElementException($"Could not locate {hobbyText} checkbox using any strategy");
        }

        #endregion

        #region Utility Methods

        public void WaitForPageLoad()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("form")));
            Console.WriteLine("Page fully loaded");
        }

        public List<string> GetAvailableGenderOptions()
        {
            var options = new List<string>();
            try
            {
                var radioButtons = driver.FindElements(By.XPath("//input[@type='radio']"));
                foreach (var button in radioButtons)
                {
                    var value = button.GetAttribute("value");
                    if (!string.IsNullOrEmpty(value))
                    {
                        options.Add(value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting gender options: {ex.Message}");
            }
            return options;
        }


        public List<string> GetAvailableHobbyOptions()
        {
            var options = new List<string>();
            try
            {
                var checkboxes = driver.FindElements(By.XPath("//input[@type='checkbox']"));
                foreach (var checkbox in checkboxes)
                {
                    var value = checkbox.GetAttribute("value");
                    if (!string.IsNullOrEmpty(value))
                    {
                        options.Add(value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting hobby options: {ex.Message}");
            }
            return options;
        }

        public bool IsFormReady()
        {
            try
            {
                // Check if main form elements are present
                var form = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("form")));
                var textInputs = driver.FindElements(By.XPath("//input[@type='text']"));
                var radioButtons = driver.FindElements(By.XPath("//input[@type='radio']"));
                var checkboxes = driver.FindElements(By.XPath("//input[@type='checkbox']"));
                
                bool isReady = form != null && textInputs.Count > 0 && radioButtons.Count > 0 && checkboxes.Count > 0;
                Console.WriteLine($"Form ready status: {isReady}");
                return isReady;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Form readiness check failed: {ex.Message}");
                return false;
            }
        }

        #endregion
    }
}