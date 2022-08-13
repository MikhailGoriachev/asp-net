using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models;
using Home_Work.Models.Figures;
using Home_Work.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Home_Work.Pages
{
    public class Figures : PageModel
    {
        //���������� �����
        public FiguresCollection collection { get; set; } = new FiguresCollection();

        //����� ��������� ��� ���������� 
        public IEnumerable<IFigure> figures = new List<IFigure>();

        //������ ��� �����������. FormFigure - ����������� ������ � Utils, ��������� ��� ��������� 1-�� onPost ���������� 
        //������������ �������� � ������
        public IFigure AddingFigure { get; set; } = Utils.FormFigure;


        //������ ��� ����������� ������
        public SelectList types = new SelectList(new List<IFigure>());

        //�������� ������ � ����� ����������
        public void LoadDropDown()
        {
            types = new SelectList(collection.Figures.DistinctBy(f => f.FigureType).Select(f => f.FigureType));
        }

        //��� ����� 
        public string FormType = "dropDown";

        public void OnGet()
        {
            FormType = "dropDown";
            figures = collection.Figures;
            LoadDropDown();
        }

        #region ����������
        //�������� ���������
        public void OnGetShowDefault()
        {
            figures = collection.Figures;
            LoadDropDown();
        }

        //���������� ������ ���������
        public void OnGetSortSquaresByArea()
        {
            figures = collection.OrderSquaresByAreaDesc();
            LoadDropDown();
        }

        //���������� ������ ���������������
        public void OnGetSortRectanglesByPerimeter()
        {
            figures = collection.OrderRectsByPerimeter();
            LoadDropDown();
        }

        //������������ � �������� �������
        public void OnGetReverseTriangles()
        {
            figures = collection.ReverseTriangles();
            LoadDropDown();
        }

        //���������� ��������� �� �������� �������
        public void OnGetSortBySquare()
        {
            figures = collection.OrderByAreaDesc();
            LoadDropDown();
        }

        //���������� ��������� �� ����
        public void OnGetSortByType()
        {
            figures = collection.OrderByType();
            LoadDropDown();
        }

        #endregion

        #region ����������
        /*���������� ������ (��������� ������)*/
        public void OnGetAddFigure()
        {
            collection.AddFigure(Utils.GetFigure());

            //���������� � �������� �������
            figures = collection.Figures.OrderByDescending(f => f.Id);
            LoadDropDown();
        }

        //����� ���� ������
        public void OnPostSelectType()
        {
            Utils.FormFigure = Request.Form["figureType"].ToString().ToLower() switch
            {
                "�������" => new Square(0),
                "�������������" => new Rectangle(0d, 0d),
                _ => new Triangle(0d, 0d, 0d),
            };

            AddingFigure = Utils.FormFigure;

            //������ ����� ������ �����
            FormType = "parameters_input";
            LoadDropDown();
            figures = collection.Figures;
        }

        //���������� ���������� ������
        public void OnPostAddParemeters()
        {
            //� ����������� �� ���� ��������� ������ ��������������� ������������ ������ � ���������� ���
            switch (AddingFigure.FigureType.ToLower())
            {
                case "�����������":
                    Triangle triangle = AddingFigure as Triangle;
                    triangle.A = /*int*/double.Parse(Request.Form["SideA"]);
                    triangle.B = /*int*/double.Parse(Request.Form["SideB"]);
                    triangle.C = /*int*/double.Parse(Request.Form["SideC"]);
                    break;
                case "�������������":
                    Rectangle rectangle = AddingFigure as Rectangle;
                    rectangle.A = /*int*/double.Parse(Request.Form["SideA"]);
                    rectangle.B = /*int*/double.Parse(Request.Form["SideB"]);
                    break;

                default:
                    (AddingFigure as Square).A = double.Parse(Request.Form["SideA"]);
                    break;
            }

            //������� ����������� ID
            AddingFigure.Id = collection.Figures.Max(f => f.Id) + 1;
            Utils.LastFigureId++;

            //���������� ������
            collection.AddFigure(AddingFigure ?? Utils.GetFigure());

            //����� � �������� �������
            figures = collection.OrderByIdDesc();

            LoadDropDown();

            FormType = "dropDown";
        }

        #endregion

        //�������� JSON ����� ��� ���������� ��������� 
        public void OnGetDeleteFile()
        {
            collection.DeleteFile();
            figures = collection.Figures;
            LoadDropDown();
        }

    }
}
