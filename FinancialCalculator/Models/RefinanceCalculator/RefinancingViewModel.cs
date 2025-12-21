using System.ComponentModel.DataAnnotations;

namespace FinancialCalculator.Models.RefinanceCalculator
{
    public class RefinancingViewModel
    {
        [Display(Name = "Размер на кредита (главница)")]
        [Range(0.01, double.MaxValue)]
        public decimal CurrentPrincipal { get; set; } = 100000m;

        [Display(Name = "Годишна лихва (%)")]
        [Range(0, 100)]
        public decimal CurrentAprPercent { get; set; } = 6.5m;

        [Display(Name = "Срок (месеци)")]
        [Range(1, 1000)]
        public int CurrentTermMonths { get; set; } = 240;

        [Display(Name = "Платени вноски (месеци)")]
        [Range(0, 1000)]
        public int PaidInstallments { get; set; } = 24;

        [Display(Name = "Такса предсрочно погасяване (%)")]
        [Range(0, 100)]
        public decimal EarlyRepaymentPenaltyPercent { get; set; } = 1.0m;

        // ----- New loan -----
        [Display(Name = "Нова годишна лихва (%)")]
        [Range(0, 100)]
        public decimal NewAprPercent { get; set; } = 5.2m;

        [Display(Name = "Първоначални такси (%)")]
        [Range(0, 100)]
        public decimal NewFeesPercent { get; set; } = 1.0m;

        [Display(Name = "Първоначални такси (сума)")]
        [Range(0, double.MaxValue)]
        public decimal NewFeesAmount { get; set; } = 0m;

        [Display(Name = "Капитализирай таксите към новата главница (по избор)")]
        public bool CapitalizeFees { get; set; } = false;
        public bool HasResult { get; set; }

        public int RemainingMonths { get; set; }

        public decimal CurrentMonthlyPayment { get; set; }
        public decimal RemainingBalance { get; set; }

        public decimal PenaltyAmount { get; set; }
        public decimal FeesAmount { get; set; }

        public decimal NewMonthlyPayment { get; set; }

        public decimal MonthlyDiff { get; set; }

        public decimal TotalOldRemaining { get; set; }
        public decimal TotalNewRemaining { get; set; }

        public decimal Savings { get; set; }
    }
}
