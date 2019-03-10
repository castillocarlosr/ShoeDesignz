using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeDesignz.Models;
using ShoeDesignz.Models.Interfaces;

namespace ShoeDesignz.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class OrderModel : PageModel
    {
        
        public OrderModel(IOrder order)
        {
            _order = order;
        }

        [BindProperty]
        public List<Order> Orders { get; set; }
        public IOrder _order { get; set; }

        /// <summary>
        /// Returns all the past orders.  More than 10.  I didn't set a limit.
        /// </summary>
        /// <returns>Returns past orders</returns>
        public async Task OnGetAsync()
        {
            Orders = await _order.GetOrders("");
            //Orders = await _order.GetOrder();
        }
    }  
}