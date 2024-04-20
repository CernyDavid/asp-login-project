using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WardrobeApp.Data;
using WardrobeApp.Models;

namespace WardrobeApp.Pages.Outfits
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<SelectListItemWithImage> HeadwearItems { get; set; }
        public List<SelectListItemWithImage> TopItems { get; set; }
        public List<SelectListItemWithImage> BottomItems { get; set; }
        public List<SelectListItemWithImage> FootwearItems { get; set; }

        [BindProperty]
        public OutfitViewModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            HeadwearItems = await GetClothingItemsSelectListByType(ClothingType.Headwear);
            TopItems = await GetClothingItemsSelectListByType(ClothingType.Top);
            BottomItems = await GetClothingItemsSelectListByType(ClothingType.Bottom);
            FootwearItems = await GetClothingItemsSelectListByType(ClothingType.Footwear);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var outfit = new Outfit
            {
                Name = Input.Name,
                HeadwearId = Input.HeadwearId,
                TopId = Input.TopId,
                BottomId = Input.BottomId,
                FootwearId = Input.FootwearId,
                OwnerId = user.Id
            };

            _context.Outfits.Add(outfit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task<List<SelectListItemWithImage>> GetClothingItemsSelectListByType(ClothingType type)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return new List<SelectListItemWithImage>();
            }

            var clothingItems = await _context.ClothingItems
                .Where(c => c.Type == type && c.OwnerId == user.Id)
                .Select(c => new SelectListItemWithImage
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Image = c.Image
                })
                .ToListAsync();
            return clothingItems;
        }
    }

    public class SelectListItemWithImage : SelectListItem
    {
        public byte[] Image { get; set; }
    }

    public class OutfitViewModel
    {
        [Required(ErrorMessage = "Outfit name is required.")]
        public string Name { get; set; }

        public Guid HeadwearId { get; set; }

        public Guid TopId { get; set; }

        public Guid BottomId { get; set; }

        public Guid FootwearId { get; set; }
    }
}
