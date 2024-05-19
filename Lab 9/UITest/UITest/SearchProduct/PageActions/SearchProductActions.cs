using OpenQA.Selenium;

namespace UITest.SearchProduct.PageActions;

public class SearchProductActions
{
    private readonly IWebDriver _webDriver;

    private string _mainUrl = "http://shop.qatl.ru/";
    
    private readonly By _searchBar = By.XPath("//input[@id='typeahead']");
    private readonly By _submitBtn = By.XPath("//form//input[@type='submit']");
    
    public SearchProductActions(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    
    public void SearchProductBySearchBar(string watchName)
    {
        var searchBarElement = _webDriver.FindElement(_searchBar);
        searchBarElement.Clear();
        searchBarElement.SendKeys(watchName);

        var submitBtn = _webDriver.FindElement(_submitBtn);
        submitBtn.Click();
    }

    public bool SearchProductOnCurrentPage(string watchName)
    {
        try
        {
            _webDriver.FindElement(By.XPath($"//h3[text()='{watchName}']"));
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool SearchOnMainPage(string watchName)
    {
        try
        {
            _webDriver.Navigate().GoToUrl(_mainUrl);
            _webDriver.FindElement(By.XPath($"//a[text()='{watchName}']")).Click();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool IsEmptyPage()
    {
        try
        {
            _webDriver.FindElement(By.XPath($"//dic[@class='product-one']"));
            return false;
        }
        catch (Exception e)
        {
            return true;
        }
    }
}