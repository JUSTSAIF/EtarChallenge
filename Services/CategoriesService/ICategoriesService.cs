using EtarChallenge.Dto.Category;

namespace EtarChallenge.Services.CategoriesService
{
    public interface ICategoriesService
    {
        public Task<CategoryResDto?> Index(int id);
        public Task<CategoryResDto?> Create(string name, string des, int userId);
        public Task Update(int id, string name, string des);
        public Task Delete(int id);
    }
}