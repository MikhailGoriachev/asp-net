using EntityFrameworkInAspNetCoreMvcIntro.Models;

namespace EfAspCoreMvciltersPaginations.Models.ViewModels
{
    public class CompanyIndexViewModel
    {
        // коллекция компаний - что выводить
        public IEnumerable<Company> Companies { get; }

        // информация о пагинации - как выводить
        public PageViewModel PageViewModel { get; }


        public CompanyIndexViewModel(IEnumerable<Company> companies, PageViewModel viewModel) {
            Companies = companies;
            PageViewModel = viewModel;
        } // CompanyIndexViewModel
    }
}
