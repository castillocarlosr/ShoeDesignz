using System;
using System.Collections.Generic;

namespace ShoeDesignz.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string UserID { get; set; }      
        public List<OrderItems> OrderItems { get; set; }
        public DateTime Now { get; set; }

     
    }
}
