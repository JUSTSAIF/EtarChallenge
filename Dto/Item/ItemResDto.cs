#nullable disable

using EtarChallenge.Dto.Category;
using EtarChallenge.Dto.User;

namespace EtarChallenge.Dto.Item
{
    public class ItemResDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public CategoryDto category { get; set; }
        public DateTime createdAt { get; set; }
        public UserDto CreatedBy { get; set; }
    }
}