# SeleniumTest (C# .NET + Selenium + NUnit)

This is a C# automation testing project using **Selenium WebDriver**, **ChromeDriver**, and **NUnit** — built with the .NET CLI and designed to be run in **Visual Studio Code on macOS** or any OS with .NET installed.


## Project Structure

```bash
.
├── FormFieldTests.cs        
├── PracticeFormPage.cs     
├── Program.cs               
└── SeleniumTest.csproj     

```

## Clone the Repository

cd SeleniumTest


## Install Dependencies
```bash
dotnet add package Selenium.WebDriver
dotnet add package Selenium.WebDriver.ChromeDriver
dotnet add package Selenium.Support
dotnet add package DotNetSeleniumExtras.WaitHelpers
dotnet add package NUnit
dotnet add package NUnit3TestAdapter
dotnet add package Microsoft.NET.Test.Sdk
```

## Run Tests

dotnet test --logger "console;verbosity=detailed"
