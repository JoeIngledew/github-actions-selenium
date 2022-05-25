namespace Tests.PageObjects;

using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Selenium.Axe;

public class HomePage
{
    private readonly IWebDriver _driver;
    private readonly IConfiguration _config;

    public HomePage(IWebDriver driver, IConfiguration config)
    {
        _driver = driver;
        _config = config;
    }

    public IWebElement Title => _driver.FindElement(By.ClassName("display-4"));

    public void Navigate()
    {
        _driver.Navigate().GoToUrl(_config["BaseUrl"]);
    }

    public AxeResult CheckAccessibility()
    {
        var res = new AxeBuilder(_driver).Analyze();

        _driver.CreateAxeHtmlReport("./axe-result.html");

        return res;
    }
}