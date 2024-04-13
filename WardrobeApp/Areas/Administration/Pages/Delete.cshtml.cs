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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AppUser AppUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appuser = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (appuser == null)
            {
                return NotFound();
            }
            else
            {
                AppUser = appuser;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appuser = await _context.Users.FindAsync(id);
            if (appuser != null)
            {
                AppUser = appuser;
                _context.Users.Remove(AppUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Dashboard");
        }
    }
}
