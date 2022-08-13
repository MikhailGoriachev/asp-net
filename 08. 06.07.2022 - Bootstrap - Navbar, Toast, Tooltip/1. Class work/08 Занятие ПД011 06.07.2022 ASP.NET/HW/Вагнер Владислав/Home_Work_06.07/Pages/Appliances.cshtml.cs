using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models.Appliances;
using Home_Work.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Home_Work.Pages
{
    public class Appliances : PageModel
    {
        //��������� ��������
        ApplianceStore store = new ApplianceStore();
        public IEnumerable<Appliance> appliances { get; set; } = new List<Appliance>();

        //����������� ������
        [BindProperty]
        public Appliance appliance { get; set; } = new Appliance();

        //�������� ����������� ������
        public SelectList types = new SelectList(new List<Appliance>());

        //�������� ����������� ������
        public void LoadDropDown()
        {
            /*types = new SelectList(store.Appliances.DistinctBy(a => a.Name), "Name", "Name");*/
            types = new SelectList(Utils.AppliancesNames());
        }

        public void OnGet()
        {
            appliances = store.Appliances;
            LoadDropDown();
        }

        //����������� �� ����������
        public void OnGetSortByName()
        {
            appliances = store.OrderByName();
            LoadDropDown();
        }

        //����������� �� �������� ����
        public void OnGetSortByPrice()
        {
            appliances = store.OrderByPriceDesc();
            LoadDropDown();
        }

        //����������� �� ����������� ����������
        public void OnGetSortByQuantity()
        {
            appliances = store.OrderByQuantityAsc();
            LoadDropDown();
        }


        //�������� ������
        public void OnGetAddAppliance()
        {
            store.AddAppliance(new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(50, 300) * 100,
                                                           Utils.GetRandom(2, 50)));

            //��������� ����������� ���� ������� �������
            appliances = store.Appliances.OrderByDescending(a => a.Id);
            LoadDropDown();
        }

        //�������� ������� �� �����
        public void OnPostAddFromForm()
        {
            //������� �������������� ����� ���������� ��������, ��������� �������� �� ������ �� ���������� ������� ��������
            appliance.Name = Request.Form["applianceType"];
            appliance.Image = "";
            store.AddAppliance(appliance);


            appliances = store.Appliances.OrderByDescending(a => a.Id);
            LoadDropDown();
        }
    }
}
