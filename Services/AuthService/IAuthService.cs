namespace EtarChallenge.Services.Auth
{
    public interface IAuthService
    {
        public Task<dynamic> Login(string username, string password);
    }
}