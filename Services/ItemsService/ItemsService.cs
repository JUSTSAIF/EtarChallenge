using EtarChallenge.Dto.Category;
using EtarChallenge.Dto.Item;
using EtarChallenge.Dto.User;

namespace EtarChallenge.Services.ItemsService
{
    public class ItemsService : IItemsService
    {
        private readonly DataContext DataContext;
        public ItemsService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<ItemResDto?> Index(int id)
        {
            var data = await DataContext.Items
                .Include(c => c.user)
                .Include(c => c.category)
                .SingleOrDefaultAsync(c => c.id == id);

            return new ItemResDto
            {
                id = data!.id,
                name = data.name,
                price = data.price,
                description = data.description,
                createdAt = data.createdAt,
                category = new CategoryDto
                {
                    id = data.category.id,
                    name = data.category.name,
                    description = data.category.description
                },
                CreatedBy = new UserDto
                {
                    id = data.user.id,
                    name = data.user.name,
                    username = data.user.username
                }
            };
        }
        public async Task<ItemResDto?> Create(string name, string des, float price, int catId, int userId)
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
            var data = await DataContext.Items
                .Include(c => c.user)
                .Include(c => c.category)
                .SingleOrDefaultAsync(c => c.id == item.id);
            return new ItemResDto
            {
                id = data!.id,
                name = data.name,
                price = data.price,
                description = data.description,
                createdAt = data.createdAt,
                category = new CategoryDto
                {
                    id = data.category.id,
                    name = data.category.name,
                    description = data.category.description
                },
                CreatedBy = new UserDto
                {
                    id = data.user.id,
                    name = data.user.name,
                    username = data.user.username
                },
            };

        }
        public async Task Update(int id, string name, string des, float price, int catId)
        {
            var item = await DataContext.Items.FindAsync(id);
            if (item != null)
            {
                item.name = name;
                item.description = des;
                item.price = price;
                item.catId = catId;
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