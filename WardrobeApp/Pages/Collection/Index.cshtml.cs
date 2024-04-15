using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WardrobeApp.Data;
using WardrobeApp.Models;

namespace WardrobeApp.Pages.ClothingItemCRUD
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly WardrobeApp.Data.ApplicationDbContext _context;
        private readonly UserManager<AppUser> UserManager;

        public IndexModel(WardrobeApp.Data.ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        public IList<ClothingItem> ClothingItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var user = await UserManager.GetUserAsync(User);

            if (user == null)
            {
                return;
            }

            ClothingItem = await _context.ClothingItems
                .Include(c => c.Owner).Where(c => c.OwnerId == user.Id).ToListAsync();
        }
    }
}
