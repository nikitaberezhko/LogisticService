using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class GetContainersLocationValidator : AbstractValidator<GetContainersLocationModel>
{
    public GetContainersLocationValidator()
    {
        RuleFor(x => x.IdsList).NotEmpty();

        RuleFor(x => x.IdsList).Must(x => x.All(e => e != Guid.Empty));
    }
}