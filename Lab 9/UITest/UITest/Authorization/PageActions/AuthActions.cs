using OpenQA.Selenium;

namespace UITest.Authorization.PageActions;

public class AuthActions
{
    private readonly IWebDriver _webDriver;

    private string _mainUrl = "http://shop.qatl.ru/";

    private readonly By _accountXPath = By.XPath("//a[text()='Account ']");
    private readonly By _toAuthXPath = By.XPath("//a[text()='Вход']");
    private readonly By _loginInputXPath = By.XPath("//input[@name='login']");
    private readonly By _passwordInputXPath = By.XPath("//input[@name='password']");
    private readonly By _submitBtnXPath = By.XPath("//button[@type='submit']");
    
    public AuthActions(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public void GoToAuthPage()
    {
        _webDriver.FindElement(_accountXPath).Click();
        _webDriver.FindElement(_toAuthXPath).Click();
    }

    public void Authorization(string login, string password)
    {
        var loginInputElement = _webDriver.FindElement(_loginInputXPath);
        loginInputElement.Clear();
        loginInputElement.SendKeys(login);
        
        var passwordInputElement = _webDriver.FindElement(_passwordInputXPath);
        passwordInputElement.Clear();
        passwordInputElement.SendKeys(password);
        
        _webDriver.FindElement(_submitBtnXPath).Click();
    }

    public bool CheckAuthorize(string className)
    {
        try
        {
            _webDriver.FindElement(By.XPath($"//div[@class='alert {className}']"));
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool CheckAuthorizeWithEmptyData(string type)
    {
        try
        {
            _webDriver.FindElement(By.XPath($"//div[@class='form-group has-feedback has-error has-danger']//input[@id='{type}']"));
            return true;
        }
        catch (Exception e)
        {
            return true;
        }
    }
}