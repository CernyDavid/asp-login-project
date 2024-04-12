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

namespace WardrobeApp.Areas.Administration.Pages
{
    public class AdminIndexModel : PageModel
    {
        private readonly WardrobeApp.Data.ApplicationDbContext _context;

        public AdminIndexModel(WardrobeApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AppUser> AppUser { get;set; } = default!;

        public async Task OnGetAsync()
        {
            AppUser = await _context.Users.ToListAsync();
        }
    }
}
