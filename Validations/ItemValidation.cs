using EtarChallenge.Dto.Item;

namespace EtarChallenge.Validations.Item
{
    public class ItemValidation : AbstractValidator<ItemDto>
    {
        private readonly DataContext DataContext;
        public ItemValidation(DataContext dataContext)
        {
            DataContext = dataContext;

            RuleFor(x => x.id)
                .MustAsync(IsItemIdExists)
                .WithMessage("Item Id not found.");

            RuleFor(x => x.name)
                .NotEmpty().WithMessage("name is required.")
                .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");

            RuleFor(x => x.description)
                .NotEmpty().WithMessage("description is required.")
                .MaximumLength(255).WithMessage("description must not exceed 255 characters.");

            RuleFor(x => x.price)
                .ExclusiveBetween(0, 9999999);

            RuleFor(x => x.catId)
                .MustAsync(IsCatIdExists)
                .WithMessage("Category Id not found.");
        }

        public async Task<bool> IsItemIdExists(int id, CancellationToken cancellationToken)
        {
            return await DataContext.Items.FindAsync(id, cancellationToken) != null;
        }

        public async Task<bool> IsCatIdExists(int id, CancellationToken cancellationToken)
        {
            return await DataContext.Categories.FindAsync(id, cancellationToken) != null;
        }
    }
}