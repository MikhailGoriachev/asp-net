using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models.Appliances;
using Home_Work.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Home_Work.Pages
{
    public class Appliances : PageModel
    {
        //Коллекция приборов
        ApplianceStore store = new ApplianceStore();
        public IEnumerable<Appliance> appliances { get; set; } = new List<Appliance>();

        //Добавляемый прибор
        [BindProperty]
        public Appliance appliance { get; set; } = new Appliance();

        //Элементы выпадающего списка
        public SelectList types = new SelectList(new List<Appliance>());

        //Загрузка выпадающего списка
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

        //Упорядочить по именованию
        public void OnGetSortByName()
        {
            appliances = store.OrderByName();
            LoadDropDown();
        }

        //Упорядочить по убыванию цены
        public void OnGetSortByPrice()
        {
            appliances = store.OrderByPriceDesc();
            LoadDropDown();
        }

        //Упорядочить по возрастанию количества
        public void OnGetSortByQuantity()
        {
            appliances = store.OrderByQuantityAsc();
            LoadDropDown();
        }


        //Добавить прибор
        public void OnGetAddAppliance()
        {
            store.AddAppliance(new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(50, 300) * 100,
                                                           Utils.GetRandom(2, 50)));

            //Последний добавленный файл выводим вначале
            appliances = store.Appliances.OrderByDescending(a => a.Id);
            LoadDropDown();
        }

        //Добавить прибори из формы
        public void OnPostAddFromForm()
        {
            //Прилось воспользоватся явным получением значения, поскольку привязка из списка не возвращала никаких значений
            appliance.Name = Request.Form["applianceType"];
            appliance.Image = "";
            store.AddAppliance(appliance);


            appliances = store.Appliances.OrderByDescending(a => a.Id);
            LoadDropDown();
        }
    }
}
