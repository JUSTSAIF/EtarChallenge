using EtarChallenge.Dto.Auth;

namespace EtarChallenge.Validations.Auth
{
    public class LoginValidation : AbstractValidator<LoginDto>
    {
        private readonly DataContext DataContext;
        public LoginValidation(DataContext _DataContext)
        {
            DataContext = _DataContext;
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(255).WithMessage("Username must not exceed 255 characters.")
                .MustAsync(IsUsernameExists).WithMessage("Username not found.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(100).WithMessage("Password must not exceed 255 characters.");
        }

        private async Task<bool> IsUsernameExists(string username, CancellationToken cancellationToken)
        {
            return await DataContext.Users.AllAsync(user => user.username == username, cancellationToken);
        }
    }
}