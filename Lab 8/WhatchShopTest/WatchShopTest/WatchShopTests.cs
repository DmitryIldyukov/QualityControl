using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using WatchShopTest;
using WhatchShopTest.Models;

namespace WhatchShopTest;

public static class AssertExtention
{
    public static void IsSameProducts(this Assert assert, JToken expected, JToken actual)
    {
        Assert.AreEqual(expected["title"], actual["title"], "Title у продуктов не идентичны");
        Assert.AreEqual(expected["alias"], actual["alias"], "Aliases у продуктов не идентичны");
        Assert.AreEqual(expected["price"], actual["price"], "Prices у продуктов не идентичны");
        Assert.AreEqual(expected["old_price"], actual["old_price"], "Old Prices у продуктов не идентичны");
        Assert.AreEqual(expected["status"], actual["status"], "Statuses у продуктов не идентичны");
        Assert.AreEqual(expected["keyword"], actual["keyword"], "Keywords у продуктов не идентичны");
        Assert.AreEqual(expected["description"], actual["description"], "Descriptions у продуктов не идентичны");
        Assert.AreEqual(expected["hit"], actual["hit"], "Hit у продуктов не идентичны");
    }
}

[TestClass]
public class WatchShopTests
{
    private static readonly WatchShopService _watchShopService = new WatchShopService(new HttpClient());
    private List<int> _addedProductIds = new List<int>();

    #region Schemas
    
    private readonly string _productSchema = File.ReadAllText(@"..\..\..\Jsons\Schemas\productSchema.json");
    private readonly string _addProductSchema = File.ReadAllText(@"..\..\..\Jsons\Schemas\addProductResponseSchema.json");

    #endregion

    #region JsonTests

    private readonly JObject _tests = JObject.Parse(File.ReadAllText(@"..\..\..\Jsons\Tests\addProductTests.json"));

    #endregion
    
    private bool IsJsonValid(JToken json, string schema)
    {
        JsonSchema jsonSchema = JsonSchema.Parse(schema);
        return json.IsValid(jsonSchema);
    }

    private async Task<JToken> GetProductById(int id)
    {
        return (await _watchShopService.GetProducts()).FirstOrDefault(_ => (int)_["id"]! == id);
    }
    
    [TestCleanup]
    public async Task Cleanup()
    {
        foreach (var id in _addedProductIds)
        {
            await _watchShopService.DeleteProduct(id);
        }
        
        _addedProductIds.Clear();
    }
    
    [TestMethod]
    public async Task Test_Get_Products_And_Validate()
    {
        // Act
        var products = await _watchShopService.GetProducts();

        // Assert
        Assert.IsNotNull(products, "Список продуктов равен null");
        Assert.IsTrue(products.Count > 0, "Список продуктов пустой");
        Assert.IsTrue(IsJsonValid(products, _productSchema));
    }
    
    [TestMethod]
    public async Task Test_Add_Valid_Product()
    {
        // Arrange
        var product = _tests["validFirst"];
        
        // Act
        var response = await _watchShopService.AddProduct(product!.ToObject<Product>()!);
        var productId = response["id"]!.ToObject<int>();
        _addedProductIds.Add(productId);
        var currentProduct = await GetProductById(productId);
        
        // Assert
        Assert.IsNotNull(currentProduct, "Продукт не найден");
        Assert.IsTrue(IsJsonValid(response, _addProductSchema));
        Assert.IsTrue(IsJsonValid(currentProduct, _productSchema));
        Assert.That.IsSameProducts(product, currentProduct);
    }
    
    [TestMethod]
    public async Task Test_Add_Valid_Product_With_Same_Alias()
    {
        // Arrange
        var product = _tests["validForAlias"];
        
        // Act
        var response1 = await _watchShopService.AddProduct(product!.ToObject<Product>()!);
        var response2 = await _watchShopService.AddProduct(product!.ToObject<Product>()!);
        var productId1 = response1["id"]!.ToObject<int>();
        var productId2 = response2["id"]!.ToObject<int>();
        _addedProductIds.Add(productId1);
        _addedProductIds.Add(productId2);
        var product1 = await GetProductById(productId1);
        var product2 = await GetProductById(productId2);
        
        // Assert
        Assert.AreEqual(product1["alias"], product["alias"], "Первый продукт должен быть с оригинальным alias");
        Assert.AreEqual(product2["alias"], product["alias"] + "-0", "Второй продукт должен быть с -0 в конце");
    }

    [DataRow("invalidByCategoryIdLess")]
    [DataRow("invalidByCategoryIdMore")]
    [TestMethod]
    public async Task Test_Add_Product_With_Invalid_Category_Id(string categoryId)
    {
        // Arrage
        var product = _tests[categoryId];
        
        // Act
        var response = await _watchShopService.AddProduct(product!.ToObject<Product>()!);
        var productId = response["id"]!.ToObject<int>();
        _addedProductIds.Add(productId);
        var currentProduct = await GetProductById(productId);
        
        // Assert
        Assert.IsNull(currentProduct, "Продукт должен не добавляться");
        Assert.AreEqual(response["status"], 1, "Ожидался статус 1");
    }
    
    [TestMethod]
    public async Task Test_Add_Product_With_Invalid_Hit()
    {
        // Arrage
        var product = _tests["invalidHit"];
        
        // Act
        var response = await _watchShopService.AddProduct(product!.ToObject<Product>()!);
        var productId = response["id"]!.ToObject<int>();
        _addedProductIds.Add(productId);
        
        // Assert
        Assert.AreEqual(response["status"], 1, "Ожидался статус 1");
    }
    
    [TestMethod]
    public async Task Test_Valid_Edit_Product()
    {
        // Arrage
        var addProduct = _tests["validFirst"];
        var editProduct = _tests["validEditProduct"];
        var addedResponse = await _watchShopService.AddProduct(addProduct!.ToObject<Product>()!);
        var productId = addedResponse["id"]!.ToObject<int>();
        _addedProductIds.Add(productId);
        editProduct!["id"] = productId.ToString();
        
        // Act
        var response = await _watchShopService.EditProduct(editProduct.ToObject<Product>()!);
        var currentProduct = await GetProductById(productId);
        
        // Assert
        Assert.IsNotNull(currentProduct, "Продукт не найден");
        Assert.IsTrue(IsJsonValid(currentProduct, _productSchema));
        Assert.That.IsSameProducts(currentProduct, editProduct);
    }
    
    [TestMethod]
    public async Task Test_Invalid_Id_Edit_Product()
    {
        // Arrage
        var addProduct = _tests["validFirst"];
        var editProduct = _tests["invalidIdEditProduct"];
        var addedResponse = await _watchShopService.AddProduct(addProduct!.ToObject<Product>()!);
        var productId = addedResponse["id"]!.ToObject<int>();
        _addedProductIds.Add(productId);
        
        // Act
        var response = await _watchShopService.EditProduct(editProduct!.ToObject<Product>()!);
        var currentProduct = await GetProductById(productId);
        
        // Assert
        Assert.AreEqual(response["status"], 1, "Ожидался статус 1");
    }
    
    [DataRow("invalidProductPrice1")]
    [DataRow("invalidProductPrice2")]
    [TestMethod]
    public async Task Test_Add_Product_With_Invalid_Price(string json)
    {
        // Arrage
        var product = _tests[json];
        
        // Act
        var response = await _watchShopService.AddProduct(product!.ToObject<Product>()!);
        var productId = response["id"]!.ToObject<int>();
        _addedProductIds.Add(productId);
        
        // Assert
        Assert.AreEqual(response["status"], 1, "Ожидался статус 1");
    }
    
    [TestMethod]
    public async Task Test_Delete_Existing_Product()
    {
        // Arrange
        var response = await _watchShopService.AddProduct(_tests["validSecond"]!.ToObject<Product>()!);
        var productId = response["id"]!.ToObject<int>();

        // Act
        await _watchShopService.DeleteProduct(productId);

        // Assert
        Assert.IsNull(await GetProductById(productId), "Продукт не удалился");
    }
    
    [DataRow(-1)]
    [DataRow(99999)]
    [TestMethod]
    public async Task Test_Delete_Not_Existing_Product(int id)
    {
        // Arrange
        var productId = id;

        // Act
        var result = await _watchShopService.DeleteProduct(productId);

        // Assert
        Assert.AreEqual(result["status"], 0, "Ожидался статус 1");
        Assert.IsNull(await GetProductById(productId), "Продукт не удалился");
    }
}