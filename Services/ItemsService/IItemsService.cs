using EtarChallenge.Dto.Item;

namespace EtarChallenge.Services.ItemsService
{
    public interface IItemsService
    {
        public Task<ItemResDto?> Index(int id);
        public Task<ItemResDto?> Create(string name, string des, float price, int catId, int userId);
        public Task Update(int id, string name, string des, float price, int catId);
        public Task Delete(int id);
    }
}