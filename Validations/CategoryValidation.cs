using EtarChallenge.Dto.Category;

namespace EtarChallenge.Validations.Category
{
    public class CategoryValidation : AbstractValidator<CategoryDto>
    {
        private readonly DataContext DataContext;

        public CategoryValidation(DataContext _dataContext)
        {
            DataContext = _dataContext;

            RuleFor(x => x.id)
                .MustAsync(IsCatIdExists)
                .WithMessage("Category Id not found.");

            RuleFor(x => x.name)
                .NotEmpty().WithMessage("name is required.")
                .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");

            RuleFor(x => x.description)
                .NotEmpty().WithMessage("description is required.")
                .MaximumLength(255).WithMessage("description must not exceed 255 characters.");
        }

        public async Task<bool> IsCatIdExists(int id, CancellationToken cancellationToken)
        {
            return await DataContext.Categories.FindAsync(id, cancellationToken) != null;
        }
    }
}