using FluentValidation;
using SDDEMO.Application.DataTransferObjects.RequestObjects;
using SDDEMO.Application.Enums;

namespace SDDEMO.API.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .Length(3, 50).WithMessage("Kullanıcı adı 3 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.mailAddress)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .Length(6, 100).WithMessage("Şifre 6 ile 100 karakter arasında olmalıdır.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");
        }
    }
}
