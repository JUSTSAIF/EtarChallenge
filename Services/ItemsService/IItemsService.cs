namespace EtarChallenge.Services.ItemsService
{
    public interface IItemsService
    {
        public Task<dynamic> Index(int id);
        public Task<dynamic> Create(string name, string des, float price, int catId);
        public Task<dynamic> Update(int id, string name, string des, float price, int catId);
        public Task<dynamic> Delete(int id);
    }
}