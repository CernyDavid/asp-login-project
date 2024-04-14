using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WardrobeApp.Data;
using WardrobeApp.Models;

namespace WardrobeApp.Pages.ClothingItemCRUD
{
    public class DetailsModel : PageModel
    {
        private readonly WardrobeApp.Data.ApplicationDbContext _context;

        public DetailsModel(WardrobeApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ClothingItem ClothingItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingitem = await _context.ClothingItems.FirstOrDefaultAsync(m => m.Id == id);
            if (clothingitem == null)
            {
                return NotFound();
            }
            else
            {
                ClothingItem = clothingitem;
            }
            return Page();
        }
    }
}
