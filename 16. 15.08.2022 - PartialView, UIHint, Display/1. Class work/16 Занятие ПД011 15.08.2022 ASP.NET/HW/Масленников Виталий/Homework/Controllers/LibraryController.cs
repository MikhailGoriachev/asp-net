using Homework.Models;
using Homework.Common;
using Homework.Models.Books;
using Homework.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers;

public class LibraryController : Controller
{
    private readonly IBooksRepository _booksRepository;
    private readonly IWebHostEnvironment _env;
    private readonly long _fileSizeLimit;
    private const string ImageFolder = "/images/books/";

    public LibraryController(IBooksRepository booksRepository,
                             IWebHostEnvironment env,
                             IConfiguration config)
    {
        _booksRepository = booksRepository;
        _env = env;
        _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
    }

    // GET Library/Index
    public IActionResult Index() => View( new LibraryIndexViewModel { Books = _booksRepository.Books } );

    // GET Library/AddBook
    public IActionResult AddBook() => 
        View("Index", new LibraryIndexViewModel { Books = _booksRepository.Books, InvokeForm = true} );

    // POST Library/AddBook
    [HttpPost]
    public async Task<IActionResult> AddBookAsync(Book book, IFormFile? file)
    { 
        try
        {
            if (file == null)
                throw new Exception("Файл обложки не выбран");
            
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    
            if (!ImageValidator.IsValidExtension(extension))
                throw new Exception($"Не поддерживаемое расширение файла {extension}");

            if (file.Length > _fileSizeLimit)
                throw new Exception($"Недопустим размер файла более 2Mb");

            if (!ImageValidator.IsValidSignature(file))
                throw new Exception("Некорректный файл изображения");

            var newFileName = $"{new DateTime().GetTimestamp()}{extension}";
                    
            var uploaded = Path.Combine($"{_env.WebRootPath}{ImageFolder}", newFileName);

            await using var fileStream = new FileStream(uploaded, FileMode.Create);
            await file.CopyToAsync(fileStream);

            book.Image = newFileName;
            
            _booksRepository.AddBook(book);
            _booksRepository.SaveData();
        }catch (Exception ex)
        {
            return View("Index", new LibraryIndexViewModel
            {
                Books = _booksRepository.Books, ErrMsg = ex.Message,
                Book = book, InvokeForm = true
            });
        }
        
        return View("Index", new LibraryIndexViewModel { Books = _booksRepository.Books, Book = null});
    }
    
    // Get Library/EditBook
    public IActionResult EditBook(int id)
    {
        return View("Index", new LibraryIndexViewModel
        {
            Books = _booksRepository.Books, InvokeForm = true, IsEdit = true, 
            Book = _booksRepository.Books.FirstOrDefault(b => b.Id == id)
        } );
    }
    
    // Post Library/EditBook
    [HttpPost]
    public IActionResult EditBook(Book book)
    {
        _booksRepository.UpdateBook(book);
        _booksRepository.SaveData();
        return View("Index", new LibraryIndexViewModel { Books = _booksRepository.Books } );
    }

    // GET Library/RemoveBook
    public IActionResult RemoveBook(int id)
    {
        _booksRepository.DeleteBook(id);
        _booksRepository.SaveData();
        return View("Index", new LibraryIndexViewModel { Books = _booksRepository.Books } );
    }
    
    // GET Library/OrderBy
    public IActionResult OrderBy(string property, string sort)
    {
        ViewBag.Title = $"Книги, упорядоченные по параметру \"{Lang.BookProperties[property]}\"";
        return View("Index", new LibraryIndexViewModel { Books = _booksRepository.Books.OrderBySort(property, sort)});
    }
    
    // GET Library/Select
    public IActionResult Select(string where)
    {
        ViewBag.Title = $"Книги, отобранные по условию \"{Lang.FilterRules[where]}\"";

        return View("Index", new LibraryIndexViewModel { Books =   where switch
        {
            "MaxAmount" =>  _booksRepository.Books.Where(b => b.Amount == _booksRepository.Books.Max(book => book.Amount)),
            "MinAmount" =>  _booksRepository.Books.Where(b => b.Amount == _booksRepository.Books.Min(book => book.Amount)),
            "Oldest" =>  _booksRepository.Books.Where(b => b.Year == _booksRepository.Books.Min(book => book.Year)),
            "Newest" =>  _booksRepository.Books.Where(b => b.Year == _booksRepository.Books.Max(book => book.Year)),
            "MostExpensive" =>  _booksRepository.Books.Where(b => b.Price == _booksRepository.Books.Max(book => book.Price)),
            "Cheapest" =>  _booksRepository.Books.Where(b => b.Price == _booksRepository.Books.Min(book => book.Price)),
            _ => throw new ArgumentOutOfRangeException(nameof(where), where, null)
        }});
    }
}