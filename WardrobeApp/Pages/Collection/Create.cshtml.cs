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

namespace WardrobeApp.Pages.ClothingItemCRUD
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly WardrobeApp.Data.ApplicationDbContext _context;
        private readonly UserManager<AppUser> UserManager;

        public CreateModel(WardrobeApp.Data.ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        public IActionResult OnGet()
        {
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        public ClothingItem ClothingItem { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await UserManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
            }

            ClothingItem = new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = Input.Name,
                Type = Input.Type,
                OwnerId = user.Id
            };

            if (Input.Image != null && Input.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Input.Image.CopyToAsync(memoryStream);
                    ClothingItem.Image = memoryStream.ToArray();
                }
            }

            _context.ClothingItems.Add(ClothingItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public class InputModel
        {
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Type")]
            public ClothingType Type { get; set; }

            [Display(Name = "Upload Image")]
            public IFormFile Image { get; set; }

        }
    }
}
