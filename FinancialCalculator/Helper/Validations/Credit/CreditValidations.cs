using FinancialCalculator.Models.CreditCalculator;

namespace FinancialCalculator.Validation
{
    public static class CreditValidations
    {
        public static List<(string Field, string Message)> Validate(LoanCalculatorViewModel vm)
        {
            var errors = new List<(string, string)>();

            if (vm.Principal <= 0)
                errors.Add((nameof(vm.Principal), "Главницата трябва да е по-голяма от 0."));

            if (vm.TermMonths <= 0)
                errors.Add((nameof(vm.TermMonths), "Срокът трябва да е поне 1 месец."));

            if (vm.AprPercent < 0 || vm.AprPercent > 100)
                errors.Add((nameof(vm.AprPercent), "Лихвата трябва да е между 0% и 100%."));

            return errors;
        }
    }
}