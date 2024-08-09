namespace BusinessObjects.Dto.BillReqRes;

public class BillItemResponse
{
    public string? JewelryId { get; set; }
    public string? Name { get; set; }
    public double JewelryPrice { get; set; }
    public double? LaborCost { get; set; }
    public double TotalPrice { get; set; }
}