using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class GetContainersByOrderIdValidator : AbstractValidator<GetContainersByOrderIdModel>
{
    public GetContainersByOrderIdValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}