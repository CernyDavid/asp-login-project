namespace WardrobeApp.Models
{
    public class ClothingItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "Unnamed";
        public byte[] Image { get; set; } = Array.Empty<byte>();
        public ClothingType Type { get; set; }
        public AppUser Owner { get; set; }
        public Guid OwnerId { get; set; }

    }
}
