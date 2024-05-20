using Microsoft.VisualStudio.TestTools.UnitTesting;using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UITest.Authorization.PageActions;
using UITest.Order.Config;
using UITest.Order.PageActions;

namespace UITest.Order.TestCases;

[TestClass]
public class OrderTests
{
    private static IWebDriver _webDriver;
    private readonly string _url = "http://shop.qatl.ru/";
    private OrderActions _actions;
    private AuthActions _authActions;
    private readonly JObject _authJson = JObject.Parse(File.ReadAllText(@"..\..\..\Authorization\Config\authData.json")); 
    private readonly JObject _registerJson = JObject.Parse(File.ReadAllText(@"..\..\..\Order\Config\registerData.json"));
    
    private readonly string errorMsg = "Произошла ошибка";
    private readonly string busyLoginMsg = "Этот логин уже занят";
    private readonly string busyEmailMsg = "Этот email уже занят";
    
    private readonly By _errorMsgXPath = By.XPath("//h1");
    private static readonly By _loginBusyAllertXPath = By.XPath("//div[@class='alert alert-danger']//ul//li[1]");
    private static readonly By _emailBusyAllertXPath = By.XPath("//div[@class='alert alert-danger']//ul//li[2]");
    
    [TestInitialize]
    public void TestInitialize()
    {
        _webDriver = new ChromeDriver();
        _webDriver.Manage().Window.Maximize();
        _webDriver.Navigate().GoToUrl(_url);
        _actions = new OrderActions(_webDriver);
        _authActions = new AuthActions(_webDriver);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _webDriver.Quit();
    }

    [TestMethod]
    public void Test_Order_With_Auth()
    {
        var data = _authJson["valid"];
        var login = data["login"];
        var password = data["password"];
        _authActions.GoToAuthPage();
        _authActions.Authorization(login.ToString(), password.ToString());
        
        _actions.AddProductToCart();
        _actions.MakeOrder();
        
        Assert.AreEqual(_webDriver.FindElement(_errorMsgXPath).Text, errorMsg, "Ожидалась ошибка");
    }

    [TestMethod]
    public void Test_Order_Without_Auth()
    {
        var data = _registerJson["valid"].ToObject<RegisterFormData>();
        
        _actions.AddProductToCart();
        _actions.FillInTheForm(data);
        _actions.MakeOrder();
        
        Assert.AreEqual(_webDriver.FindElement(_errorMsgXPath).Text, errorMsg, "Ожидалась ошибка");
    }
    
    [TestMethod]
    public void Test_Order_Without_Auth_With_Busy_Data()
    {
        var data = _registerJson["busyData"].ToObject<RegisterFormData>();
        
        _actions.AddProductToCart();
        _actions.FillInTheForm(data);
        _actions.MakeOrder();
        
        Assert.AreEqual(_webDriver.FindElement(_loginBusyAllertXPath).Text, busyLoginMsg, "Ожидалась ошибка, что логин занят");
        Assert.AreEqual(_webDriver.FindElement(_emailBusyAllertXPath).Text, busyEmailMsg, "Ожидалась ошибка, что email занят");
    }
}