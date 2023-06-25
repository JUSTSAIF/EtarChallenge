namespace EtarChallenge.Services.Auth
{
    public interface IAuthService
    {
        public Task<TokenDto?> Login(string username, string password);
        public string TokenGenerator(int userId);
    }
}