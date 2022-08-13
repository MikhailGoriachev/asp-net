using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RoutingIntro.Pages
{
    public class Page04Model : PageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }

        public void OnGet(int id, string name, int  salary)
        {
            Id = id;
            Name = name;
            Salary = salary;
        }

        // пример еще одного обработчика запроса GET
        public void OnGetNameToUpper(int id, string name, int salary)
        {
            Id = id;
            Name = name.ToUpper();
            Salary = salary;
        }
    }
}
