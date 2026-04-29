namespace Pos.DataTransfer;

public class ProductTypeDto
{
    public int ProductTypeId { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
    public int SortOrder { get; set; }
    public string RowVersion { get; set; }
    public int StatId { get; set; }
    public string StatName { get; set; }


    public ProductTypeDto()
    {
        ProductTypeId = 0;
        Name = "";
        Details = "";
        SortOrder = 0;
        RowVersion = "";
        StatId = 0;
        StatName = "";
    }

}

