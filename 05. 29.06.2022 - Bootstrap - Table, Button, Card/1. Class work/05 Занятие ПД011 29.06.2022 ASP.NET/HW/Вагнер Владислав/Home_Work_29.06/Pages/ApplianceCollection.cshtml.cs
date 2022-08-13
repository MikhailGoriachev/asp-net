using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models.Appliances;
using Home_Work.Utilities;

namespace Home_Work.Pages
{
    public class ApplianceCollectionModel : PageModel
    {
        //��������� ��������
        ApplianceStore store = new ApplianceStore();
        public IEnumerable<Appliance> appliances { get; set; } = new List<Appliance>();

        public void OnGet()
        {
            appliances = store.Appliances;
        }

        //����������� �� ����������
        public void OnPostSortByName()
        {
            appliances = store.OrderByName();
        }

        //����������� �� �������� ����
        public void OnPostSortByPrice()
        {
            appliances = store.OrderByPriceDesc();
        }

        //����������� �� ����������� ����������
        public void OnPostSortByQuantity()
        {
            appliances = store.OrderByQuantityAsc();
        }
        //����������� �� ����������� ����������
        public void OnPostAddAppliance()
        {
            store.AddAppliance(new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(50, 300) * 100,
                                                           Utils.GetRandom(2, 50)));

            //��������� ����������� ���� ������� �������
            appliances = store.Appliances.OrderByDescending(a => a.Id);
        }

        //������� ������� � ��������� ���
        public void OnPostSelectByPrice(int priceLo, int priceHi)
        {
            appliances = store.SelectByPrice(priceLo, priceHi);
        }
    }
}
