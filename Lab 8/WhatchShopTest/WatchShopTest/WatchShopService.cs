using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WhatchShopTest.Models;

namespace WatchShopTest;

public class WatchShopService
{
    private readonly HttpClient _httpClient;

    public WatchShopService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JArray> GetProducts()
    {
        var requestUrl = new Uri("http://shop.qatl.ru/api/products");
        var response = await _httpClient.GetAsync(requestUrl);
        
        var productsJson = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
        var productsString = productsJson.ToString();
        return (JArray)JToken.Parse(productsString);
    }

    public async Task<JObject> DeleteProduct(int id)
    {
        var requestUrl = new Uri($"http://shop.qatl.ru/api/deleteproduct");
        var uriBuilder = new UriBuilder(requestUrl);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query[nameof(Product.id)] = id.ToString();
        uriBuilder.Query = query.ToString();
        requestUrl = uriBuilder.Uri;
        var response = await _httpClient.GetAsync(requestUrl);
        var productJson = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
        var productString = productJson.ToString();
        return (JObject)JToken.Parse(productString);
    }

    public async Task<JObject> AddProduct(Product product)
    {
        var requestUrl = new Uri("http://shop.qatl.ru/api/addproduct");
        var data = new StringContent(JsonConvert.SerializeObject(product));
        var response = await _httpClient.PostAsync(requestUrl, data);
        var productJson = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
        var productString = productJson.ToString();
        return (JObject)JToken.Parse(productString);
    }

    public async Task<JObject> EditProduct(Product product)
    {
        var requestUrl = new Uri("http://shop.qatl.ru/api/editproduct");
        var data = new StringContent(JsonConvert.SerializeObject(product));
        var response = await _httpClient.PostAsync(requestUrl, data);
        var productJson = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
        var productString = productJson.ToString();
        return (JObject)JToken.Parse(productString);
    }
}