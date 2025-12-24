using Microsoft.AspNetCore.Mvc;
using FinancialCalculator.Models.CreditCalculator;
using FinancialCalculator.Services;
using FinancialCalculator.Validation;

namespace FinancialCalculator.Controllers
{
    public class CreditController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View(new LoanCalculatorViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoanCalculatorViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var errors = CreditValidations.Validate(vm);
            foreach (var (field, message) in errors)
            {
                if (string.IsNullOrWhiteSpace(field))
                    ModelState.AddModelError(string.Empty, message);
                else
                    ModelState.AddModelError(field, message);
            }

            if (!ModelState.IsValid)
                return View(vm);

            CreditUtils.Calculate(vm);

            return View(vm);
        }
    }
}