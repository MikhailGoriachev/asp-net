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
    [IgnoreAntiforgeryToken]
    public class SailingFleetModel : PageModel
    {
        private List<SailingShip> _sailingShips;
        public List<SailingShip> SailingShips {
            get
            {
                return _sailingShips;
            }
            set { 
                _sailingShips = value;
            } 
        }
        public void OnGet()
        {
            //Инициализация списка
            SailingShips = new List<SailingShip>(){
                new SailingShip("линейный корабль «Виктория»",
                                Utils.GetRandom(18d,30d),
                                Utils.GetRandom(5d,9d),
                                Utils.GetRandom(30_000,100_000),
                                Utils.GetRandom(1800,1910),
                                SailingShip.PicNames[0]),
                new SailingShip("чайный клиппер «Катти Сарк» ",
                                Utils.GetRandom(18d,30d),
                                Utils.GetRandom(5d,9d),
                                Utils.GetRandom(30_000,100_000),
                                Utils.GetRandom(1800,1910),
                                SailingShip.PicNames[1]),
                new SailingShip("барк «Крузенштерн»",
                                Utils.GetRandom(18d,30d),
                                Utils.GetRandom(5d,9d),
                                Utils.GetRandom(30_000,100_000),
                                Utils.GetRandom(1800,1910),
                                SailingShip.PicNames[3]),
            };
        }

        public void OnPost(SailingShip[] sailingShips)
        {
            SailingShips = new List<SailingShip>(sailingShips);
        }
    }
}
