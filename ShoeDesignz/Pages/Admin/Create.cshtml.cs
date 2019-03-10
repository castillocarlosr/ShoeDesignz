using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeDesignz.Data;
using ShoeDesignz.Models;

namespace ShoeDesignz.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly ShoeDesignzDbContext _context;

        public CreateModel(ShoeDesignzDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET endpoint creating past orders with DB context
        /// </summary>
        /// <param name="context">DB Context</param>
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Inventory Inventory { get; set; }
        
        /// <summary>
        /// POST adds the shoe
        /// </summary>
        /// <returns>Returns a page</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Shoes.Add(Inventory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}