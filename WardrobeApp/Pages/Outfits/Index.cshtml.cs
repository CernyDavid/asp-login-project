using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> UserManager;

        public IndexModel(WardrobeApp.Data.ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        public IList<Outfit> Outfit { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return;
            }

            Outfit = await _context.Outfits
                .Include(o => o.Bottom)
                .Include(o => o.Footwear)
                .Include(o => o.Headwear)
                .Include(o => o.Owner)
                .Include(o => o.Top).Where(o => o.OwnerId == user.Id).ToListAsync();
        }
    }
}
