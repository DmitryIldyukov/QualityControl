using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UITest.Authorization.PageActions;

namespace UITest.Authorization.TestCases;

[TestClass]
public class AuthTests
{
    private static IWebDriver _webDriver;
    private readonly string _url = "http://shop.qatl.ru/";
    private AuthActions _actions;
    private readonly JObject _authJson = JObject.Parse(File.ReadAllText(@"..\..\..\Authorization\Config\authData.json")); 

    [TestInitialize]
    public void TestInitialize()
    {
        _webDriver = new ChromeDriver();
        _webDriver.Manage().Window.Maximize();
        _webDriver.Navigate().GoToUrl(_url);
        _actions = new AuthActions(_webDriver);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _webDriver.Quit();
    }

    [TestMethod]
    [DataRow("valid", "alert-success")]
    [DataRow("invalid", "alert-danger")]
    public void Test_Authorization(string json, string className)
    {
        var data = _authJson[json];
        var login = data["login"];
        var password = data["password"];
        
        _actions.GoToAuthPage();
        _actions.Authorization(login.ToString(), password.ToString());
        
        Assert.IsTrue(_actions.CheckAuthorize(className), "У уведомления некорректный className");
    }
    
    [TestMethod]
    public void Test_Authorization_With_Empty_Data()
    {
        var data = _authJson["empty"];
        var login = data["login"];
        var password = data["password"];
        
        _actions.GoToAuthPage();
        _actions.Authorization(login.ToString(), password.ToString());
        
        Assert.IsTrue(_actions.CheckAuthorizeWithEmptyData("login"), "Input поле для логина должно иметь className has-error и has-danger");
        Assert.IsTrue(_actions.CheckAuthorizeWithEmptyData("password"), "Input поле для пароля должно иметь className has-error и has-danger");
    }
}