using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeDesignz.Data;
using ShoeDesignz.Models;

namespace ShoeDesignz.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class IndexModel : PageModel
    {
        
        private readonly ShoeDesignzDbContext _context;

        /// <summary>
        /// To construct past orders with shoes DB context
        /// </summary>
        /// <param name="context"></param>
        public IndexModel(ShoeDesignzDbContext context)
        {
            _context = context;
        }

        public IList<Inventory> Inventories { get; set; }

        /// <summary>
        /// GET endpoint for the main page with all the Shoes for admin dashboard
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            Inventories = await _context.Shoes.ToListAsync();
        }
        
        
    }
}