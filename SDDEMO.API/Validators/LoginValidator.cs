using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using SDDEMO.Application.DataTransferObjects.RequestObjects;
using SDDEMO.Application.Enums;
using SDDEMO.Application.Extensions;

namespace SDDEMO.API.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(a => a.username).
                NotNull().
                NotEmpty().
                WithMessage(ValidationMessages.FieldIsRequired.ToDescriptionString().Replace("{fieldName}", "Kullanıcı Adı"));

            RuleFor(a => a.password).
                NotNull().
                NotEmpty().
                WithMessage(ValidationMessages.FieldIsRequired.ToDescriptionString().Replace("{fieldName}", "Şifre"));
        }
    }
}
