using FluentValidation;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Validators
{
    public class BucketItemValidator : AbstractValidator<BucketItem>
    {
        public BucketItemValidator()
        {
            RuleFor(item => item.ItemDescription)
                .NotEmpty()
                .WithMessage("Description cannot be empty")
                .Length(3, 50)
                .WithMessage("Length must be between 3 and 50");

        }
    }
}
