namespace UsingTagHelpers2.Models.ViewModels;

// Класс Модель представления страницы для реализации пагинации
public class PageViewModel
{
    // текущий номер страницы
    public int PageNumber { get; set; }

    // количество страниц
    public int CountPages { get; set; }

    // есть ли предыдущая страница
    public bool HasPrevPage => PageNumber > 1;

    // есть ли следующая страница
    public bool HasNextPage => PageNumber < CountPages;


    // конструктор по умолчанию
    public PageViewModel()
    {
    }

    // конструктор инициализирующий
    public PageViewModel(int countItems, int pageNumber, int pageSize)
    {
        // установка номера страницы
        PageNumber = pageNumber;

        // подсчёт количества страниц
        CountPages = (int)Math.Ceiling(countItems / (double)pageSize);
    }

} // class PageViewModel
