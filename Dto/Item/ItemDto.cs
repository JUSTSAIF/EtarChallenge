#nullable disable

namespace EtarChallenge.Dto.Item
{
    public class ItemDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public int catId { get; set; }
    }
}