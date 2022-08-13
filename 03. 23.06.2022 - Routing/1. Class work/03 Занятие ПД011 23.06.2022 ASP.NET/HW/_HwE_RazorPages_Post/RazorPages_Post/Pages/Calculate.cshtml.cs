using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RazorPages_Post.Models;

namespace RazorPages_Post.Pages
{
    // Вычисления по варианту 13
    [IgnoreAntiforgeryToken]
    public class CalculateModel : PageModel
    {
        public Variant13 Calc { get; set; }
        public (double Z1, double Z2) Result { get; private set; }

        
        // хотя метод и пустой, но лучше оставить, из-за соображений
        // архитектуры ASP.NET 
        public void OnGet() {
            // Calc = new Variant13 { A = double.NaN, B = double.NaN }; 
        } // OnGet

        // обработка формы
        public void OnPost(double a, double b) {
            Calc = new Variant13 { A = a, B = b };

            Result = Calc.Calc();
        } // OnPost
    } // class CalculateModel
}
