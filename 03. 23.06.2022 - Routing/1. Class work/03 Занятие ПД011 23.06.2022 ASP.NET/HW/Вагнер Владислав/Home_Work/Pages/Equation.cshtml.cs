using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models;
using Home_Work.Utilities;

namespace Home_Work.Pages
{
    public class Equation : PageModel
    {

        private Count _equation;

        public Count Equate { get => _equation; set => _equation = value; }

        public void OnGet()
        {
        }
        public void OnPost(double A, double  B)
        {
            _equation = new Count(A, B);
            _equation.Calculate();
        }
    }
}
