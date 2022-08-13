using H_W6ASP_NET.Models.Task1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace H_W6ASP_NET.Pages
{
    public class AppliancesModel : PageModel
    {

        // ��������� ������ � ������� �������
        private static List<Appliance> _appliances = new List<Appliance>();

        public List<Appliance> Appliances = new List<Appliance>();

        // ������ ��� ����� ��� ������/������
        private string _path = $"{Environment.CurrentDirectory}/App_Data/appliance.json";


        // ���������� GET-������� � ��������
        public void OnGet()
        {
            if (System.IO.File.Exists(_path))
            {
                Deserialize();
            }
            else {
                // ������������ ���������
                _appliances = new List<Appliance>(new[]
                {
                    new Appliance("���������� ������",    "������� ���.�������", "OC8532", 34000, 3, "appliance01.jpg"),
                    new Appliance("������� ����",         "������� ���.�������", "OC8642", 31000, 2, "appliance02.jpg"),
                    new Appliance("������������� ������", "������ ���.�������", "BK2591", 1500, 5, "appliance03.jpg"),
                    new Appliance("���",                  "������ ���.�������", "BK8368", 5050, 7, "appliance04.jpg"),
                    new Appliance("�������",              "������ ���.�������", "BK8420", 6300, 3, "appliance05.jpg"),
                    new Appliance("�������� �������",     "������� ���.�������", "OC8523", 8200, 2, "appliance06.jpg"),
                    new Appliance("������������� ����",   "������ ���.�������", "BK8307", 4000, 4, "appliance07.jpg"),
                    new Appliance("�����������",          "������ ���.�������", "BK8592", 5400, 2, "appliance08.jpg"),
                    new Appliance("�����������",          "������� ���.�������", "OC9132", 44000, 3, "appliance09.jpg"),
                    new Appliance("������������� ������", "������� ���.�������", "OC8824", 36000, 2, "appliance10.jpg"),
                    new Appliance("���������",            "������� ���.�������", "OC4612", 28000, 4, "appliance11.jpg"),
                    new Appliance("�����������",          "������� ���.�������", "OC9524", 26400, 5, "appliance12.jpg"),
                });

                // ������ � JSON-�������
                Serialize();
            } 

            // ��������� ������ ��� ������ 
            Appliances = _appliances;
        } // OnGet


        // ����� ���������, ������������� �� ������������
        public void OnGetOrderByTitle() {
            Deserialize();
            Appliances = _appliances.OrderBy(a => a.Title).ToList();
        }


        // ����� ���������, ������������� �� �������� ����
        public void OnGetOrderByPriceDesc() {
            Deserialize();
            Appliances = _appliances.OrderByDescending(a => a.Price).ToList();
        }


        // ����� ���������, ������������� �� ����������� ����������
        public void OnGetOrderByAmount() {
            Deserialize();
            Appliances = _appliances.OrderBy(a => a.Amount).ToList();
        }

        // ������� �������� ������
        public static Dictionary<string, string> FileNames { get; } = new()
        {
            ["�����������"] = "appliance12.jpg",
            ["���"] = "appliance04.jpg",
            ["�����������"] = "appliance09.jpg",
            ["���������� ������"] = "appliance01.jpg",
            ["������� ����"] = "appliance02.jpg",
            ["�����������"] = "appliance08.jpg"
        };

        // ������ ������������ �������
        public SelectList NameAppliance { get; } = new SelectList(FileNames.Select(f => f.Key));


        [BindProperty] // ������ ��� ����� ������ ����� �������       
        public Appliance Appliance { get; set; } = new ("�����������", "������� ���.�������", "OC9524", 26400, 5, "appliance12.jpg");

        public void OnGetAddAppliance()
        {
            // ��������� ����� �� ���� �������
            Appliance.Image = FileNames[Appliance.Title];

            _appliances.Insert(0, Appliance);
            Serialize();

            Appliances = _appliances;
        }


        #region ������ � ������ ���������
        // ������ ��������� �� ����� � ������� JSON 
        private void Deserialize()
        {
            string json = System.IO.File.ReadAllText(_path);
            _appliances = JsonConvert.DeserializeObject<List<Appliance>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
            })!;
        } // Deseizlize

        // ������ ��������� � ���� � ������� JSON
        public void Serialize()
        {
            // ������ � JSON-�������
            string json = JsonConvert.SerializeObject(_appliances);
            System.IO.File.WriteAllText(_path, json);
        } // Serialize
        #endregion

    } // class AppliancesModel
}
