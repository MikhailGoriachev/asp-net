using H_W6ASP_NET.Infrastructure;
using H_W6ASP_NET.Models.Task02;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace H_W6ASP_NET.Pages
{
    public class FiguresModel : PageModel
    {
        // ��������� ������ � �������
        private List<IFigure> _figures = new List<IFigure>();


        public IEnumerable<IFigure> Figures = new List<IFigure>();

        // ������ ��� ����� ��� ������/������
        private string _path = $"{Environment.CurrentDirectory}/App_Data/figure.json";


        // ���������� GET-������� � ��������
        public void OnGet()
        {
            if (System.IO.File.Exists(_path))
            {
                Deserialize();
            }
            else {
                // ������������ ���������
                _figures = new List<IFigure>(new IFigure[] {
                    new Square(4d),
                    new Square(5d),
                    new Rectangle(2d, 6d),
                    new Rectangle(7d, 12d),
                    new Triangle((2d, 3d, 4d)),
                    new Triangle((4d, 7d, 8d)),
                    new Square(8.3),
                    new Rectangle(5d, 12d),
                    new Rectangle(7.2, 9.5),
                    new Triangle((3d, 4d, 5d))
                });

                // ������ � JSON-�������
                Serialize();
            }

            // ��������� ������ ��� ������ 
            Figures = _figures;
        } // OnGet


        // ����� ������ ���������, �� �������� �������
        public void OnGetSquareAreaDesc()
        {
            Deserialize();
            Figures = _figures
                .Where(f => f.Type == "�������")
                .OrderByDescending(f => f.Area());
        } // OnGetSquareAreaDesc


        // ����� ������ ���������������, �� ����������� ���������
        public void OnGetRectanglePerim()
        {
            Deserialize();
            Figures = _figures
                .Where(f => f.Type == "�������������")
                .OrderBy(f => f.Perimeter());
        } // OnGetRectanglePerim

        // ����� ������ �������������,
        // � �������� ������� �� ��������� � ������� � ���������
        public void OnGetTriangleRevers()
        {
            Deserialize();
            Figures = _figures
                .Where(f => f.Type == "�����������")
                .Reverse();
        } // OnGetTriangleRevers

        // ����� ���� ��������� �� �������� �������
        public void OnGetOrderByAreaDesc() {

            Deserialize();
            Figures = _figures.OrderByDescending(f => f.Area());
        } // OnGetOrderByAreaDesc

        // ����� ���� ���������, ������������� �� ���� �����
        public void OnGetOrderByType() {
           
            Deserialize();
            Figures = _figures.OrderBy(f => f.Type);
        } // OnGetOrderByType


        // ������ �����
        public SelectList TypesFigure { get; } = new SelectList(new List<string>() { "�������������", "�����������", "�������" });


        // ������� a
        [BindProperty]
        public double SideA { get; set; }

        // ������� b
        [BindProperty]
        public double SideB { get; set; }

        // ������� c
        [BindProperty]
        public double SideC { get; set; }

        // ��� ������
        [BindProperty]
        public string? FigureType { get; set; }


        // ���������� ������
        public void OnPostAddFigure(string type)
        {
            Deserialize();

            // ������� � ������ ���������
            _figures.Insert(0, FigureType switch
            {
                "�������" => new Square() { A = SideA },
                "�������������" => new Rectangle() { A = SideA, B = SideB },
                _ => new Triangle() { Sides = (SideA, SideB, SideC) }
            });

            // ��������� ������� ���������
            Figures = _figures;

            // ���������� ������
            Serialize();
        }

        // ��������� ���������� ������
        public void OnGetAddFigureRand()
        {
            Deserialize();

            // ������������ ������� ��� ���������� � ���������
            // ��� ���� ��������
            int type = Utils.GetRandom(0, 2);

            // ���������� �������� ������
            IFigure figure = type switch
            {
                1 => new Square(Utils.GetRandom(10d, 20d)),
                2 => new Rectangle(Utils.GetRandom(5d, 7d), Utils.GetRandom(12d, 15d)),
                _ => new Triangle()
            };
            _figures.Insert(0, figure);

            // ������ � JSON-�������
            Serialize();

            // ��������� � ����������� ���������
            Figures = _figures;
        } // OnGetAddFigure


        #region ������ � ������ ���������
        // ������ ��������� �� ����� � ������� JSON 
        private void Deserialize()
        {
            string json = System.IO.File.ReadAllText(_path);
            _figures = JsonConvert.DeserializeObject<List<IFigure>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
            })!;
        } // Deseizlize

        // ������ ��������� � ���� � ������� JSON 
        public void Serialize()
        {
            string json = JsonConvert.SerializeObject(_figures, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                });
            System.IO.File.WriteAllText(_path, json);
        } // Serialize
        #endregion

    } // class FiguresModel
}
