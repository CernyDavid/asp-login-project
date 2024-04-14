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
    [Authorize("Admin")]
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AppUser> AppUser { get; set; } = default!;

		public string CurrentFilter { get; set; }

		public async Task OnGetAsync(string searchString)
        {
            AppUser = await _context.Users.ToListAsync();

            CurrentFilter = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
				AppUser = await _context.Users.Where(u => u.Nick.Contains(searchString)).ToListAsync();
			}
        }
    }
}
