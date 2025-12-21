using FinancialCalculator.Models.RefinanceCalculator;

namespace FinancialCalculator.Services
{
    public static class RefinanceUtils
    {
        public static RefinancingViewModel Calculate(RefinancingViewModel vm)
        {
            int N = vm.CurrentTermMonths;
            int k = vm.PaidInstallments;
            int M = N - k;

            vm.RemainingMonths = M;

            decimal a0Raw = AnnuityPayment(vm.CurrentPrincipal, vm.CurrentAprPercent, N);
            decimal bRaw = RemainingBalance(vm.CurrentPrincipal, vm.CurrentAprPercent, N, k, a0Raw);

            decimal penaltyRaw = bRaw * (vm.EarlyRepaymentPenaltyPercent / 100m);

            decimal feesForTotalRaw = (vm.CurrentPrincipal * (vm.NewFeesPercent / 100m)) + vm.NewFeesAmount;
            decimal feesDisplayRaw = vm.NewFeesAmount;

            decimal newPrincipalRaw = bRaw;
            if (vm.CapitalizeFees)
                newPrincipalRaw += penaltyRaw + feesForTotalRaw;

            decimal a1Raw = AnnuityPayment(newPrincipalRaw, vm.NewAprPercent, M);

            decimal totalOldRaw = a0Raw * M;

            decimal totalNewRaw = vm.CapitalizeFees
                ? (a1Raw * M)
                : (a1Raw * M + penaltyRaw + feesForTotalRaw);

            decimal savingsRaw = totalOldRaw - totalNewRaw;

            vm.CurrentMonthlyPayment = Round2(a0Raw);
            vm.RemainingBalance = Round2(bRaw);

            vm.PenaltyAmount = Round2(penaltyRaw);
            vm.FeesAmount = Round2(feesDisplayRaw);

            vm.NewMonthlyPayment = Round2(a1Raw);
            vm.MonthlyDiff = Round2(a0Raw - a1Raw);

            vm.TotalOldRemaining = Round2(totalOldRaw);
            vm.TotalNewRemaining = Round2(totalNewRaw);
            vm.Savings = Round2(savingsRaw);

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

        private static decimal RemainingBalance(decimal principal, decimal aprPercent, int totalMonths, int paidMonths, decimal monthlyPayment)
        {
            if (paidMonths <= 0) return principal;
            if (paidMonths >= totalMonths) return 0m;

            double r = (double)(aprPercent / 100m) / 12.0;

            if (Math.Abs(r) < 1e-12)
            {
                decimal paidPrincipal = (principal / totalMonths) * paidMonths;
                decimal remaining = principal - paidPrincipal;
                return remaining < 0 ? 0m : remaining;
            }

            double P = (double)principal;
            double A = (double)monthlyPayment;
            double pow = Math.Pow(1.0 + r, paidMonths);

            double balance = P * pow - A * ((pow - 1.0) / r);
            if (balance < 0) balance = 0;

            return (decimal)balance;
        }

        private static decimal Round2(decimal x)
            => Math.Round(x, 2, MidpointRounding.AwayFromZero);
    }
}
