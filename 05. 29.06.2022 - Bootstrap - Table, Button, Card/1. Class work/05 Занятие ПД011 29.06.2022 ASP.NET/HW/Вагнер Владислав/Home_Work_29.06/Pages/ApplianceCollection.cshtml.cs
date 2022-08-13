using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models.Appliances;
using Home_Work.Utilities;

namespace Home_Work.Pages
{
    public class ApplianceCollectionModel : PageModel
    {
        //Коллекция приборов
        ApplianceStore store = new ApplianceStore();
        public IEnumerable<Appliance> appliances { get; set; } = new List<Appliance>();

        public void OnGet()
        {
            appliances = store.Appliances;
        }

        //Упорядочить по именованию
        public void OnPostSortByName()
        {
            appliances = store.OrderByName();
        }

        //Упорядочить по убыванию цены
        public void OnPostSortByPrice()
        {
            appliances = store.OrderByPriceDesc();
        }

        //Упорядочить по возрастанию количества
        public void OnPostSortByQuantity()
        {
            appliances = store.OrderByQuantityAsc();
        }
        //Упорядочить по возрастанию количества
        public void OnPostAddAppliance()
        {
            store.AddAppliance(new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(50, 300) * 100,
                                                           Utils.GetRandom(2, 50)));

            //Последний добавленный файл выводим вначале
            appliances = store.Appliances.OrderByDescending(a => a.Id);
        }

        //Выборка записей в диапазоне цен
        public void OnPostSelectByPrice(int priceLo, int priceHi)
        {
            appliances = store.SelectByPrice(priceLo, priceHi);
        }
    }
}
