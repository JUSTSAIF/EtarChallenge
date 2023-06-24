namespace EtarChallenge.Services.ItemsService
{
    public interface IItemsService
    {
        public Task<Item?> Index(int id);
        public Task<Item?> Create(string name, string des, float price, int catId, int userId);
        public Task Update(int id, string name, string des, float price, int catId);
        public Task Delete(int id);
    }
}