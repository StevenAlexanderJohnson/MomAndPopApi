public class Inventory
{
    public Inventory(Int64 id, Int64 storeId, int productId, int quantity, string image, string description)
    {
        Id = id;
        StoreId = storeId;
        ProductId = productId;
        Quantity = quantity;
        Image = image;
        Description = description;
    }

    public Int64 Id { get; set; }
    public Int64 StoreId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
}