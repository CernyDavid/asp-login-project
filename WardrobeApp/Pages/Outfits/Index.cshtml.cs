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
    public class IndexModel : PageModel
    {
        private readonly WardrobeApp.Data.ApplicationDbContext _context;

        public IndexModel(WardrobeApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Outfit> Outfit { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Outfit = await _context.Outfits
                .Include(o => o.Accessory)
                .Include(o => o.Bottom)
                .Include(o => o.Footwear)
                .Include(o => o.Headwear)
                .Include(o => o.Owner)
                .Include(o => o.Top).ToListAsync();
        }
    }
}
