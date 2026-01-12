using FinancialCalculator.Helper.Utils.Leasing;
using FinancialCalculator.Models.LeasingCalculator;

namespace FinancialCalculator.Tests;

public class LeasingCalculatorTests
{
    [Fact]
    public void Calculate_NoFees_ZeroOverpayment_HappyPath_ComputesExpectedResult()
    {
        var vm = new LeasingCalculatorViewModel
        {
            PriceWithVat = 1200m,
            DownPayment = 200m,
            Months = 10,
            MonthlyPayment = 100m,
            ProcessingFeeAmount = null,
            ProcessingFeePercent = null,
            ProcessingFeeBase = ProcessingFeeBase.FinancedAmount
        };

        var result = LeasingUtils.Calculate(vm);

        Assert.Equal(1000m, result.FinancedAmount);
        Assert.Equal(0m, result.ProcessingFee);
        Assert.Equal(1000m, result.TotalInstallments);
        Assert.Equal(1200m, result.TotalPaid);
        Assert.Equal(0m, result.OverpaymentAmount);
        Assert.Equal(0m, result.OverpaymentPercent);
        Assert.Equal(0m, result.ImpliedMonthlyRate);
        Assert.Equal(0m, result.ImpliedAnnualEffectiveRate);
    }

    [Fact]
    public void Calculate_DownPaymentGreaterThanPrice_BadPath_Throws()
    {
        var vm = new LeasingCalculatorViewModel
        {
            PriceWithVat = 1200m,
            DownPayment = 1200.01m,
            Months = 10,
            MonthlyPayment = 100m
        };

        Assert.Throws<ArgumentException>(() => LeasingUtils.Calculate(vm));
    }
}

