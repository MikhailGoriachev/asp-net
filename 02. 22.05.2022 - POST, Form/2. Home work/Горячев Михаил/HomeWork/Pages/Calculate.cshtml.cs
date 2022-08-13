using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages
{
    public class CalculateModel : PageModel
    {
        // формула
        private Formula _formula;

        public Formula Formula
        {
            get => _formula;
            set => _formula = value;
        }



        // обработка запроса GET
        public void OnGet()
        {

        }


        // обработка запроса POST
        public void OnPost()
        {
            // установка значений
            _formula = new Formula(double.Parse(Request.Form["alpha"]), double.Parse(Request.Form["beta"]));

            // вычисление
            _formula.Calculate();
        }

    }
}
