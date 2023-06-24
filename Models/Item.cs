#nullable disable

namespace EtarChallenge.Models
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public int catId { get; set; }
        public Category category { get; set; }
        public int createdBy { get; set; }
        public User user { get; set; }
        public DateTime createdAt { get; set; }
    }
}