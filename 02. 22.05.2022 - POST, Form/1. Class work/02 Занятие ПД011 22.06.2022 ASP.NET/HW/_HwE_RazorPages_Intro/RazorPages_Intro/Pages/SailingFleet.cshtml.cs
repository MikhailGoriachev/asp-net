using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages_Intro.Models;

namespace RazorPages_Intro.Pages
{
    public class SailingFleetModel : PageModel
    {
        // коллекция кораблей парусного флота
        private List<Sail> _fleet = new List<Sail>();
        public List<Sail> Fleet {
            get => _fleet; 
            private set => _fleet = value;
        } // Fleet

        // обработка GET-запроса /SailingFleet
        public void OnGet() {
            _fleet.Clear();
            _fleet.AddRange(new[]{
                new Sail("Линейный корабль 'Виктория'", 69.34, 15.8, 3556, 1765, "victory.jpg"),
                new Sail("Чайный клиппер 'Катти Сарк'", 85.4, 10.97, 2133, 1869, "cuttysark.jpg"),
                new Sail("Барк 'Крузенштерн'", 114.5, 14, 1645, 1926, "kruzenstern.jpg"),
            });

            // корректируем место размещения картинок
            _fleet.ForEach(s => s.Image = "/images/task02/" + s.Image);
        } // OnGet

    } // class SailingFleetModel
}
