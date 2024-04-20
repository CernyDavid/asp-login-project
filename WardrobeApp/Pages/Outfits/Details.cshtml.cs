using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WardrobeApp.Data;
using WardrobeApp.Models;

namespace WardrobeApp.Pages.Outfits
{
    public class DetailsModel : PageModel
    {
        private readonly WardrobeApp.Data.ApplicationDbContext _context;

        public DetailsModel(WardrobeApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
                var top = await _context.ClothingItems.FirstOrDefaultAsync(m => m.Id == outfit.TopId);
                var bottom = await _context.ClothingItems.FirstOrDefaultAsync(m => m.Id == outfit.BottomId);
                var footwear = await _context.ClothingItems.FirstOrDefaultAsync(m => m.Id == outfit.FootwearId);
                var headwear = await _context.ClothingItems.FirstOrDefaultAsync(m => m.Id == outfit.HeadwearId);
                if (top != null)
                {
                    outfit.Top = top;
                }
                if (bottom != null)
                {
                    outfit.Bottom = bottom;
                }
                if (footwear != null)
                {
                    outfit.Footwear = footwear;
                }
                if (headwear != null)
                {
                    outfit.Headwear = headwear;
                }
            }
            return Page();
        }
    }
}
