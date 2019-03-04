using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public List<CartItems> CartItems { get; set; }
    }
}
