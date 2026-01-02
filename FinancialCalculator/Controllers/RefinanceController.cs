using Microsoft.AspNetCore.Mvc;
using FinancialCalculator.Models.RefinanceCalculator;
using FinancialCalculator.Services;
using FinancialCalculator.Validation;

namespace FinancialCalculator.Controllers
{
    public class RefinanceController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View(new RefinancingViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(RefinancingViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var errors = RefinanceValidations.Validate(vm);
            foreach (var (field, message) in errors)
            {
                if (string.IsNullOrWhiteSpace(field))
                    ModelState.AddModelError(string.Empty, message);
                else
                    ModelState.AddModelError(field, message);
            }

            if (!ModelState.IsValid)
                return View(vm);

            RefinanceUtils.Calculate(vm);

            return View(vm);
        }
    }
}
