using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WardrobeApp.Data;
using WardrobeApp.Models;
using static WardrobeApp.Areas.Administration.Pages.CreateModel;

namespace WardrobeApp.Areas.Administration.Pages
{
    [Authorize("Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EditUserInputModel Input { get; set; }

        public AppUser AppUser { get; set; } = default!;

        public string[] Genders = ["Male", "Female", "Other", "Not specified"];

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
            AppUser = appuser;
            Input = new EditUserInputModel
            {
                Id = AppUser.Id,
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            AppUser = await _context.Users.FirstOrDefaultAsync(m => m.Id == Input.Id);

            if (AppUser == null)
            {
                Console.WriteLine("AppUser is null");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            var hasher = new PasswordHasher<AppUser>();

            if (Input.Password != null)
            {
                AppUser.PasswordHash = hasher.HashPassword(new AppUser(), Input.Password);
            }
            AppUser.Nick = Input.Nickname;
            AppUser.Email = Input.Email;
            AppUser.UserName = Input.Email;
            AppUser.NormalizedUserName = Input.Email.ToUpper();
            AppUser.NormalizedEmail = Input.Email.ToUpper();
            AppUser.SecurityStamp = Input.SecurityStamp;
            AppUser.Gender = Input.Gender;
            if (Input.EmailConfirmed)
            {
                Console.WriteLine("Email confirmed");
                AppUser.EmailConfirmed = true;
            }
            else
            {
                Console.WriteLine("Email not confirmed");
                AppUser.EmailConfirmed = false;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(AppUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Dashboard");
        }

        private bool AppUserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public class EditUserInputModel
        {
            [Display(Name = "Nickname")]
            public string Nickname { get; set; }

            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Security Stamp")]
            public string SecurityStamp { get; set; }

            [Display(Name = "Gender")]
            public string Gender { get; set; }

            [Display(Name = "Email Confirmed")]
            public bool EmailConfirmed { get; set; }

            public Guid Id { get; set; }
        }
    }
}
