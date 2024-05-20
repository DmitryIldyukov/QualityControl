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
    
    private const string FIRST_PRODUCT_NAME = "1234";
    private const string SECOND_PRODUCT_NAME = "CASIO GA-1000-1AER";
    
    private static readonly By _firstProductNameXPath = By.XPath("//tr[1]//td[2]//a");
    private static readonly By _secondProductNameXPath = By.XPath("//tr[2]//td[2]//a");
    private static readonly By _firstProductQuantitiesXPath = By.XPath("//tr[1]//td[3]");
    private static readonly By _secondProductQuantitiesXPath = By.XPath("//tr[2]//td[3]");
    private static readonly By _firstProductPriceXPath = By.XPath("//tr[1]//td[4]");
    private static readonly By _secondProductPriceXPath = By.XPath("//tr[2]//td[4]");
    private static readonly By _totalQuantityWithOneProductXPath = By.XPath("//tr[2]//td[2]");
    private static readonly By _totalQuantityWithTwoProductXPath = By.XPath("//tr[3]//td[2]");
    private static readonly By _totalPriceWithOneProductXPath = By.XPath("//tr[3]//td[2]");
    private static readonly By _totalPriceWithTwoProductXPath = By.XPath("//tr[4]//td[2]");

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
        
        Assert.AreEqual(_webDriver.FindElement(_firstProductNameXPath).Text.ToLower(), FIRST_PRODUCT_NAME.ToLower(), 
            "Имена товаров не соответствуют");
        Assert.AreEqual(_webDriver.FindElement(_firstProductQuantitiesXPath).Text, quantity.ToString(), 
            "Кол-во первого товара не соответствует ожидаемому");
        Assert.AreEqual(int.Parse(_webDriver.FindElement(_totalQuantityWithOneProductXPath).Text), int.Parse(_webDriver.FindElement(_firstProductQuantitiesXPath).Text), 
            "Кол-во всех товаров не соответствует ожидаемому");
        Assert.AreEqual($"${int.Parse(_webDriver.FindElement(_firstProductPriceXPath).Text) * quantity}", 
            _webDriver.FindElement(_totalPriceWithOneProductXPath).Text,
            "Общая цена не соответствует ожидаемой");
    }
    
    [TestMethod]
    public void AddTwoProductToCart()
    {
        int firstQuantity = 6;
        int secondQuantity = 9;

        _actions.AddTwoProductInCart(firstQuantity, secondQuantity);
        
        var firstProductName = _webDriver.FindElement(_firstProductNameXPath).Text;
        var secondProductName = _webDriver.FindElement(_secondProductNameXPath).Text;
        var firstProductQuantity = _webDriver.FindElement(_firstProductQuantitiesXPath).Text;
        var secondProductQuantity = _webDriver.FindElement(_secondProductQuantitiesXPath).Text;
        var firstProductPrice = _webDriver.FindElement(_firstProductPriceXPath).Text;
        var secondProductPrice = _webDriver.FindElement(_secondProductPriceXPath).Text;
        var totalQuantity = _webDriver.FindElement(_totalQuantityWithTwoProductXPath).Text;
        var totalPrice = _webDriver.FindElement(_totalPriceWithTwoProductXPath).Text;
        
        Assert.AreEqual(int.Parse(totalQuantity), 
            int.Parse(firstProductQuantity) + int.Parse(secondProductQuantity), 
            "Общее кол-во товаров не соответствует ожидаемому.");
        Assert.AreEqual(totalPrice, 
            $"${int.Parse(firstProductPrice) * int.Parse(firstProductQuantity) + int.Parse(secondProductPrice) * int.Parse(secondProductQuantity)}", 
            "Общая стоимость товаров не соответсвует ожидаемой");
        Assert.AreEqual(firstProductName.ToLower(), FIRST_PRODUCT_NAME.ToLower(), "Название первого товара не соответсвует действительности");
        Assert.AreEqual(secondProductName.ToLower(), SECOND_PRODUCT_NAME.ToLower(), "Название второго товара не соответсвует действительности");
        Assert.AreEqual(firstProductQuantity, firstQuantity.ToString(), "Кол-во первого товара не соответствует ожидаемому");
        Assert.AreEqual(secondProductQuantity, secondQuantity.ToString(), "Кол-во второго товара не соответствует ожидаемому");
    }
}