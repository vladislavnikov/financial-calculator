using FinancialCalculator.Models.RefinanceCalculator;
using FinancialCalculator.Services;
using FinancialCalculator.Validation;

namespace FinancialCalculator.Tests;

public class RefinanceCalculatorTests
{
    [Fact]
    public void Calculate_ZeroApr_NoFees_NoPenalty_HappyPath_ComputesExpectedTotals()
    {
        var vm = new RefinancingViewModel
        {
            CurrentPrincipal = 1200m,
            CurrentAprPercent = 0m,
            CurrentTermMonths = 12,
            PaidInstallments = 0,
            EarlyRepaymentPenaltyPercent = 0m,
            NewAprPercent = 0m,
            NewFeesPercent = 0m,
            NewFeesAmount = 0m,
            CapitalizeFees = false
        };

        RefinanceUtils.Calculate(vm);

        Assert.True(vm.HasResult);
        Assert.Equal(12, vm.RemainingMonths);
        Assert.Equal(100m, vm.CurrentMonthlyPayment);
        Assert.Equal(1200m, vm.RemainingBalance);
        Assert.Equal(0m, vm.PenaltyAmount);
        Assert.Equal(0m, vm.FeesAmount);
        Assert.Equal(100m, vm.NewMonthlyPayment);
        Assert.Equal(0m, vm.MonthlyDiff);
        Assert.Equal(1200m, vm.TotalOldRemaining);
        Assert.Equal(1200m, vm.TotalNewRemaining);
        Assert.Equal(0m, vm.Savings);
    }

    [Fact]
    public void Validate_PaidInstallmentsEqualTerm_BadPath_ReturnsRemainingMonthsError()
    {
        var vm = new RefinancingViewModel
        {
            CurrentPrincipal = 1000m,
            CurrentAprPercent = 5m,
            CurrentTermMonths = 12,
            PaidInstallments = 12,
            NewAprPercent = 5m
        };

        var errors = RefinanceValidations.Validate(vm);

        Assert.Contains(errors, e => e.Message.Contains("Оставащите месеци"));
    }
}

