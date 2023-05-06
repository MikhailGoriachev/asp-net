using BooksInLibrary.Models;

namespace BooksInLibrary.Infrastructure;

// вспомогательные методы для приложения
public static class Utils
{
    public static List<Book> InitializeBooks() => new List<Book>(new[] {
        new Book( 1, "Бейтс К.", "Как программировать на Java", 2012, 5, 890, "cover001.jpg"),
        new Book( 2, "Васильев А.Н.", "Как программировать на JavaScript", 2019, 2, 780, "cover002.jpg"),
        new Book( 3, "Радченко М.С.", "1С:Предприятие 8.3. Разработка приложений", 2013, 5, 450, "cover003.jpg"),
        new Book( 4, "Гальярди В.", "Раздельный Django", 2022, 6, 630, "cover004.jpg"),
        new Book( 5, "Троелсен Э.", "Язык C# 9. Платформа .NET 5", 2021, 8, 1200, "cover005.jpg"),
        new Book( 6, "Боон К.", "Pascal для всех", 2003, 2, 250, "cover006.jpg"),
        new Book( 7, "Фаронов В.В.", "Delphi 3", 2003, 8, 250, "cover007.jpg"),
        new Book( 8, "Кузнецов В.М.", "PHP 7. Самоучитель", 2019, 1, 900, "cover008.jpg"),
        new Book( 9, "Постолит А.", "Основы искусственного интеллекта в примерах на Python", 2021, 5, 690, "cover009.jpg"),
        new Book(10, "МакФарланд Д.", "JavaScript и jQuery", 2022, 9, 980, "cover010.jpg"),
        new Book(11, "Блинов Н.Н.", "Java. Методы программирования", 2013, 6, 450, "cover011.jpg"),
    });
} // class Utils

