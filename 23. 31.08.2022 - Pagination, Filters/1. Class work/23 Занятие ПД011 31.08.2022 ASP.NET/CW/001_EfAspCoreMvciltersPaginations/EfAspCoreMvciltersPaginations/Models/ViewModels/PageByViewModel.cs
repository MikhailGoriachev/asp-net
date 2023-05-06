using EntityFrameworkInAspNetCoreMvcIntro.Models;

namespace EfAspCoreMvciltersPaginations.Models.ViewModels;

// Информация о пагинации и коллекция выводимых пользователей
// вывод в методе действия PageBy, поэтому и такое название модели
public class PageByViewModel
{
    // коллекция пользователей - что выводить
    public IEnumerable<User> Users { get; }

    // информация о пагинации - как выводить
    public PageViewModel PageViewModel { get; }


    public PageByViewModel(IEnumerable<User> users, PageViewModel viewModel) {
        Users = users;
        PageViewModel = viewModel;
    } // PageByViewModel
} // class PageByViewModel
