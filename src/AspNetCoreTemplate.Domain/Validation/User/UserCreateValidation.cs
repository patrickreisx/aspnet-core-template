using FluentValidation;

namespace AspNetCoreTemplate.Domain.Validation.User;

public class UserCreateValidation : AbstractValidator<Entities.User>
{
	public UserCreateValidation()
	{
		RuleFor(x => x.UserName)
			.NotEmpty()
			.WithMessage("UserName is required");

		RuleFor(x => x.Email)
			.NotEmpty()
			.WithMessage("Email is required")
			.EmailAddress()
			.WithMessage("Invalid email");

		RuleFor(x => x.Password)
			.NotEmpty()
			.WithMessage("Password is required")
			.MinimumLength(6)
			.WithMessage("Password must have at least 6 characters");
	}
}
