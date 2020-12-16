
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BMICalculator.Pages
{
    public class BmiModel : PageModel
    {
        [BindProperty]
        public Bmi BMI { get; set; }
    }
}