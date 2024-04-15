using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WardrobeApp.Data;
using WardrobeApp.Models;

namespace WardrobeApp.Pages.ClothingItemCRUD
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly WardrobeApp.Data.ApplicationDbContext _context;

        public EditModel(WardrobeApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Name { get; set; } = default!;

        [BindProperty]
        public Guid Id { get; set; }

        public ClothingItem ClothingItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingitem =  await _context.ClothingItems.FirstOrDefaultAsync(m => m.Id == id);
            if (clothingitem == null)
            {
                return NotFound();
            }
            ClothingItem = clothingitem;
            Id = ClothingItem.Id;
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ClothingItem = await _context.ClothingItems.FirstOrDefaultAsync(m => m.Id == Id);

            if (ClothingItem == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            ClothingItem.Name = Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClothingItemExists(ClothingItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClothingItemExists(Guid id)
        {
            return _context.ClothingItems.Any(e => e.Id == id);
        }
    }
}
