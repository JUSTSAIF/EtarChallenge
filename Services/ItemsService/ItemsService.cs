namespace EtarChallenge.Services.ItemsService
{
    public class ItemsService : IItemsService
    {
        public Task<dynamic> Create(string name, string des, float price, int catId)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> Index(int id)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> Update(int id, string name, string des, float price, int catId)
        {
            throw new NotImplementedException();
        }
    }
}