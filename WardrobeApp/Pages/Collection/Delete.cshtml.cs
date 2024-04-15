using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WardrobeApp.Data;
using WardrobeApp.Models;

namespace WardrobeApp.Pages.ClothingItemCRUD
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly WardrobeApp.Data.ApplicationDbContext _context;

        public DeleteModel(WardrobeApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingitem = await _context.ClothingItems.FindAsync(id);
            if (clothingitem != null)
            {
                ClothingItem = clothingitem;
                _context.ClothingItems.Remove(ClothingItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
