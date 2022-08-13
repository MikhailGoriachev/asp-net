using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using BootstrapIntro.Infrastructure;
using BootstrapIntro.Models.Task02;
using Newtonsoft.Json;


namespace BootstrapIntro.Pages
{
    [IgnoreAntiforgeryToken]
    public class Page2Model : PageModel
    {
        // ������ ��������� ������, �������������� (persistent) ���������
        private List<IFigure> _figures = new List<IFigure>();

        // �������� ��� PageModel
        public IEnumerable<IFigure> Figures = new List<IFigure>();

        // ������ ��� ����� ��� ������/������
        private string _path = $"{Environment.CurrentDirectory}/App_Data/figures.json";

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
                _figures = new List<IFigure>(new IFigure[] {
                    new Square(8),
                    new Square(7),
                    new Rectangle(5, 6),
                    new Rectangle(9, 12),
                    new Triangle(12, 18, 20),
                    new Triangle(5, 11, 9),
                    new Square(10),
                    new Rectangle(7, 10),
                    new Triangle(8, 10, 8)
                });

                // ������ � JSON-�������
                Serialize();
            } // if

            // ��������� ������ � ���������� ��� ������ �� ��������
            // ���������� ������� �������� ������ �� ��������
            Figures =  _figures;
            ViewData["Mode"] = "source";
        } // OnGet


        // ����� ������ ���������, ������������� �� �������� �������
        public void OnGetSquaresOnly() {
            // ��������� ������ �� �����
            Deserialize();

            // ������������ ��������� ��� �����������, � �������� ����������
            Figures = _figures
                .Where(f => f.Type == "�������")
                .OrderByDescending(f => f.Area());
            
            // ��� ��������� ������ ������ �������� ������ ���������
            ViewData["Mode"] = "squares";
        } // OnGetSquaresOnly


        // ����� ������ ���������������, ������������� �� ����������� ����������
        public void OnGetRectanglesOnly() {
            // ��������� ������ �� �����
            Deserialize();

            // ������������ ��������� ��� �����������, � �������� ����������
            // Figures.Clear();
            Figures = _figures
                .Where(f => f.Type == "�������������")
                .OrderBy(f => f.Perimeter());

            // ��� ��������� ������ ������ �������� ������ ���������
            ViewData["Mode"] = "rectangles";
        } // OnGetRectanglesOnly


        // ����� ������ �������������, � �������� �������, �� ��������� � ������� � ���������
        public void OnGetTrianglesOnly() {
            // ��������� ������ �� �����
            Deserialize();

            // ������������ ��������� ��� �����������, � �������� ����������
            // Figures.Clear();
            Figures = _figures
                .Where(f => f.Type == "�����������")
                .Reverse();

            // ��� ��������� ������ ������ �������� ������ ���������
            ViewData["Mode"] = "triangles";
        } // OnGetTrianglesOnly


        // ����� ��������� �� �������� �������
        public void OnGetOrderByAreaDesc() {
            // ��������� ������ �� �����
            Deserialize();

            // ������������ ��������� ��� �����������, � �������� ����������
            Figures = _figures.OrderByDescending(f => f.Area());

            // ��� ��������� ������ ������ �������� ������ ���������
            ViewData["Mode"] = "areas";
        } // OnGetOrderByAreaDesc


        // ����� ���� ���������, ������������� �� ���� �����
        public void OnGetOrderByFigType() {
            // ��������� ������ �� �����
            Deserialize();

            // ������������ ��������� ��� �����������, � �������� ����������
            Figures = _figures.OrderBy(f => f.Type);

            // ��� ��������� ������ ������ �������� ������ ���������
            ViewData["Mode"] = "types";
        } // OnGetOrderByFigType

        // �������� ������ �� �������
        public void OnGetCreateFigure() {
            // ������ ��������� �����
            Deserialize();

            // ������������ ������� ��� ���������� � ���������
            // ��� ���� ��������
            int type = Utils.GetRandom(0, 2);

            // ���������� �������� ������
            IFigure figure = type switch {
                1 => new Square(Utils.GetRandom(20, 50)),
                2 => new Rectangle(Utils.GetRandom(20, 50), Utils.GetRandom(20, 50)),
                _ => new Triangle()
            };

            // ������� ������ � ������ ��������, ��� �������� ������
            _figures.Insert(0, figure);
            Serialize();

            // ������������ ��������� ��� �����������, � ����������� ���������
            Figures = _figures;
        } // OnGetCreateFigure


        // ----------------------------------------------------------------------
        // ������ ��������� �� ����� �  ������� JSON (���������� NewtonSoft)
        private void Deserialize() {
            string json = System.IO.File.ReadAllText(_path);
            _figures = JsonConvert.DeserializeObject<List<IFigure>>(json, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Objects,
            })!;
        } // Deseizlize

        // ������ ��������� � ���� � ������� JSON (���������� NewtonSoft)
        public void Serialize() {
            string json = JsonConvert.SerializeObject(_figures, Formatting.Indented,
                new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.Objects,
                });
            System.IO.File.WriteAllText(_path, json);
        } // Serialize
    } // class Page3Model 
}
