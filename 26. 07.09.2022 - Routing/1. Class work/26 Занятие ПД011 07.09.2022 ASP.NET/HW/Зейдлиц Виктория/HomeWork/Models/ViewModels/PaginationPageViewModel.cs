namespace HomeWork.Models.ViewModels;

public class PaginationPageViewModel<V>
{
    // коллеция элементов
    public IEnumerable<V> Items { get; set; }

    // модель страницы для пагинации
    public PageViewModel PageViewModel { get; set; }


    // конструктор инициализирующий
    public PaginationPageViewModel(IEnumerable<V> items, PageViewModel pageViewModel)
    {
        Items = items;
        PageViewModel = pageViewModel;
    }

} // class PaginationPageViewModel 
