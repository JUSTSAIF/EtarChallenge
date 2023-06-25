#nullable disable
using EtarChallenge.Dto.User;

namespace EtarChallenge.Dto.Category
{
    public class CategoryResDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; }
        public UserDto CreatedBy { get; set; }
    }
}