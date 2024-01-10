using BlazorSozluk.Common.Constants;
using BlazorSozluk.Common.Models.RequestModels;
using FluentValidation;

namespace BlazorSozluk.Api.Application.Features.Commands.User;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
	public LoginUserCommandValidator()
	{
		RuleFor(x => x.EmailAdress)
			.NotEmpty().WithMessage(ValidationMessage.NotEmpty_EN)
			.NotEmpty().WithMessage(ValidationMessage.NotNull_EN)
			.EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage(ValidationMessage.NotValidEmail_EN);

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage(ValidationMessage.NotEmpty_EN)
			.NotEmpty().WithMessage(ValidationMessage.NotNull_EN)
			.MinimumLength(6).WithMessage(ValidationMessage.MinimumLength_EN);
    }
}
