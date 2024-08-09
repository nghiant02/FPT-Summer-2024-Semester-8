public class OrderStatusDTO
{
    public int OrderId { get; set; }
    public string OrderStatus { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string ReturnReason { get; set; }
}
