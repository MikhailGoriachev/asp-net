using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BootstrapIntro.Models.Task01;
using BootstrapIntro.Models.Task02;
using Newtonsoft.Json;

namespace BootstrapIntro.Pages
{
    // �������� 1.
    // �������� ��������� ������ � ������� ������� �� ������� ������
    // (������������, ��� �������, �������, ����, ����������, �����������).
    // � ��������� ������ ���� �� ����� 12 ���������. ��������� �����������
    // ������� �������, ������� ��������� � ����� ������� JSON � �����
    // App_Data �������.
    //
    // ����������� ������ ������ �������� ������� ������� ���������� ���
    // ������ ��������, ������������� ���������������� �������� Bootstrap.
    //
    // �� �������-�������, ����������� �� �������� ��������� �����������
    // �������� GET:
    //     � ����� ���������, ������������� �� ������������
    //     � ����� ���������, ������������� �� �������� ����
    //     � ����� ���������, ������������� �� ����������� ����������
    // 
    public class Page1Model : PageModel
    {
        // ��������� ������� �������
        private IEnumerable<Goods> _goods = new List<Goods>();

        // ������ ��� ������ ���������
        public IEnumerable<Goods>? DisplayGoods;

        // ������ ��� ����� ��� ������/������
        private string _path = $"{Environment.CurrentDirectory}/App_Data/goods.json";

        // GET-������ ��������
        public void OnGet() {
            // ���� ���� ���� - ������ ������ �� ����� � ���������
            // ���� ����� ��� - ������������ ��������� � �������� ������
            // � ����

            if (System.IO.File.Exists(_path)) {
                Deserialize();
            } else  {
                // ������������ ���������
                _goods = new List<Goods> {
                    new ("������������� ����", "DEXP MS-70", "4726172", 6379, 12, "microwave_oven.jpg"),
                    new ("�������-�����", "Rekam RVL-1670G", "8150067", 5799, 5, "robot_vc.jpg"),
                    new ("�������� ���������", "DEXP BLN-3", "1385289", 1884, 6, "pan_cacker.jpg"),
                    new ("�������-�����", "ELAR1 Smartbot Brush", "4721104", 11119, 7, "robot_vc.jpg"),
                    new ("����������� � �������������", "DEXP RE-TD160NMA/W", "1319870", 22399, 5, "fridge.jpg"),
                    new ("������������� ����", "Gorenje MO17ELS", "8199146", 6699, 3, "microwave_oven.jpg"),
                    new ("���������� ������", "BEKO WSRE612Z-SW", "1118440", 35099, 3, "wash_machine.jpg"),
                    new ("�������-�����", "Scarlett SC-VC80R12", "5313977", 8989, 6, "robot_vc.jpg"),
                    new ("��������", "Maxwell MV-19HBN", "1098490", 3334, 5, "pan_cacker.jpg"),
                    new ("������������� ����", "Winia KOR-7707SW", "8199146", 8264, 2, "microwave_oven.jpg"),
                    new ("����������� ����������", "������ �70", "8116403", 15959, 3, "fridge.jpg"),
                    new ("���������� ������", "������ WM-HB712/10", "5327321", 35099, 3, "wash_machine.jpg"),
                    new ("��������", "Tefal PY-303635", "0174095", 6184, 4, "pan_cacker.jpg"),
                    new ("������������� ����", "Hisense H20MOWS1H", "5330121", 7249, 7, "microwave_oven.jpg"),
                };

                // ������ � JSON-�������
                Serialize();
            } // if

            // ��������� ������ �� ��������� � ���� ��� ������ � ���������� 
            // �������� ������ � ������������� ���� ��������
            DisplayGoods = _goods;
            ViewData["Mode"] = "source";
        } // OnGet


        // ����� ���������, ������������� �� �������
        public void OnGetOrderBy(string target) {
            // ������ ��������� �� �����
            Deserialize();

            // �������������� ��������� �� �������
            switch (target) {
                // �������������� �� ������������
                case "name":
                    _goods = _goods.OrderBy(g => g.Name);
                    ViewData["Mode"] = "name";
                    break;
                // �������������� �� �������� ����
                case "price":
                    _goods = _goods.OrderByDescending(g => g.Price);
                    ViewData["Mode"] = "price";
                    break;
                // ������������� �� ����������� ����������
                case "quantity":
                    _goods = _goods.OrderBy(g => g.Quantity);
                    ViewData["Mode"] = "quantity";
                    break;
            } // switch

            // ����� ������������� ��������� 
            DisplayGoods = _goods;
        } // OnGetOrderBy

        // ----------------------------------------------------------------------
        // ������ ��������� �� ����� �  ������� JSON (���������� NewtonSoft)
        private void Deserialize() {
            string json = System.IO.File.ReadAllText(_path);
            _goods = JsonConvert.DeserializeObject<List<Goods>>(json)!;
        } // Deseizlize

        // ������ ��������� � ���� � ������� JSON (���������� NewtonSoft)
        private void Serialize() {
            string json = JsonConvert.SerializeObject(_goods);
            System.IO.File.WriteAllText(_path, json);
        } // Serialize
    } // class Page1Model
}
