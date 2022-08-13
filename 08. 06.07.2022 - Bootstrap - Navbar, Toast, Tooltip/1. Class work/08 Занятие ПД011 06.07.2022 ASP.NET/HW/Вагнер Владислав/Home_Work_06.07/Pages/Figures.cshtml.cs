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
        //������������ �������� � ������ ������������
        public IFigure AddingFigure { get; set; } = Utils.FormFigure;


        //������ ��� ����������� ������
        public SelectList types = new SelectList(new List<IFigure>());


        //��� ����� 
        public string FormType = "dropDown";

        public void OnGet()
        {
            FormType = "dropDown";
            figures = collection.Figures;
        }

        #region ����������
        //�������� ���������
        public void OnGetShowDefault() 
            => figures = collection.Figures;

        //���������� ������ ���������
        public void OnGetSortSquaresByArea()
            => figures = collection.OrderSquaresByAreaDesc();

        //���������� ������ ���������������
        public void OnGetSortRectanglesByPerimeter()
            =>figures = collection.OrderRectsByPerimeter();

        //������������ � �������� �������
        public void OnGetReverseTriangles()
            => figures = collection.ReverseTriangles();
            

        //���������� ��������� �� �������� �������
        public void OnGetSortBySquare()
            => figures = collection.OrderByAreaDesc();

        //���������� ��������� �� ����
        public void OnGetSortByType()
            => figures = collection.OrderByType();

        #endregion

        #region ����������
        /*���������� ������ (��������� ������)*/
        public void OnGetAddFigure()
        {
            collection.AddFigure(Utils.GetFigure());

            //���������� � �������� �������
            figures = collection.Figures.OrderByDescending(f => f.Id);
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
            
            figures = collection.Figures;
        }

        //���������� ���������� ������
        public void OnPostAddParameters()
        {
            //� ����������� �� ���� ��������� ������ ��������������� ������������ ������ � ���������� ���
            switch (AddingFigure.FigureType.ToLower())
            {
                case "�����������":
                    Triangle triangle = AddingFigure as Triangle;
                    triangle.A = double.Parse(Request.Form["SideA"]);
                    triangle.B = double.Parse(Request.Form["SideB"]);
                    triangle.C = double.Parse(Request.Form["SideC"]);
                    break;
                case "�������������":
                    Rectangle rectangle = AddingFigure as Rectangle;
                    rectangle.A = double.Parse(Request.Form["SideA"]);
                    rectangle.B = double.Parse(Request.Form["SideB"]);
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

            FormType = "dropDown";
        }

        #endregion

        //�������� JSON ����� ��� ���������� ��������� 
        public void OnGetDeleteFile()
        {
            collection.DeleteFile();
            figures = collection.Figures;
        }

    }
}
