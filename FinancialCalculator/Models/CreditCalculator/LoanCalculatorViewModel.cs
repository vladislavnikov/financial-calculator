using System.ComponentModel.DataAnnotations;

namespace FinancialCalculator.Models.CreditCalculator
{
    public class LoanCalculatorViewModel
    {
        [Display(Name = "Размер на кредита (главница)")]
        [Range(0.01, double.MaxValue)]
        public decimal Principal { get; set; } = 100000m;

        [Display(Name = "Годишна лихва (%)")]
        [Range(0, 100)]
        public decimal AprPercent { get; set; } = 6.5m;

        [Display(Name = "Срок (месеци)")]
        [Range(1, 1000)]
        public int TermMonths { get; set; } = 240;

        public bool HasResult { get; set; }

        [Display(Name = "Месечна вноска")]
        public decimal MonthlyPayment { get; set; }

        [Display(Name = "Общо платена сума")]
        public decimal TotalPaid { get; set; }

        [Display(Name = "Общо лихви")]
        public decimal TotalInterest { get; set; }
    }
}
