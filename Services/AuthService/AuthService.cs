
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace EtarChallenge.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly DataContext DataContext;
        public AuthService(DataContext dataContext)
        {
            DataContext = dataContext;
        }
        
        public async Task<TokenDto?> Login(string username, string password)
        {
            var user = await DataContext.Users.SingleOrDefaultAsync(x => x.username == username);
            if (user != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.password, password);

                if (passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    var Token = TokenGenerator(user.id);
                    return new TokenDto
                    {
                        Name = user.name,
                        Token = Token
                    };
                }
            }
            return null;
        }
        public string TokenGenerator(int userId)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!);
            Claim[] Claims = new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}