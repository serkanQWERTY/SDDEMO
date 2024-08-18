using FluentValidation;
using SDDEMO.Application.DataTransferObjects.RequestObjects;
using SDDEMO.Domain;

namespace SDDEMO.API.Validators
{
    public class BuildingConfigurationValidator : AbstractValidator<AddBuildingConfigurationsDTO>
    {
        public BuildingConfigurationValidator()
        {
            RuleFor(x => x.BuildingCost).GreaterThan(0).WithMessage("Building cost pozitif olmalıdır.");
            RuleFor(x => x.ConstructionTime).InclusiveBetween(30, 1800).WithMessage("Construction time 30 saniye ile 1800 saniye arası olmalıdır.");
            RuleFor(x => x.BuildingType).NotEmpty().WithMessage("Building type boş olamaz.");
        }
    }
}
