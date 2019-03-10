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
    public class DeleteModel : PageModel
    {
        private readonly ShoeDesignzDbContext _context;

        /// <summary>
        /// Constructs past orders with DB context
        /// </summary>
        /// <param name="context">DB Context</param>
        public DeleteModel(ShoeDesignzDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Inventory Inventory { get; set; }

        /// <summary>
        /// GET endpoint to the deleting of a Shoe
        /// </summary>
        /// <param name="id">the id of the product</param>
        /// <returns>returns a page with the details of the product</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Inventory = await _context.Shoes.FirstOrDefaultAsync(m => m.ID == id);

            if (Inventory == null)
            {
                return NotFound();
            }
            return Page();
        }

        /// <summary>
        /// POST Deletes a single Shoe on post
        /// </summary>
        /// <param name="id">id of shoe to delete</param>
        /// <returns>Returns a page</returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Inventory = await _context.Shoes.FindAsync(id);

            if (Inventory != null)
            {
                _context.Shoes.Remove(Inventory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}