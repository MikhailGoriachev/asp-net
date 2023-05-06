namespace HomeWork.Models.ViewModels;


// Класс для реализации страницы с пагинацией
public class PaginationPageViewModel<T>
{
    // коллеция элементов
    public IEnumerable<T> Items { get; set; }

    // модель страницы для пагинации
    public PageViewModel PageViewModel { get; set; }


    // конструктор инициализирующий
    public PaginationPageViewModel(IEnumerable<T> items, PageViewModel pageViewModel)
    {
        Items = items;
        PageViewModel = pageViewModel;
    }
}
