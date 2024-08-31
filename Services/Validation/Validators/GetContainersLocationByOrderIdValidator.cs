using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class GetContainersLocationByOrderIdValidator : AbstractValidator<GetContainersLocationByOrderIdModel>
{
    public GetContainersLocationByOrderIdValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}