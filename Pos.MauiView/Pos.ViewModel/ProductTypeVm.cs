namespace Pos.ViewModel;

public class ProductTypeVm
{
    public int ProductTypeId { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
    public int SortOrder { get; set; }
    public string RowVersion { get; set; }
    public int StatId { get; set; }
    public string StatName { get; set; }
    public string Search { get; set; }
    public string SortMode { get; set; }
    public int ActionerId { get; set; }
    public string ActionerName { get; set; }
    


    public ProductTypeVm()
    {
        ProductTypeId = 0;
        Name = "";
        Details = "";
        SortOrder = 0;
        RowVersion = "";
        StatId = 0;
        StatName = "";
        SortMode = "";
        ActionerId = 0;
        ActionerName = "";
        Search = "";
    }

}

