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

namespace WardrobeApp.Pages.Outfits
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
        public Outfit Outfit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outfit = await _context.Outfits.FirstOrDefaultAsync(m => m.Id == id);

            if (outfit == null)
            {
                return NotFound();
            }
            else
            {
                Outfit = outfit;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outfit = await _context.Outfits.FindAsync(id);
            if (outfit != null)
            {
                Outfit = outfit;
                _context.Outfits.Remove(Outfit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
