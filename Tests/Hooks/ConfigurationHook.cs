namespace Tests.Hooks;

using Microsoft.Extensions.Configuration;

using BoDi;
using TechTalk.SpecFlow;
using OpenQA.Selenium;

[Binding]
public class ConfigurationHook
{
    [BeforeTestRun]
    public static void BeforeTestRun(ObjectContainer testThreadContainer)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("testsettings.json", false, false)
            .AddEnvironmentVariables()
            .AddUserSecrets<ConfigurationHook>()
            .Build();

        testThreadContainer.BaseContainer.RegisterInstanceAs<IConfiguration>(config);
    }

    [BeforeScenario]
    public static void BeforeScenario(ObjectContainer testThreadContainer)
    {
        var browserDriver = new Drivers.BrowserDriver(testThreadContainer.Resolve<IConfiguration>());
        testThreadContainer.BaseContainer.RegisterInstanceAs<IWebDriver>(browserDriver.Current);
    }

    [AfterScenario]
    public static void AfterScenario(ObjectContainer  testThreadContainer)
    {
        var driver = testThreadContainer.BaseContainer.Resolve<IWebDriver>();
        driver.Quit();
        driver.Dispose();
    }
}