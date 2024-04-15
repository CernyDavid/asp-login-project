using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WardrobeApp.Data;
using WardrobeApp.Models;

namespace WardrobeApp.Pages.Outfits
{
    public class CreateModel : PageModel
    {
        private readonly WardrobeApp.Data.ApplicationDbContext _context;

        public CreateModel(WardrobeApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AccessoryId"] = new SelectList(_context.ClothingItems, "Id", "Id");
        ViewData["BottomId"] = new SelectList(_context.ClothingItems, "Id", "Id");
        ViewData["FootwearId"] = new SelectList(_context.ClothingItems, "Id", "Id");
        ViewData["HeadwearId"] = new SelectList(_context.ClothingItems, "Id", "Id");
        ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
        ViewData["TopId"] = new SelectList(_context.ClothingItems, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Outfit Outfit { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Outfits.Add(Outfit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
