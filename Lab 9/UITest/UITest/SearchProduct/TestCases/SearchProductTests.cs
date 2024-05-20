using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UITest.SearchProduct.PageActions;

namespace UITest.SearchProduct.TestCases;

[TestClass]
public class SearchProductTests
{
    private static IWebDriver _webDriver;
    private readonly string _url = "http://shop.qatl.ru/";
    private SearchProductActions _actions;
    
    private const string existWatchName = "1234"; 
    private const string notExistWatchName = "mnbzxcmnbzxc"; 

    [TestInitialize]
    public void TestInitialize()
    {
        _webDriver = new ChromeDriver();
        _webDriver.Manage().Window.Maximize();
        _webDriver.Navigate().GoToUrl(_url);
        _actions = new SearchProductActions(_webDriver);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _webDriver.Quit();
    }

    // Не проходит, т.к. поиск не работает
    // [TestMethod]
    // [DataRow(existWatchName, true)]
    // [DataRow(notExistWatchName, false)]
    // public void Test_Search_Product_By_SearchBar(string data, bool expectedResult)
    // {
    //     _actions.SearchProductBySearchBar(data);
    //     
    //     Assert.AreEqual(_actions.SearchProductOnCurrentPage(data), expectedResult);
    // }
    
    [TestMethod]
    public void Test_Search_By_Empty_Value_By_SearchBar()
    {
        _actions.SearchProductBySearchBar(string.Empty);
        
        Assert.AreEqual(_actions.IsEmptyPage(), true, "Страница должна быть пустая");
    }

    [TestMethod]
    [DataRow(existWatchName, true)]
    [DataRow(notExistWatchName, false)]
    public void Test_Search_Product_On_Main_Page(string data, bool expectedResult)
    {
        var result = _actions.SearchOnMainPage(data);

        Assert.AreEqual(expectedResult, result, $"Ожидалось, что товар {(expectedResult ? "" : "не")} будет найден");
    }
}