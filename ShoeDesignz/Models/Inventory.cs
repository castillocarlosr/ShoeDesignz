using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models
{
    public class Inventory
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Sku { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public Genders Gender { get; set; }

        public string Image { get; set; }


        public enum Genders
        {
            Male,
            Female,
            Neutral
        }
    }
}
