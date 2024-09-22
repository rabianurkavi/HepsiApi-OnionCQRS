using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Auth.Command.Register
{
    public class RegisterCommandValidator:AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(2)
                .WithName("İsim Soyisim");

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(70)
                .MinimumLength(8)
                .EmailAddress()
                .WithName("E-posta adresi");

            RuleFor(x => x.Password)
               .NotEmpty()
               .MaximumLength(30)
               .MinimumLength(6)
               .WithName("Parola");

            RuleFor(x => x.ConfirmPassword)
               .NotEmpty()
               .MaximumLength(30)
               .MinimumLength(6)
               .WithName("Parola")
               .Equal(x => x.Password);
        }
    }
    
}
