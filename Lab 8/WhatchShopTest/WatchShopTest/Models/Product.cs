namespace WhatchShopTest.Models;

public class Product
{
    public int id { get; set; }
    public int category_id { get; set; }
    public string title { get; set; }
    public string alias { get; set; }
    public string content { get; set; }
    public decimal price { get; set; }
    public decimal old_price { get; set; }
    public Byte status { get; set; }
    public string keywords { get; set; }
    public string description { get; set; }
    public Byte? hit { get; set; }
}