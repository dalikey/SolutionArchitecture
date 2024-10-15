namespace PaymentManagement.Domain;

public class Payment
{
    public int Price { get; set; }

    public string OrderNumber { get; set; } 

    public string UserName { get; set; }

    public string SupplierName { get; set; }

    public Dictionary<int, int> ProductIdQuantity { get; set; } = new();

    public bool IsForwardPaid { get; set; }

}
