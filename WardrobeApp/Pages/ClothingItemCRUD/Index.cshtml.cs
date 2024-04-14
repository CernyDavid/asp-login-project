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
    public class IndexModel : PageModel
    {
        private readonly WardrobeApp.Data.ApplicationDbContext _context;

        public IndexModel(WardrobeApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ClothingItem> ClothingItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ClothingItem = await _context.ClothingItems
                .Include(c => c.Owner).ToListAsync();
        }
    }
}
