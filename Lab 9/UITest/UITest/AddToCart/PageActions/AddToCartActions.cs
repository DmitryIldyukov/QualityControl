using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UITest.AddToCart.PageActions;

public class AddToCartActions
{
    private readonly IWebDriver _webDriver;

    private string _mainUrl = "http://shop.qatl.ru/";
    
    private static readonly By _firstProductXPath = By.XPath("//a[text()='1234']");
    private static readonly By _secondProductXPath = By.XPath("//a[text()='Casio GA-1000-1AER']");
    private static readonly By _addToCartBtnXPath = By.XPath("//a[@id='productAdd']");
    private static readonly By _quantityInputXPath = By.XPath("//input[@name='quantity']");
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

    private const string FIRST_PRODUCT_NAME = "1234";
    private const string SECOND_PRODUCT_NAME = "CASIO GA-1000-1AER";

    private string FIRST_PRODUCT_QUANTITY;
    private string SECOND_PRODUCT_QUANTITY;
    

    public AddToCartActions(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public void AddProductInCart(int quantity)
    {
        FIRST_PRODUCT_QUANTITY = quantity.ToString();
        
        var productAddElement = _webDriver.FindElement(_firstProductXPath);
        productAddElement.Click();

        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));

        var quantityElement = _webDriver.FindElement(_quantityInputXPath);
        quantityElement.Clear();
        quantityElement.SendKeys(quantity.ToString());

        var addToCartBtnElement = _webDriver.FindElement(_addToCartBtnXPath);
        addToCartBtnElement.Click();
        
        Thread.Sleep(3000);
    }

    public void AddTwoProductInCart(int quantityFirst, int quantitySecond)
    {
        FIRST_PRODUCT_QUANTITY = quantityFirst.ToString();
        SECOND_PRODUCT_QUANTITY = quantitySecond.ToString();
        
        var productAddFirtsElement = _webDriver.FindElement(_firstProductXPath);
        productAddFirtsElement.Click();

        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));

        var quantityFirtsElement = _webDriver.FindElement(_quantityInputXPath);
        quantityFirtsElement.Clear();
        quantityFirtsElement.SendKeys(quantityFirst.ToString());
        
        var addToCartBtnFirstElement = _webDriver.FindElement(_addToCartBtnXPath);
        addToCartBtnFirstElement.Click();
        
        _webDriver.Navigate().GoToUrl(_mainUrl);
        Thread.Sleep(5000);
        
        var productAddSecondElement = _webDriver.FindElement(_secondProductXPath);
        productAddSecondElement.Click();

        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));

        var quantitySecondElement = _webDriver.FindElement(_quantityInputXPath);
        quantitySecondElement.Clear();
        quantitySecondElement.SendKeys(quantitySecond.ToString());
        
        var addToCartBtnSecondElement = _webDriver.FindElement(_addToCartBtnXPath);
        addToCartBtnSecondElement.Click();
        
        Thread.Sleep(5000);
    }
    
    public bool CheckingCartWithTwoProduct()
    {
        var firstProductName = _webDriver.FindElement(_firstProductNameXPath).Text;
        var secondProductName = _webDriver.FindElement(_secondProductNameXPath).Text;
        var firstProductQuantity = _webDriver.FindElement(_firstProductQuantitiesXPath).Text;
        var secondProductQuantity = _webDriver.FindElement(_secondProductQuantitiesXPath).Text;
        var firstProductPrice = _webDriver.FindElement(_firstProductPriceXPath).Text;
        var secondProductPrice = _webDriver.FindElement(_secondProductPriceXPath).Text;
        var totalQuantity = _webDriver.FindElement(_totalQuantityWithTwoProductXPath).Text;
        var totalPrice = _webDriver.FindElement(_totalPriceWithTwoProductXPath).Text;

        if (int.Parse(totalQuantity) != int.Parse(firstProductQuantity) + int.Parse(secondProductQuantity)) return false;
        if (totalPrice != $"${int.Parse(firstProductPrice) * int.Parse(firstProductQuantity) + int.Parse(secondProductPrice) * int.Parse(secondProductQuantity)}") return false;
        if (firstProductName.ToLower() != FIRST_PRODUCT_NAME.ToLower()) return false;
        if (secondProductName.ToLower() != SECOND_PRODUCT_NAME.ToLower()) return false;
        if (firstProductQuantity != FIRST_PRODUCT_QUANTITY) return false;
        if (secondProductQuantity != SECOND_PRODUCT_QUANTITY) return false;

        return true;
    }
}