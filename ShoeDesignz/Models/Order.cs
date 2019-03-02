using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public List<OrderItems> OrderItems { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
