using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models;
using Home_Work.Models.Figures;
using Home_Work.Utilities;

namespace Home_Work.Pages
{
    public class FiguresCollectionModel : PageModel
    {
        //���������� �����
        public List<iFigure> Figures { get; set; } = new List<iFigure>()
            {
                Utils.GetFigure(),
                Utils.GetFigure(),
                Utils.GetFigure(),
                Utils.GetFigure(),
                Utils.GetFigure(),
            };

        //����� ��������� ��� ���������� 
        public List<iFigure> FiguresCopy = new List<iFigure>();

        //���� �������� ���� ����������� OnGet
        public bool IsDefault = true;

        public void OnGet()
        {
            /*Figures = new List<iFigure>()
            {
                Utils.GetFigure(),
                Utils.GetFigure(),
                Utils.GetFigure(),
                Utils.GetFigure(),
                Utils.GetFigure(),
            };

            //���������� ����� ��� ���������� � �������
            FiguresCopy = Figures.Select((f) => f).ToList();*/
        }

        //���������� ������ ��������
        public void OnGetSortSquaresByArea()
        {
            IsDefault = false;
            FiguresCopy.AddRange(Figures
                .Where((f) => f.FigureType.ToLower().Contains("����"))
                .OrderByDescending(f => f.Area()));
        }

        //���������� ������ ���������������
        public void OnGetSortRectanglesByPerimeter()
        {
            IsDefault = false;
            FiguresCopy = Figures.Where((f) => f.FigureType.ToLower().Contains("�����")).ToList();
            FiguresCopy.Sort((s1, s2) => s1.Perimeter() - s2.Perimeter());
        }

        //���������� ������ ���������������
        public void OnGetReverseTriangles()
        {
            IsDefault = false;
            FiguresCopy = Figures
                .Where((f) => f.FigureType.ToLower().Contains("�����������"))
                .Reverse()
                .ToList();
        }
        //���������� ��������� ������������� �������
        public void OnGetSortBySquare()
        {
            Figures.Sort((f1, f2) => f1.Area() - f2.Area());
        }


        /*���������� ������*/
        public void OnPost()
        {
            Figures.Add(Utils.GetFigure());
            //���������� ����� ��� ���������� � �������
            FiguresCopy = Figures.Select((f) => f).ToList();
        }
    }
}
