using FinancialCalculator.Models.RefinanceCalculator;

namespace FinancialCalculator.Validation
{
    public static class RefinanceValidations
    {
        public static List<(string Field, string Message)> Validate(RefinancingViewModel vm)
        {
            var errors = new List<(string, string)>();

            if (vm.CurrentTermMonths <= 0)
                errors.Add((nameof(vm.CurrentTermMonths), "Срокът трябва да е поне 1 месец."));

            if (vm.PaidInstallments < 0)
                errors.Add((nameof(vm.PaidInstallments), "Платените вноски не може да са отрицателни."));

            if (vm.PaidInstallments > vm.CurrentTermMonths)
                errors.Add((nameof(vm.PaidInstallments), "Платените вноски не може да са повече от срока."));

            int M = vm.CurrentTermMonths - vm.PaidInstallments;
            if (M <= 0)
                errors.Add(("", "Оставащите месеци са 0 (кредитът изглежда изплатен)."));

            if (vm.CurrentPrincipal <= 0)
                errors.Add((nameof(vm.CurrentPrincipal), "Главницата трябва да е по-голяма от 0."));

            if (vm.CurrentAprPercent < 0 || vm.CurrentAprPercent > 100)
                errors.Add((nameof(vm.CurrentAprPercent), "Лихвата трябва да е между 0% и 100%."));

            if (vm.NewAprPercent < 0 || vm.NewAprPercent > 100)
                errors.Add((nameof(vm.NewAprPercent), "Новата лихва трябва да е между 0% и 100%."));

            return errors;
        }
    }
}
