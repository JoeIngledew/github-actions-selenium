namespace Tests.Steps;

using System;
using FluentAssertions;
using TechTalk.SpecFlow;
using PageObjects;
	
[Binding]
public class HomeSteps
{
    private readonly ScenarioContext _scenarioContext;
    private readonly HomePage _home;

    public HomeSteps(ScenarioContext scenarioContext, HomePage home)
    {
        _scenarioContext = scenarioContext;
        _home = home;
    }

    [Given(@"I am on the home page")]
    public void GivenIamonthehomepage()
    {
        _home.Navigate();
    }

    [Then(@"the title is ""(.*)""")]
    public void Thenthetitleis(string args1)
    {
        _home.Title.Text.Should().Be(args1);
    }

    [Then(@"the page is accessible")]
    public void Thenthepageisaccessible()
    {
        var res = _home.CheckAccessibility();
        res.Violations.Should().BeEmpty();
    }
}