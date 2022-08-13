using BootstrapForms.Infrastructure;
using BootstrapForms.Models.Task02;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BootstrapForms.Pages
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
        
        // ���������� �� ����� ��� ����� ���������� ������
        public bool RenderParamForm { get; private set; } = false;

        // ��� ������, ��� ������� ��������� ��������� ����� ����� ����������
        public string FigType { get; private set; } = "none";

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


        // �������� ������ �� ��������� ����
        public void OnPostCreateFigure(string figType) {
            // ������ ��������� �����
            Deserialize();

            // ���������� �������� ������
            IFigure figure = figType switch {
                "square" => new Square(1),
                "rectangle" => new Rectangle(1, 1),
                _ => new Triangle()
            };

            // ������� ������ � ������ ���������, ��� �������� ������
            // ���������� ��������� �����
            _figures.Insert(0, figure);
            Serialize();

            // ������������ ��������� ��� �����������, � ����������� ���������
            Figures = _figures;
            RenderParamForm = true;
            FigType = figType;
        } // OnPostCreateFigure


        // ��������� ���������� ��� ����������� ������
        public void OnPostSetFigParam(string figType, double a, double b, double c) {
            // ������ ��������� �����
            Deserialize();

            // ���������� ��������� ������
            switch (figType) {
                case "triangle":
                    _figures[0] = new Triangle(a, b, c);
                    break;

                case "square":
                    _figures[0] = new Square(a);
                    break;

                default:
                    _figures[0] = new Rectangle(a, b);
                    break;
            } // switch

            // ��������� ��������� �����
            Serialize();

            // ���������� ��������� �����
            RenderParamForm = false;
            Figures = _figures;
        } // OnPostSetFigParam


        // ----------------------------------------------------------------------
        // ������ ��������� �� ����� �  ������� JSON (���������� NewtonSoft)
        private void Deserialize() {
            string json = System.IO.File.ReadAllText(_path);
            _figures = JsonConvert.DeserializeObject<List<IFigure>>(json, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Objects,
            })!;
        } // Deserialize

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
