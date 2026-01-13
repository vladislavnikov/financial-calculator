using FinancialCalculator.Models.CreditCalculator;

namespace FinancialCalculator.Services
{
    public static class CreditUtils
    {
        public static LoanCalculatorViewModel Calculate(LoanCalculatorViewModel vm)
        {
            decimal monthly = AnnuityPayment(vm.Principal, vm.AprPercent, vm.TermMonths);
            decimal total = monthly * vm.TermMonths;
            decimal interest = total - vm.Principal;

            vm.MonthlyPayment = Round2(monthly);
            vm.TotalPaid = Round2(total);
            vm.TotalInterest = Round2(interest);
            vm.HasResult = true;
            return vm;
        }

        private static decimal AnnuityPayment(decimal principal, decimal aprPercent, int months)
        {
            if (months <= 0) return 0m;

            double r = (double)(aprPercent / 100m) / 12.0;
            if (Math.Abs(r) < 1e-12)
                return principal / months;

            double P = (double)principal;
            double denom = 1.0 - Math.Pow(1.0 + r, -months);
            return (decimal)(P * r / denom);
        }

        private static decimal Round2(decimal x)
            => Math.Round(x, 2, MidpointRounding.AwayFromZero);
    }
}
