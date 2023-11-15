using FluentValidation;
using Organik.Case.Application.Dtos;

namespace Organik.Case.Application.Validations
{
	public class CheckCodeValidator : AbstractValidator<CheckCodeRequest>
	{
		public CheckCodeValidator()
		{
			RuleFor(x => x.UserId).NotEmpty();
			RuleFor(x => x.Token).NotEmpty();
			RuleFor(x => x.Code).NotEmpty();
		}
	}
}

