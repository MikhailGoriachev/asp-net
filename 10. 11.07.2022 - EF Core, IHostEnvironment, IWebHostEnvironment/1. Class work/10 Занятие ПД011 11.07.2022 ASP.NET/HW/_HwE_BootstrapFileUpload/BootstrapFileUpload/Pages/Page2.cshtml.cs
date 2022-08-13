using BootstrapFileUpload.Infrastructure;
using BootstrapFileUpload.Models.Task02;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace BootstrapFileUpload.Pages
{
    // ��������� ������� � ����� ������� JSON � ����� App_Data ������� ���������
    // �������� � ��������: ���, ��������, �����, ������, �������������, ���
    // ���������, ���� �������. ������������� ��������� �������� � ��������
    // ������ ����������� ��� ���������� ����� (���������� �� ����� 10 �������). 
    // ���������� ��� ����������:
    //     � ����� ���� ��������� � �������� ������� 
    //     � ����� ���������, ������������� �� ����������� ���� ������������
    //     � ����� ���������, ������������� �� �������� �������������
    //     � ����� ���������, ������������� �� ��������� ��������
    //     � ���������� ������� � ��������� (�����������, � ����������� ����������
    //       ��������� � �����)
    // 

    [IgnoreAntiforgeryToken]
    public class Page2Model : PageModel
    {
        // ������ ��������� ������
        private List<Ship> _fleet = new List<Ship>();

        // �������� ��� PageModel
        public IEnumerable<Ship>? DisplayFleet;

        // ������ ��� ����� ������������� �� �����
        [BindProperty]
        public Ship Ship { get; set; } = new Ship();

        // ������ ��� ����� ��� ������/������
        private string _path = $"{Environment.CurrentDirectory}/App_Data/Task02/fleet.json";
        

        // ��������� �� ������, ��� ������� "���������"
        public string ErrorMsg { get; private set; } = "";

        // ���������� GET-������� � ��������
        public void OnGet() {
            // ���� ���� ���� - ������ ������ �� ����� � ���������
            // ���� ����� ��� - ������������ ��������� � �������� ������
            // � ����
            // ������ �� ����� � ������� JSON
            if (System.IO.File.Exists(_path)) {
               Deserialize();
            } else {
                // ������������ ���������
                _fleet = new List<Ship>(new [] {
                    new Ship("�������� �������", "��������", 69.34, 15.8, 3556, 1765, "victory.jpg"),
                    new Ship("�������� �������", "��� ���������", 63.4, 17.3, 4700, 1838, "tri_sviatitelia.jpg"),
                    new Ship("������ �������", "����� ����", 85.4, 10.97, 2133, 1869, "cuttysark.jpg"),
                    new Ship("������ �������", "���������", 64.7, 11.0, 991, 1869, "thermopylae.jpg"),
                    new Ship("����", "�����������", 114.5, 14, 1645, 1926, "kruzenstern.jpg"),
                    new Ship("����", "�������", 88.0, 12.7, 4750, 1892, "camrad.jpg"),
                });

                // ������ � JSON-�������
                Serialize();
            } // if

            // ��������� ������ � ���������� ��� ������ �� ��������
            // ���������� ������� �������� ������ �� ��������
            DisplayFleet =  _fleet;
        } // OnGet

        // ��������� ����� ����� ���������� ������������ �������
        public void OnPost() {
            // ������ ��������� �� �����
            Deserialize();

            // ������ ��� ����� � ������������ ������

            // �������� ����� � ��������� � ��������� ���������
            _fleet.Add(Ship);
            Serialize();

            // ����� ����������� ���������
            DisplayFleet = _fleet;
        } // OnPost


        // ----------------------------------------------------------------------
        // ������ ��������� �� ����� �  ������� JSON (���������� NewtonSoft)
        private void Deserialize() {
            string json = System.IO.File.ReadAllText(_path);
            _fleet = JsonConvert.DeserializeObject<List<Ship>>(json)!;
        } // Deseizlize

        // ������ ��������� � ���� � ������� JSON (���������� NewtonSoft)
        private void Serialize() {
            string json = JsonConvert.SerializeObject(_fleet);
            System.IO.File.WriteAllText(_path, json);
        } // Serialize
    } // class Page3Model 
}
