using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class GetLocationValidator : AbstractValidator<GetLocationModel>
{
    public GetLocationValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}