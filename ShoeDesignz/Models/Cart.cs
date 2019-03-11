using System.Collections.Generic;

namespace ShoeDesignz.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public List<CartItems> CartItems { get; set; }
    }
}
