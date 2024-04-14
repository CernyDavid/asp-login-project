namespace WardrobeApp.Models
{
    public class Outfit
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public DateOnly Created { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public ClothingItem Headwear { get; set; } = default!;
        public Guid HeadwearId { get; set; }
        public ClothingItem Top { get; set; } = default!;
        public Guid TopId { get; set; }
        public ClothingItem Bottom { get; set; } = default!;
        public Guid BottomId { get; set; }
        public ClothingItem Footwear { get; set; } = default!;
        public Guid FootwearId { get; set; }
        public ClothingItem Accessory { get; set; } = default!;
        public Guid AccessoryId { get; set; }

        public AppUser Owner { get; set; } = default!;
        public Guid OwnerId { get; set; }

    }
}
