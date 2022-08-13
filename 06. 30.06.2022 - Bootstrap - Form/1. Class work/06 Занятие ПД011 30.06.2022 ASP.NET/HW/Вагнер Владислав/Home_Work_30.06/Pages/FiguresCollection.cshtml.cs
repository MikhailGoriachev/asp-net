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

        public void OnGet()
        {
            Figures = Set.Figures;
        }

        #region ����������
        //�������� ���������
        public void OnGetShowDefault()
        {
            Figures = Set.Figures;
        }

        //���������� ������ ���������
        public void OnGetSortSquaresByArea()
        {
            Figures = Set.OrderSquaresByAreaDesc();
        }

        //���������� ������ ���������������
        public void OnGetSortRectanglesByPerimeter()
        {
            Figures = Set.OrderRectsByPerimeter();
        }

        //���������� ������ ���������������
        public void OnGetReverseTriangles()
        {
            Figures = Set.ReverseTriangles();
        }

        //���������� ��������� �� �������� �������
        public void OnGetSortBySquare()
        {
            Figures = Set.OrderByAreaDesc();
        }

        //���������� ��������� �� ����
        public void OnGetSortByType()
        {
            Figures = Set.OrderByType();
        }

        #endregion

        #region ����������
        /*���������� ������*/
        public void OnGetAddFigure()
        {
            Set.AddFigure(Utils.GetFigure());

            //���������� � �������� �������
            Figures = Set.Figures.OrderByDescending(f => f.Id);
        }

        #endregion

        //�������� JSON ����� ��� ���������� ��������� 
        public void OnGetDeleteFile()
        {
            Set.DeleteFile();
            Figures = Set.Figures;
        }

    }
}
