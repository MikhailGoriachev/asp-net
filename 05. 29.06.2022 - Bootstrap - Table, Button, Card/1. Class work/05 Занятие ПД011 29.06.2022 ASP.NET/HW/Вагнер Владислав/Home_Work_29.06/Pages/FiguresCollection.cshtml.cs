using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models;
using Home_Work.Models.Figures;
using Home_Work.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Home_Work.Pages
{
    public class FiguresCollectionModel : PageModel
    {
        //���������� �����
        public FiguresSet Set { get; set; } = new FiguresSet();

        //����� ��������� ��� ���������� 
        public IEnumerable<IFigure> Figures = new List<IFigure>();

        /*[BindProperty]*/
        //������ ��� ���������� ����� �����
        //public IFigure Figure { get; set; }

        //������ ��� ����������� ������
        public SelectList types = new SelectList(new List<IFigure>());

        //�������� ������ � ����� ����������
        public void LoadDropDown()
        {
            types = new SelectList(Set.Figures.DistinctBy(f => f.FigureType), "FigureType", "FigureType");
        }
        public void OnGet()
        {
            Figures = Set.Figures;
            /*types = new SelectList(Figures.DistinctBy(f => f.FigureType), "FigureType", "FigureType");*/
            LoadDropDown();
        }

        #region ����������
        //�������� ���������
        public void OnPostShowDefault()
        {
            Figures = Set.Figures;
            LoadDropDown();
        }

        //���������� ������ ���������
        public void OnPostSortSquaresByArea()
        {
            LoadDropDown();
            Figures = Set.OrderSquaresByAreaDesc();
        }

        //���������� ������ ���������������
        public void OnPostSortRectanglesByPerimeter()
        {
            LoadDropDown();
            Figures = Set.OrderRectsByPerimeter();
        }

        //���������� ������ ���������������
        public void OnPostReverseTriangles()
        {
            LoadDropDown();
            Figures = Set.ReverseTriangles();
        }

        //���������� ��������� �� �������� �������
        public void OnPostSortBySquare()
        {
            LoadDropDown();
            Figures = Set.OrderByAreaDesc();
        }

        //���������� ��������� �� ����
        public void OnPostSortByType()
        {
            Figures = Set.OrderByType();
            LoadDropDown();
        }

        #endregion

        #region ����������
        /*���������� ������*/
        public void OnPostAddFigure()
        {
            Set.AddFigure(Utils.GetFigure());

            //���������� � �������� �������
            Figures = Set.Figures.OrderByDescending(f => f.Id);
            LoadDropDown();
        }

        /*���������� ������ �� �����*/
        public void OnPostAddFigureForm()
        {
            //�������� �������
            IFigure Figure = Request.Form["Type"].ToString().ToLower() switch
            {
                "�������" => new Square(int.Parse(Request.Form["SideA"])),
                "�������������" => new Rectangle(int.Parse(Request.Form["SideA"]), int.Parse(Request.Form["SideB"])),
                _ => new Triangle(int.Parse(Request.Form["SideA"]), int.Parse(Request.Form["SideB"]), int.Parse(Request.Form["SideC"])),
            };

            //������� ����������� ID
            Figure.Id = Set.Figures.Max(f => f.Id) + 1;
            Utils.LastFigureId++;

            //���� ������ ������� �� �������, �� ��������� ��������� ������
            Set.AddFigure(Figure ?? Utils.GetFigure());

            //���������� � �������� ������� 
            Figures = Set.Figures.OrderByDescending(f => f.Id);
            LoadDropDown();
        }
        #endregion

        //�������� JSON ����� ��� ���������� ��������� 
        public void OnPostDeleteFile()
        {
            Set.DeleteFile();
            Figures = Set.Figures;
            LoadDropDown();
        }

    }
}
