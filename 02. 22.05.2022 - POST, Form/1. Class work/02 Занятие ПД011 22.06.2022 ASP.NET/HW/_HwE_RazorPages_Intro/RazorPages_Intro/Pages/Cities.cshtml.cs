using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages_Intro.Models;

namespace RazorPages_Intro.Pages
{
    public class CitiesModel : PageModel
    {
        // ��������� �������� � �������
        private List<City> _cities = new List<City>();
        public List<City> Cities {
            get => _cities;
            private set => _cities = value;
        } // Cities

        public void OnGet()
        {
            _cities.Clear();
            _cities.AddRange(new[]{
                new City(), // �������
                new City("����������", 1872, "yasinovataia.png", 34_607, 13.58),
                new City("��������", 1754, "gorlovka.png", 244_033, 422),
                new City("��������", 1690, "makeevka.png", 340_377, 426),
                new City("������", 1869, "donetsk.png", 913_323, 385),
            });

            // ������������ ����� ������������ ��������
            _cities.ForEach(c => c.Arms = "/images/task03/" + c.Arms);
        } // OnGet
    }
}
