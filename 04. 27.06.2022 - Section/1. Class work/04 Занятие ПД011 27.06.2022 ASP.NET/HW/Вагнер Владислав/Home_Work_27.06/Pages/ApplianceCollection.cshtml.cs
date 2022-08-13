using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models;
using Home_Work.Utilities;

namespace Home_Work.Pages
{
    public class ApplianceCollectionModel : PageModel
    {
        //��������� ��������
        public List<Appliance> Appliances { get; set; } = Utils.AppliancesStatic;/*
        {
            new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(15_000, 100_000),
                                                           Utils.GetRandom(2, 50)),
            new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(15_000, 100_000),
                                                           Utils.GetRandom(2, 50)),
            new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(15_000, 100_000),
                                                           Utils.GetRandom(2, 50)),
            new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(15_000, 100_000),
                                                           Utils.GetRandom(2, 50)),
            new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(15_000, 100_000),
                                                           Utils.GetRandom(2, 50)),
            new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(15_000, 100_000),
                                                           Utils.GetRandom(2, 50)),
            new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(15_000, 100_000),
                                                           Utils.GetRandom(2, 50)),
            new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(15_000, 100_000),
                                                           Utils.GetRandom(2, 50)),
            new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(15_000, 100_000),
                                                           Utils.GetRandom(2, 50)),
            new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(15_000, 100_000),
                                                           Utils.GetRandom(2, 50)),
            new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(15_000, 100_000),
                                                           Utils.GetRandom(2, 50)),
            new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(15_000, 100_000),
                                                           Utils.GetRandom(2, 50)),
        }*/
        public List<Appliance> Selected { get; set; } = new List<Appliance>();

        //������� ����� OnGet �� ���� �������-������������, ��������� ����� �������� ��������������� � ������ �������� �� 
        //����������. ������� ����������, �� ������� �� �������
        #region GET-�����������
        public void OnGet()
        {

            //for (int i = 0; i < 12; i++)
            //{
            //    Appliances.Add(new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
            //                                               Utils.GetRandom(15_000, 100_000),
            //                                               Utils.GetRandom(2, 50)));
            //}//for
            ///*Selected = new List<Appliance>();*/
        }

        //����������� �� ����������
        public void OnGetSortByName()
        {
            /*OnGet();
            Appliances.Sort((a1, a2) => a1.Name.CompareTo(a2.Name));*/
            Utils.AppliancesStatic.Sort((a1, a2) => a1.Name.CompareTo(a2.Name));
        }
        //����������� �� �������� ����
        public void OnGetSortByPrice()
        {
            /*OnGet();
            Appliances.Sort((a1, a2) => a2.Price - a1.Price);*/
            Utils.AppliancesStatic.Sort((a1, a2) => a2.Price - a1.Price);
        }

        //����������� �� ����������� ����������
        public void OnGetSortByQuantity()
        {
            /*OnGet();
            Appliances.Sort((a1, a2) => a1.Quantity - a2.Quantity) ;*/
            Utils.AppliancesStatic.Sort((a1, a2) => a1.Quantity - a2.Quantity) ;
        }
        #endregion

        //������� ������� � ��������� ���
        public void OnPost(int priceLo, int priceHi)
        {
            /*OnGet();
            Selected = Appliances.Where((a) => a.Price >= priceLo && a.Price <= priceHi).ToList();*/
            Selected = Utils.AppliancesStatic.Where((a) => a.Price >= priceLo && a.Price <= priceHi).ToList();
        }
    }
}
