using FinancialCalculator.Models.LeasingCalculator;

namespace FinancialCalculator.Helper.Utils.Leasing
{
    public static class LeasingUtils
    {
        public static LeasingResultVm Calculate(LeasingCalculatorViewModel vm)
        {
            if (vm.DownPayment > vm.PriceWithVat)
                throw new ArgumentException("Първоначалната вноска не може да е по-голяма от цената.");

            var financed = vm.PriceWithVat - vm.DownPayment;

            decimal fee = 0m;

            if (vm.ProcessingFeeAmount is > 0m)
            {
                fee = vm.ProcessingFeeAmount.Value;
            }
            else if (vm.ProcessingFeePercent is > 0m)
            {
                var feeBase = vm.ProcessingFeeBase == ProcessingFeeBase.FullPrice
                    ? vm.PriceWithVat
                    : financed;

                fee = Math.Round(
                    feeBase * (vm.ProcessingFeePercent.Value / 100m),
                    2,
                    MidpointRounding.AwayFromZero
                );
            }

            var totalInstallments = vm.MonthlyPayment * vm.Months;
            var totalPaid = vm.DownPayment + fee + totalInstallments;
            var overpayment = totalPaid - vm.PriceWithVat;
            var overpaymentPct = vm.PriceWithVat == 0 ? 0 : (overpayment / vm.PriceWithVat) * 100m;

            var (rMonthly, rAnnualEff) = TrySolveImpliedRate(financed, vm.MonthlyPayment, vm.Months);

            return new LeasingResultVm
            {
                FinancedAmount = financed,
                ProcessingFee = fee,
                TotalInstallments = totalInstallments,
                TotalPaid = totalPaid,
                OverpaymentAmount = overpayment,
                OverpaymentPercent = Math.Round(overpaymentPct, 2),
                ImpliedMonthlyRate = rMonthly,
                ImpliedAnnualEffectiveRate = rAnnualEff
            };
        }

        private static (decimal? montly, decimal? annualEff) TrySolveImpliedRate(decimal pv, decimal pmt, int n)
        {
            if (pv <= 0 || pmt <= 0 || n <= 0)
                return (null, null);

            if (pmt * n < pv * 0.999m)
                return (null, null);

            decimal lo = 0m;
            decimal hi = 1m;

            for (int i = 0; i < 60; i++)
            {
                var mid = (lo + hi) / 2m;
                var pvMid = PresentValueOfAnnuity(pmt, mid, n);

                if (pvMid > pv) lo = mid;
                else hi = mid;
            }

            var r = (lo + hi) / 2m;

            if (r < 0.0000001m)
                return (0m, 0m);

            var annualEff = (decimal)Math.Pow((double)(1m + r), 12) - 1m;

            return (Math.Round(r, 6), Math.Round(annualEff, 6));
        }

        private static decimal PresentValueOfAnnuity(decimal pmt, decimal r, int n)
        {
            if (r == 0m) return pmt * n;
            var pow = (decimal)Math.Pow((double)(1m + r), -n);
            return pmt * (1m - pow) / r;
        }

    }
}
