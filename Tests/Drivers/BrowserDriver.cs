namespace Tests.Drivers;

using System;

using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

public class BrowserDriver : IDisposable
{
    private readonly Lazy<IWebDriver> _cwdLazy;
    private readonly IConfiguration _config;
    private bool disposedValue;

    public BrowserDriver(IConfiguration config)
    {
        _cwdLazy = new Lazy<IWebDriver>(CreateWebDriver);
        _config = config;
    }

    public IWebDriver Current => _cwdLazy.Value;

    private IWebDriver CreateWebDriver()
    {
        var options = new ChromeOptions { AcceptInsecureCertificates = true };
        options.AddArgument("--incognito");
        options.AddArgument("--start-maximized");

        bool hasRemoteConfig = bool.TryParse(_config["UseRemoteBrowser"], out bool useRemote);
        if (hasRemoteConfig && useRemote)
        {
            return new RemoteWebDriver(
                new Uri("http://selenium:4444"),
                options
            );
        }

        return new ChromeDriver(
            ChromeDriverService.CreateDefaultService(),
            options
        );
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                if (_cwdLazy.IsValueCreated)
                    Current.Quit();
            }

            
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}