using FluentValidation;
using Back_End.Models;
using Back_End.Data;


namespace Back_End.Validator
{
    
    public class MaterialModelValidator : AbstractValidator<MaterialModel>
    {
        private readonly AppDbContext _context;

        public MaterialModelValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .Length(3, 100).WithMessage("Code must be between 3 and 100 characters.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(3, 80).WithMessage("Name must be between 3 and 80 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("Description must be less than 100 characters.");
            RuleFor(x => x.FiscalCode)
                .NotEmpty().WithMessage("Fiscal code is required.")
                .Length(5, 20).WithMessage("Fiscal code must be between 5 and 20 characters.")
                .Matches("^[A-Z0-9]+$").WithMessage("Fiscal code must contain only   letters and numbers.")
                .Must(IsUniqueFiscalCode).WithMessage("Fiscal code already exists.");
            RuleFor(x => x.Specie)
                .NotEmpty().WithMessage("Specie is required.");
            RuleFor(x => x.IdSupplier)
                .Must(ValidationIdSupplier).WithMessage("Invalid Supplier ID.");
        }

        private bool ValidationIdSupplier(int id)
        {
           return _context.Supplier.Any(x => x.Id == id);
        }

        private bool IsUniqueFiscalCode(string fiscalCode)
        {
            
            return !_context.Material.Any(x => x.FiscalCode == fiscalCode);
        }


    }
}