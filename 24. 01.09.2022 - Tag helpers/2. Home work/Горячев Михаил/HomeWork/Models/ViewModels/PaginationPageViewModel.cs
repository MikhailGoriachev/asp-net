namespace HomeWork.Models.ViewModels;


// Интерфейс для реализации страницы с пагинацией
public class PaginationPageViewModel<T>
{
    // коллеция элементов
    public List<T> Items { get; set; }

    // модель страницы для пагинации
    public PageViewModel PageViewModel { get; set; }


    // конструктор инициализирующий
    public PaginationPageViewModel(List<T> items, PageViewModel pageViewModel)
    {
        Items = items;
        PageViewModel = pageViewModel;
    }
}
