using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators;

public class BrandUpdateValidator : AbstractValidator<BrandUpdateDto>
{
    public BrandUpdateValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("El Id es obligatorio");
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
        RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
    }
}