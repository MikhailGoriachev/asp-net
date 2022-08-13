using BindingRouteHandler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BindingRouteHandler.Pages
{
    // �������� 2.
    // �������� ��������� ������ � ������� ������� �� ������� ������
    // (������������, ��� �������, �������, ����, ����������, �����������).
    // � ��������� ������ ���� �� ����� 12 ���������. ��������� �����������
    // ������� �������, ������� ��������� � ����� �� ���������. �� �������,
    // ����������� �� �������� ��������� ����������� �������� GET:
    //     � ����� ���������, ������������� �� ������������
    //     � ����� ���������, ������������� �� �������� ����
    //     � ����� ���������, ������������� �� ����������� ����������
    // � ����� ������� �������� ����, �� ������ submit �������� �����
    // ��������� � ������ � �����, ���������� � �������� ��������. 
    // 
    public class Page2Model : PageModel
    {
        // ��������� ������� �������
        private IEnumerable<Goods> _goods = new List<Goods> {
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

        // ������ ��� ������ ���������
        public IEnumerable<Goods>? DisplayGoods;


        public void OnGet() {
        } // OnGet


        // ����� ���������, ������������� �� �������
        public void OnGetOrderBy(string target) {
            _goods = target switch {
                // �������������� �� ������������
                "name" => _goods.OrderBy(g => g.Name),
                
                // �������������� �� �������� ����
                "price" => _goods.OrderByDescending(g => g.Price),
                
                // ������������� �� ����������� ����������
                "quantity" => _goods.OrderBy(g => g.Price),
                _ => _goods
            };

            // ����� ������������� ��������� 
            DisplayGoods = _goods;
        } // OnGetOrderBy


        // � ����� ������� �������� ����, �� ������ submit �������� �����
        // ��������� � ������ � �����, ���������� � �������� ��������. 
        public void OnPost(int low, int high) {
            // ������� ������ � �� �����
            DisplayGoods = _goods.Where(g => low <= g.Price && g.Price <= high);
        } // OnPost
    } // class Page2Model
}
