using System.ComponentModel.DataAnnotations;

namespace FinancialCalculator.Models.LeasingCalculator
{
    public enum ProcessingFeeBase
    {
        [Display(Name = "Върху сумата за финансиране (цена - първоначална вноска)")]
        FinancedAmount = 0,

        [Display(Name = "Върху пълната цена")]
        FullPrice = 1
    }


    public class LeasingCalculatorViewModel
    {
        [Display(Name = "Цена на стоката с ДДС (лв.)")]
        [Range(0.01, 1_000_000_000)]
        public decimal PriceWithVat { get; set; } = 1000m;

        [Display(Name = "Първоначална вноска (лв.)")]
        [Range(0, 1_000_000_000)]
        public decimal DownPayment { get; set; } = 0m;

        [Display(Name = "Срок на лизинга (месеци)")]
        [Range(1, 600)]
        public int Months { get; set; } = 12;

        [Display(Name = "Месечна вноска (лв.)")]
        [Range(0.01, 1_000_000_000)]
        public decimal MonthlyPayment { get; set; } = 100m;

        [Display(Name = "Първоначална такса за обработка (%)")]
        [Range(0, 100)]
        public decimal? ProcessingFeePercent { get; set; }

        [Display(Name = "Първоначална такса за обработка (лв.)")]
        [Range(0, 1_000_000_000)]
        public decimal? ProcessingFeeAmount { get; set; }

        [Display(Name = "Таксата се начислява върху")]
        public ProcessingFeeBase ProcessingFeeBase { get; set; } = ProcessingFeeBase.FinancedAmount;

        public LeasingResultVm? Result { get; set; }
        public string? Error { get; set; }
    }

    public class LeasingResultVm
    {
        public decimal FinancedAmount { get; set; }
        public decimal ProcessingFee { get; set; }
        public decimal TotalInstallments { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal OverpaymentAmount { get; set; }
        public decimal OverpaymentPercent { get; set; }
        public decimal? ImpliedMonthlyRate { get; set; }
        public decimal? ImpliedAnnualEffectiveRate { get; set; }
    }
}
