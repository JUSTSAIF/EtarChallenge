namespace EtarChallenge.Services.CategoriesService
{
    public interface ICategoriesService
    {
        public Task<dynamic> Index(int id);
        public Task<dynamic> Create(string name, string des);
        public Task<dynamic> Update(int id, string name, string des);
        public Task<dynamic> Delete(int id);
    }
}