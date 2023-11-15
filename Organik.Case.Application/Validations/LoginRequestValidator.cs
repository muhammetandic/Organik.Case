using FluentValidation;
using Organik.Case.Application.Dtos;

namespace Organik.Case.Application.Validations
{
	public class LoginRequestValidator : AbstractValidator<LoginRequest>
	{
		public LoginRequestValidator()
		{
			RuleFor(x => x.Username).NotEmpty();
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}

