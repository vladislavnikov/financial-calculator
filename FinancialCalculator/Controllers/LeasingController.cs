using Microsoft.AspNetCore.Mvc;
using FinancialCalculator.Models.LeasingCalculator;
using FinancialCalculator.Helper.Utils.Leasing;

namespace FinancialCalculator.Controllers
{
    public class LeasingController : Controller
    {
        

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LeasingCalculatorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LeasingCalculatorViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                vm.Result = LeasingUtils.Calculate(vm);
            }
            catch (Exception ex)
            {
                vm.Error = ex.Message;
            }

            return View(vm);
        }
    }
}
