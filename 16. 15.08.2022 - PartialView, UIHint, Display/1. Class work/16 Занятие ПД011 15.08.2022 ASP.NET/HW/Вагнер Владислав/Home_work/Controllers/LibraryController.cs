using Microsoft.AspNetCore.Mvc;
using Home_work.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Home_work.Controllers;

public class LibraryController : Controller
{
    //Библиотека
    private Library _library;

    public LibraryController()
    {
        _library = new();
    }

    public IActionResult Books()
    {
        ViewBag.IsDefault = true;
        return View(_library.Books);
    }

    #region CRUD

    //Добавление
    public IActionResult AddBook()
    {
        ViewBag.AddingFormMode = true;
        /*ViewBag.ImagesNames = new SelectList(_library.Books, "Image", "Image");*/

        //Добавляемвсе имена файлов в папке с обложками
        ViewBag.ImagesNames = new SelectList(Directory.GetFiles(@"wwwroot/images/covers")
                                                      .Select(f => f.Substring(f.IndexOf('\\')+1))
                                                      .OrderByDescending(f => f));


        //Корректный Id, если коллекция пуста
        int maxId = _library.Books.Count > 0 ? _library.Books.Max(f => f.Id) : 0;

        //Создание объекиа
        return View("Forms", new Book(maxId + 1, "-","-","-",0,0,0));
    }

    //Редактирование
    public IActionResult EditBook(int id)
    {
        Book? book = _library.Books.FirstOrDefault(b => b.Id == id);

        //Если элемент не найден, то форму не запускаем
        if (book is null)
            return View("Books", _library.Books);

        ViewBag.AddingFormMode = false;
        /*ViewBag.ImagesNames = new SelectList(_library.Books, "Image", "Image", book.Image);*/

        //Выпадающий список имён файлов
        ViewBag.ImagesNames = new SelectList(Directory.GetFiles(@"wwwroot/images/covers")
                                                      .Select(f => f.Substring(f.IndexOf('\\')+1))
                                                      .OrderBy(f => f), book.Image);

        //Форма редактирования объекта
        return View("Forms", book);
    }

    //Post-обработчик формы
    [HttpPost]
    public IActionResult FormSubmit(Book model, bool addingmode)
    {

        if (addingmode)
            _library.AddBook(model ?? new Book());
        else 
            _library.EditBook(model);

        //Если добавление - сортируем коллекцию по Id
        return View("Books", addingmode?_library.Books.OrderByDescending(b => b.Id): _library.Books);
    }

    //Удаление
    public IActionResult DeleteBook(int id)
    {
        if (_library.Books.Count <= 0)
            return View("Books", _library.Books);

        _library.DeleteBook(id);

        return View("Books",_library.Books);
    }
    #endregion

    #region Сортировки

    //сортировка по фамилиям авторов
    public IActionResult OrderByAuthor()
    {

        return View("Books", _library.SortByAuthor());
    }

    //сортировка упорядоченных по годам издания
    public IActionResult OrderByPubYear()
    {

        return View("Books", _library.SortByYear());
    }
    //сортировка упорядоченных по убыванию цены
    public IActionResult OrderByPriceDesc()
    {

        return View("Books", _library.SortByPrice());
    }

    #endregion

    #region Выборки

    //Выборка книг с максимальным количеством экземпляров
    public IActionResult SelectByMaxQuantity()
    {
        ViewBag.ShowControls = 0;
        return View("Books", _library.SelectByMaxQuantity());
    }

    //Выборка книг с минимальным количеством экземпляров
    public IActionResult SelectByMinQuantity()
    {
        ViewBag.ShowControls = 0;

        return View("Books", _library.SelectByMinQuantity());
    }

    //Самые старые книги
    public IActionResult SelectOldest()
    {
        ViewBag.ShowControls = 0;

        return View("Books", _library.SelectOldest());
    }

    //Самые новые книги
    public IActionResult SelectNewest()
    {
        ViewBag.ShowControls = 0;

        return View("Books", _library.SelectNewest());
    }

    //Самые дорогие книги
    public IActionResult SelectMostExpensive()
    {
        ViewBag.ShowControls = 0;

        return View("Books", _library.SelectMostExpensive());
    }

    //Самые дешевые книги
    public IActionResult SelectCheapest()
    {
        ViewBag.ShowControls = 0;

        return View("Books", _library.SelectCheapest());
    }

    #endregion

    public IActionResult DeleteFile()
    {
        _library.DeleteFile();

        return View("Books",_library.Books);
    }
}
