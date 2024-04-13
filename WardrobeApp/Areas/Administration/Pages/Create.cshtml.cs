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
using WardrobeApp.Data;
using WardrobeApp.Models;

namespace WardrobeApp.Areas.Administration.Pages
{
    [Authorize("Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateuserInputModel Input { get; set; }

        public AppUser AppUser { get; set; } = default!;

        public string[] Genders = ["Male", "Female", "Other", "Not specified"];

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var hasher = new PasswordHasher<AppUser>();
            AppUser = new AppUser
            {
                Nick = Input.Nickname,
                Email = Input.Email,
                PasswordHash = hasher.HashPassword(new AppUser(), Input.Password),
                SecurityStamp = Input.SecurityStamp,
                Gender = Input.Gender,
                UserName = Input.Email,
                NormalizedUserName = Input.Email.ToUpper(),
                NormalizedEmail = Input.Email.ToUpper(),
                EmailConfirmed = true
            };

            _context.Users.Add(AppUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Dashboard");
        }

        public class CreateuserInputModel
        {
            [Display(Name = "Nickname")]
            public string Nickname { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Security Stamp")]
            public string SecurityStamp { get; set; }

            [Display(Name = "Gender")]
            public string Gender { get; set; }
        }
    }
}
