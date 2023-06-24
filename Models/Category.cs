#nullable disable

namespace EtarChallenge.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int createdBy { get; set; }
        public User user { get; set; }
        public DateTime createdAt { get; set; }
    }
}