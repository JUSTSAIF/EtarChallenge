namespace EtarChallenge.Services.CategoriesService
{
    public class CategoriesService : ICategoriesService
    {
        private readonly DataContext DataContext;
        public CategoriesService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<Category?> Index(int id)
        {
            var cat = await DataContext.Categories.FindAsync(id);
            return cat;
        }
        public async Task<Category?> Create(string name, string des, int userId)
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
            return category;
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