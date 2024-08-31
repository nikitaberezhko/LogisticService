using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class UpdateContainersLocationValidator : AbstractValidator<UpdateContainersLocationModel>
{
    public UpdateContainersLocationValidator()
    {
        RuleFor(x => x.ContainersList).NotEmpty();

        RuleFor(x => x.ContainersList)
            .Must(x => x.All(e => e.Id != Guid.Empty))
            .Must(x => x.All(e => e.Latitude >= -90 && e.Latitude <= 90))
            .Must(x => x.All(e => e.Longitude >= -180 && e.Longitude <= 180));
    }
}