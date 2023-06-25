using EtarChallenge.Dto.Category;

namespace EtarChallenge.Services.CategoriesService
{
    public class CategoriesService : ICategoriesService
    {
        private readonly DataContext DataContext;
        public CategoriesService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<CategoryResDto?> Index(int id)
        {
            var cat = await DataContext.Categories
                .Include(c => c.user)
                .SingleOrDefaultAsync(c => c.id == id);
            return new CategoryResDto
            {
                id = cat!.id,
                name = cat.name,
                description = cat.description,
                createdAt = cat.createdAt,
                CreatedBy = new Dto.User.UserDto
                {
                    id = cat.user.id,
                    name = cat.user.name,
                    username = cat.user.username
                }
            };
        }
        public async Task<CategoryResDto?> Create(string name, string des, int userId)
        {
            Category category = new Models.Category
            {
                createdAt = DateTime.Now,
                createdBy = userId,
                description = des,
                name = name
            };
            await DataContext.Categories.AddAsync(category);
            await DataContext.SaveChangesAsync();
            var cat = await DataContext.Categories
                .Include(c => c.user)
                .SingleOrDefaultAsync(c => c.id == category.id);
            return new CategoryResDto
            {
                id = cat!.id,
                name = cat.name,
                description = cat.description,
                createdAt = cat.createdAt,
                CreatedBy = new Dto.User.UserDto
                {
                    id = cat.user.id,
                    name = cat.user.name,
                    username = cat.user.username
                }
            };

        }
        public async Task Update(int id, string name, string des)
        {
            var cat = await DataContext.Categories.FindAsync(id);
            if (cat != null)
            {
                cat.name = name;
                cat.description = des;
                await DataContext.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var cat = await DataContext.Categories.FindAsync(id);
            if (cat != null)
            {
                DataContext.Categories.Remove(cat);
                await DataContext.SaveChangesAsync();
            }
        }
    }
}