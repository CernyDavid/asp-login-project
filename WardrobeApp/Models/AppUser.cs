using Microsoft.AspNetCore.Identity;

namespace WardrobeApp.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Nick { get; set; } = "";
        public string Gender { get; set; } = "Not specified";
        public DateOnly DateOfCreation { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public ICollection<ClothingItem> Clothes { get; set; } = new List<ClothingItem>();
    }
}
