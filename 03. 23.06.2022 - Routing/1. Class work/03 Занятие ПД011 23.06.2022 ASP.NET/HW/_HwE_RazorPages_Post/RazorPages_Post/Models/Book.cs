namespace RazorPages_Post.Models
{
    // данные о книге
    public class Book
    {
        // автор
        private string _author;
        public string Author { 
            get => _author; 
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Недопустимое значение в поле автора");
                _author = value;
            } // set
        } // Author

        // название
        private string _title;
        public string Title {
            get => _title;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Недопустимое значение в поле названия");
                _title = value;
            } // set
        } // Title

        // год издания
        private int _year;
        public int Year {
            get => _year;
            set {
                if (value < 0 || value > DateTime.Now.Year)
                    throw new InvalidDataException("Недопустимое значение года издания");
                _year = value;
            } // set
        } // Year

        // цена книги
        private int _price;
        public int Price
        {
            get => _price;
            set {
                if (value < 0)
                    throw new InvalidDataException("Недопустимое значение цены книги");
                _price = value;
            } // set
        } // Price

        // изображение обложки
        private string _cover;
        public string Cover {
            get => _cover;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Недопустимое значение в поле имени файла обложки");
                _cover = value;
            } // set
        } // Cover

        public Book():this("Бриггс Дж.", "Python для детей. Самоучитель по программированию", 2017, 890, "cover1.jpg") { }
        public Book(string author, string title, int year, int price, string cover) {
            Author = author;
            Title = title;
            Year = year;
            Price = price;
            Cover = cover;
        } // Book
    } // class Book
}
