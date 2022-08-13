using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models;
using Home_Work.Utilities;

namespace Home_Work.Pages
{
    public class ApplianceModel : PageModel
    {

        //Бытовой прибор
        [BindProperty]
        public Appliance appliance { get; set; } = new Appliance("Холодильник",
                                                                Utils.GetRandom(10_000,500_000), 
                                                                Utils.GetRandom(150, 1000)*100, 
                                                                Utils.GetRandom(2, 50));

        public void OnGet()
        {
        }
        public void OnPost()
        {

        }
    }
}
