using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using UITest.Order.Config;

namespace UITest.Order.PageActions;

public class OrderActions
{
    private readonly IWebDriver _webDriver;

    private string _mainUrl = "http://shop.qatl.ru/";
    
    private static readonly By _firstProductXPath = By.XPath("//a[@href='cart/add?id=2']");
    private static readonly By _toOrderBtnXPath = By.XPath("//div[@class='modal-footer']//a");
    private static readonly By _orderBtnXPath = By.XPath("//button[@type='submit']");
    
    private static readonly By _loginInputXPath = By.XPath("//input[@name='login']");
    private static readonly By _passwordInputXPath = By.XPath("//input[@name='password']");
    private static readonly By _nameInputXPath = By.XPath("//input[@name='name']");
    private static readonly By _emailInputXPath = By.XPath("//input[@name='email']");
    private static readonly By _addressInputXPath = By.XPath("//input[@name='address']");
    
    public OrderActions(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public void AddProductToCart()
    {
        _webDriver.Navigate().GoToUrl(_mainUrl);
        _webDriver.FindElement(_firstProductXPath).Click();
        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.ElementToBeClickable(_toOrderBtnXPath));
        _webDriver.FindElement(_toOrderBtnXPath).Click();
    }

    public void FillInTheForm(RegisterFormData data)
    {
        var loginInputElement = _webDriver.FindElement(_loginInputXPath);
        loginInputElement.Clear();
        loginInputElement.SendKeys(data.Login);
        
        var passwordInputElement = _webDriver.FindElement(_passwordInputXPath);
        passwordInputElement.Clear();
        passwordInputElement.SendKeys(data.Password);
        
        var nameInputElement = _webDriver.FindElement(_nameInputXPath);
        nameInputElement.Clear();
        nameInputElement.SendKeys(data.Name);
        
        var emailInputElement = _webDriver.FindElement(_emailInputXPath);
        emailInputElement.Clear();
        emailInputElement.SendKeys(data.Email);
        
        var addressInputElement = _webDriver.FindElement(_addressInputXPath);
        addressInputElement.Clear();
        addressInputElement.SendKeys(data.Address);
    }

    public void MakeOrder()
    {
        _webDriver.FindElement(_orderBtnXPath).Click();
    }
}