using Back_End.Data;
using Back_End.Models;
using FluentValidation;

namespace Back_End.Validator
{
    public class SupplierModelValidator : AbstractValidator<SupplierModel>
    {
        private readonly AppDbContext _context;
        public SupplierModelValidator(AppDbContext context)
        {
            _context = context;    

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(3, 80).WithMessage("Name must be between 3 and 80 characters.");

             RuleFor(x => x.Cnpj)
                .NotEmpty().WithMessage("CNPJ is required.")
                .Length(14).WithMessage("CNPJ must be exactly 14 characters.")
                .Must(IsValidCnpj).WithMessage("Invalid CNPJ.")
                .Must(IsUniqueCnpj).WithMessage("CNPJ already exists.");

            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("CEP is required.")
                .Length(8).WithMessage("CEP must be exactly 8 characters.")
                .Must(IsValidCep).WithMessage("Invalid CEP format. Only numeric characters are allowed.");


            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .Length(3, 100).WithMessage("Country must be between 3 and 100 characters.");

            RuleFor(x => x.RegistDate)
                .NotEmpty().WithMessage("Registration date is required.")
                .Must(BeAValidDate).WithMessage("Invalid date format.");
                //.LessThanOrEqualTo(GetCurrentDateTime()).WithMessage("Registration date cannot be in the future.");

            _context = context;
        }


        private static bool IsValidCnpj(string cnpj)
        {

            if (string.IsNullOrEmpty(cnpj))
            {
                return false;
            }

            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpj.Length != 14)
            {
                return false;
            }

            if (cnpj.Distinct().Count() == 1)
            {
                return false;
            }

            var firstVerifier = CalculateVerifierDigit(cnpj.Substring(0, 12), new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 });
            var secondVerifier = CalculateVerifierDigit(cnpj.Substring(0, 12) + firstVerifier, new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 });
           
            return cnpj.EndsWith($"{firstVerifier}{secondVerifier}");
        }

        private static int CalculateVerifierDigit(string cnpj, int[] weights)
        {
            var sum = cnpj.Select((digit, index) => (digit - '0') * weights[index]).Sum();
            var remainder = sum % 11;
            return remainder < 2 ? 0 : 11 - remainder;
        }

        private bool IsUniqueCnpj(string cnpj)
        {

            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());
            return !_context.Supplier.Any(s => s.Cnpj == cnpj);
        }

        private static bool IsValidCep(string cep)
        {
            
            return !string.IsNullOrEmpty(cep) && cep.All(char.IsDigit) && cep.Length == 8;
        }

        private static bool BeAValidDate(DateTime date)
        {
            
            return date != default(DateTime);
        }

        private static DateTime GetCurrentDateTime()
        {
           
            return DateTime.UtcNow.AddHours(-3); // GMT-3
        }

    }
}
