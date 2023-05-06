namespace EfAspCoreMvciltersPaginations.Models.ViewModels;

// Этот класс хранит всю информацию о пагинации:
//     номер текущей страницы в свойстве PageNumber,
//     общее количество страниц в свойстве TotalPages,
//     свойства HasPreviousPage и HasNextPage, с помощью которых можно
//     узнать, есть ли до и после текущей страницы еще какие-нибудь страницы
public class PageViewModel
{
    public int PageNumber { get; }
    public int TotalPages { get; }

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;


    public PageViewModel(int count, int pageNumber, int pageSize) {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    } // PageViewModel
} // class PageViewModel

