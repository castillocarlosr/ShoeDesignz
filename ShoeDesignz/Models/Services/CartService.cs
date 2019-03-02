using ShoeDesignz.Data;
using ShoeDesignz.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models.Services
{
    public class CartService : ICart
    {
        private ShoeDesignzDbContext _context { get; }
        public CartService(ShoeDesignzDbContext context)
        {
            _context = context;
        }
        public Task<CartItems> AddCartItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCartItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CartItems>> SendOrder()
        {
            throw new NotImplementedException();
        }

        public Task UpdateCart(CartItems CartItems)
        {
            throw new NotImplementedException();
        }
    }
}
