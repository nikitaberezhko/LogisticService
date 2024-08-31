using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class UpdateLocationValidator : AbstractValidator<UpdateLocationModel>
{
    public UpdateLocationValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(x => x.Latitude)
            .GreaterThanOrEqualTo(-90)
            .LessThanOrEqualTo(90);
        
        RuleFor(x => x.Longitude)
            .GreaterThanOrEqualTo(-180)
            .LessThanOrEqualTo(180);
    }
}