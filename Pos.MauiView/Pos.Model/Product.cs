namespace Pos.Model;

public class Product
{
    public int ProductId { get; set; }
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
    public int SortOrder { get; set; }
    public decimal WholePrice { get; set; }
    public decimal RetailPrice { get; set; }
    public int StatId { get; set; }


    public Product()
    {
        ProductId = 0;
        ProductCode = "";
        Name = "";
        Details = "";
        SortOrder = 0;
        WholePrice = 0;
        RetailPrice = 0;
        StatId = 0;
    }

}
