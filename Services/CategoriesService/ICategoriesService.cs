namespace EtarChallenge.Services.CategoriesService
{
    public interface ICategoriesService
    {
        public Task<Category?> Index(int id);
        public Task<Category?> Create(string name, string des, int userId);
        public Task Update(int id, string name, string des);
        public Task Delete(int id);
    }
}