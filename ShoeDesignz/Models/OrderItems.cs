namespace ShoeDesignz.Models
{
    public class OrderItems
    {
        public int ID { get; set; }
        public int InventoryID { get; set; }
        public int CartID { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }

        public Inventory Inventory { get; set; }
        public Order Order { get; set; }
        public Cart cart { get; set; }

    }
}
