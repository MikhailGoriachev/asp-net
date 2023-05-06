using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages
{
    public class SailingFleetModel : PageModel
    {
        // коллекция кораблей
        private List<Ship> _shipList;
        public List<Ship> ShipList
        {
            get { return _shipList; }
            set { _shipList = value; }
        }


        // обработка запроса GET
        public void OnGet()
        {
            _shipList = _shipList ?? new List<Ship> {
                new Ship("Виктория", 46, 12.2, 1300, 1721, "victory.jpeg"),
                new Ship("Катти Сарк", 85.4, 11.2, 936, 1869, "sark.png"),
                new Ship("Крузенштерн", 46, 14, 5805, 1926, "krusenstern.jpg")
            };
        }
    }
}
