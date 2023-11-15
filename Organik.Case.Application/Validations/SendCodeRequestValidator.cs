using FluentValidation;
using Organik.Case.Application.Dtos;

namespace Organik.Case.Application.Validations;

public class SendCodeRequestValidator : AbstractValidator<SendCodeRequest>
{
    public SendCodeRequestValidator()
    {
        RuleFor(x => x.SendMethod).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Token).NotEmpty();
    }
}