namespace ShoeDesignz.Models
{
    public class CartItems
    {
        public int ID { get; set; }
        public int CartID { get; set; }
        //public int OrderID { get; set; }
        public int InventoryID { get; set; }
        public int Quantity { get; set; }

        public Cart Cart { get; set; }
        public Inventory Inventory { get; set; }
    }
}
