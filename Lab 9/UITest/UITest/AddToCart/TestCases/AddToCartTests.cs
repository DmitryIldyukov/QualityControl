using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UITest.AddToCart.PageActions;

namespace UITest.AddToCart.TestCases;

[TestClass]
public class AddToCartTests
{
    private static IWebDriver _webDriver;
    private readonly string _url = "http://shop.qatl.ru/";
    private AddToCartActions _actions;

    [TestInitialize]
    public void TestInitialize()
    {
        _webDriver = new ChromeDriver();
        _webDriver.Manage().Window.Maximize();
        _webDriver.Navigate().GoToUrl(_url);
        _actions = new AddToCartActions(_webDriver);
    }
    
    [TestCleanup]
    public void TestCleanup()
    {
        _webDriver.Quit();
    }

    [TestMethod]
    public void AddProductToCart()
    {
        int quantity = 3;
        
        _actions.AddProductInCart(quantity);
        
        Assert.IsTrue(_actions.CheckingCartWithOneProduct(), "Продукты в корзине некорректны.");
    }
    
    [TestMethod]
    public void AddTwoProductToCart()
    {
        int firstQuantity = 6;
        int secondQuantity = 9;
        
        _actions.AddTwoProductInCart(firstQuantity, secondQuantity);
        
        Assert.IsTrue(_actions.CheckingCartWithTwoProduct(), "Продукты в корзине некорректны.");
    }
}