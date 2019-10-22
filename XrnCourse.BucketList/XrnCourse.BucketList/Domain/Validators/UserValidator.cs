using FluentValidation;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Validators
{

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                    .WithMessage("E-mail cannot be empty")
                .EmailAddress()
                    .WithMessage("Please enter a valid e-mail address");

            RuleFor(bucket => bucket.UserName)
                .NotEmpty()
                .WithMessage("Please enter a valid username");

            //todo: ensure email and username are unique
        }
    }
}
