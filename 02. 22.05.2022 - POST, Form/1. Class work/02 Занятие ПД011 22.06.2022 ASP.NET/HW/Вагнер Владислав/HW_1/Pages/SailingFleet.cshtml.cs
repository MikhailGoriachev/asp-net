using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HW_1.Models;
using HW_1.Utilities;

namespace HW_1.Pages
{
    public class SailingFleetModel : PageModel
    {
        public List<SailingShip> sailingShips;
        public void OnGet()
        {
            //������������� ������
            sailingShips = new List<SailingShip>(){
                new SailingShip("�������� ������� ����������",
                                Utils.GetRandom(18d,30d),
                                Utils.GetRandom(5d,9d),
                                Utils.GetRandom(30_000,100_000),
                                Utils.GetRandom(1800,1910),
                                "~/wwroot/images/ships/victory.jpg"),
                new SailingShip("������ ������� ������ ���� ",
                                Utils.GetRandom(18d,30d),
                                Utils.GetRandom(5d,9d),
                                Utils.GetRandom(30_000,100_000),
                                Utils.GetRandom(1800,1910),
                                "~/wwroot/images/ships/cutty_sark.jpg"),
                new SailingShip("���� ������������",
                                Utils.GetRandom(18d,30d),
                                Utils.GetRandom(5d,9d),
                                Utils.GetRandom(30_000,100_000),
                                Utils.GetRandom(1800,1910),
                                "/wwroot/images/ships/krusenstern.jpg"),
            };
        }
    }
}
