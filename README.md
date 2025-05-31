# SeleniumTest (C# .NET + Selenium + NUnit)

This is a C# automation testing project using **Selenium WebDriver**, **ChromeDriver**, and **NUnit** — built with the .NET CLI and designed to be run in **Visual Studio Code on macOS** or any OS with .NET installed.


## Project Structure

```bash
.
├── FormFieldTests.cs        # NUnit test class for form field interactions
├── PracticeFormPage.cs      # Page Object Model (POM) class for form elements
├── Program.cs               # Main entry point (optional)
└── SeleniumTest.csproj      # Project file

```

## Clone the Repository

cd SeleniumTest


## Install Dependencies

dotnet add package Selenium.WebDriver
dotnet add package Selenium.WebDriver.ChromeDriver
dotnet add package Selenium.Support
dotnet add package DotNetSeleniumExtras.WaitHelpers
dotnet add package NUnit
dotnet add package NUnit3TestAdapter
dotnet add package Microsoft.NET.Test.Sdk

## Run Tests

dotnet test --logger "console;verbosity=detailed"
