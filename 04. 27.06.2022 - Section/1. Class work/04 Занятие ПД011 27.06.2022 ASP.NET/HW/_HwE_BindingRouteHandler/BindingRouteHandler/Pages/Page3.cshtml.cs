using BindingRouteHandler.Infrastructure;
using BindingRouteHandler.Models.Task03;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BindingRouteHandler.Pages
{
    [IgnoreAntiforgeryToken]
    public class Page3Model : PageModel
    {
        // ������ ��������� ������, �������������� (persistent) ���������
        private /* static */ List<IFigure> _figures = new List<IFigure>(new IFigure []
        {
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

        // �������� ��� PageModel
        public IEnumerable<IFigure> Figures = new List<IFigure>();

        // ���������� GET-������� � ��������
        public void OnGet() {
            // ��������� ������ � ���������� ��� ������ �� ��������
            Figures =  _figures;
        } // OnGet


        // ����� ������ ���������, ������������� �� �������� �������
        public void OnGetSquaresOnly() {
            // ������������ ��������� ��� �����������, � �������� ����������
            Figures = _figures
                .Where(f => f.Type == "�������")
                .OrderByDescending(f => f.Area());
        } // OnGetSquaresOnly


        // ����� ������ ���������������, ������������� �� ����������� ����������
        public void OnGetRectanglesOnly() {
            // ������������ ��������� ��� �����������, � �������� ����������
            // Figures.Clear();
            Figures = _figures
                .Where(f => f.Type == "�������������")
                .OrderBy(f => f.Perimeter());
        } // OnGetRectanglesOnly


        // ����� ������ �������������, � �������� �������, �� ��������� � ������� � ���������
        public void OnGetTrianglesOnly() {
            // ������������ ��������� ��� �����������, � �������� ����������
            // Figures.Clear();
            Figures = _figures
                .Where(f => f.Type == "�����������")
                .Reverse();
        } // OnGetTrianglesOnly


        // ����� ��������� �� �������� �������
        public void OnGetOrderByAreaDesc() {
            // ������������ ��������� ��� �����������, � �������� ����������
            // Figures.Clear();
            Figures = _figures.OrderByDescending(f => f.Area());
        } // OnGetOrderByAreaDesc


        // ���������� POST-������� � ��������
        public void OnPost() {
            // ������������ ������� ��� ���������� � ���������
            // ��� ���� ��������
            int type = Utils.GetRandom(0, 2);

            // ���������� �������� ������
            IFigure figure = type switch {
                1 => new Square(Utils.GetRandom(20, 50)),
                2 => new Rectangle(Utils.GetRandom(20, 50), Utils.GetRandom(20, 50)),
                _ => new Triangle()
            };
            _figures.Insert(0, figure);

            // ������������ ��������� ��� �����������, � ����������� ���������
            Figures = _figures;
        } // OnPost
    } // class Page3Model 
}
