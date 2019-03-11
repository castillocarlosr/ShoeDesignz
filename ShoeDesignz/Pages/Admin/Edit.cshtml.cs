using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ShoeDesignz.Models;
using ShoeDesignz.Models.Interfaces;

namespace ShoeDesignz.Pages.Admin
{
    public class EditModel : PageModel
    {
        
        private readonly IInventory _inventory;

        [FromRoute]
        public int? ID { get; set; }

        [BindProperty]
        public Inventory Inventory { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public EditModel(IInventory inventory, IConfiguration configuration)
        {
            _inventory = inventory;
        }

        public async Task OnGet()
        {
            Inventory = await _inventory.GetInventoryByID(ID.GetValueOrDefault()) ?? new Inventory();
        }

        public async Task<IActionResult> OnPost()
        {
            var shoe = await _inventory.GetInventoryByID(ID.GetValueOrDefault()) ?? new Inventory();

            shoe.Name = Inventory.Name;
            shoe.Sku = Inventory.Sku;
            shoe.Price = Inventory.Price;
            shoe.DiscountPrice = Inventory.DiscountPrice;
            shoe.Gender = Inventory.Gender;
            shoe.Description = Inventory.Description;
            shoe.Image = Inventory.Image;

            if (Image != null)
            {
                //to create a temporaru file path
                var filePath = Path.GetTempFileName();

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
            }

            await _inventory.UpdateInventory(shoe);

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _inventory.DeleteInventory(ID.Value);
            return RedirectToPage("/Index");
        }
    }
}