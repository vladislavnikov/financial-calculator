using FinancialCalculator.Models.CreditCalculator;
using FinancialCalculator.Services;
using FinancialCalculator.Validation;

namespace FinancialCalculator.Tests;

public class CreditCalculatorTests
{
    [Fact]
    public void Calculate_ZeroApr_HappyPath_ComputesExpectedTotals()
    {
        var vm = new LoanCalculatorViewModel
        {
            Principal = 1200m,
            AprPercent = 0m,
            TermMonths = 12
        };

        CreditUtils.Calculate(vm);

        Assert.True(vm.HasResult);
        Assert.Equal(100m, vm.MonthlyPayment);
        Assert.Equal(1200m, vm.TotalPaid);
        Assert.Equal(0m, vm.TotalInterest);
    }

    [Fact]
    public void Validate_NegativePrincipal_BadPath_ReturnsError()
    {
        var vm = new LoanCalculatorViewModel
        {
            Principal = -1m,
            AprPercent = 0m,
            TermMonths = 12
        };

        var errors = CreditValidations.Validate(vm);

        Assert.Contains(errors, e => e.Field == nameof(vm.Principal));
    }
}