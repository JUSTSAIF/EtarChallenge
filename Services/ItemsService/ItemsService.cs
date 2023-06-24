namespace EtarChallenge.Services.ItemsService
{
    public class ItemsService : IItemsService
    {
        private readonly DataContext DataContext;
        public ItemsService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<Item?> Index(int id)
        {
            var item = await DataContext.Items.FindAsync(id);
            return item;
        }
        public async Task<Item?> Create(string name, string des, float price, int catId, int userId)
        {
            Item item = new Models.Item
            {
                name = name,
                description = des,
                price = price,
                catId = catId,
                createdBy = userId,
                createdAt = DateTime.Now
            };
            await DataContext.Items.AddAsync(item);
            await DataContext.SaveChangesAsync();
            return item;
        }
        public async Task Update(int id, string name, string des, float price, int catId)
        {
            var item = await DataContext.Items.FindAsync(id);
            if (item != null)
            {
                item.name = name;
                item.description = des;
                item.price = price;
                await DataContext.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var item = await DataContext.Items.FindAsync(id);
            if (item != null)
            {
                DataContext.Items.Remove(item);
                await DataContext.SaveChangesAsync();
            }
        }
    }
}