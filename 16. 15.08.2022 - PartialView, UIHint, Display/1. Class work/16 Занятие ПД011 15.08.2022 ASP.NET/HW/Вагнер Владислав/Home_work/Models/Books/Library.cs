using Home_work.Utilities;
using Newtonsoft.Json;

namespace Home_work.Models
{
    public class Library
    {
        private List<Book> _books;
        public List<Book> Books
        {
            get => _books;
            set => _books = value ?? new();
        }


        //Путь к JSON-файлу
        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _filePath = $"{Environment.CurrentDirectory}\\App_Data\\books.json";
                }
                _filePath = value;
            }
        }

        #region Конструкторы
        public Library(string FilePath)
        {
            this.FilePath = FilePath;
            //Если файл есть - десериализация, в противном случае генерация
            if (File.Exists(_filePath))
            {
                Desiralize();
            }
            else
            {
                _books = new(Utils.GetBooks());
                Serialize();
            }
        }//ctor

        public Library() : this($"{Environment.CurrentDirectory}\\App_Data\\books.json")
        {

        }
        #endregion

        #region JSON
        //Сериализация 
        public void Serialize()
        {
            string json = JsonConvert.SerializeObject(_books,Formatting.Indented);

            //Получение имени папки
            string directory = _filePath.Replace(_filePath.Substring(_filePath.LastIndexOf('\\')), "");

            //Есил каталога нет, то создать 
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(_filePath, json);
        }

        //Десериализация
        public void Desiralize()
        {
            string json = File.ReadAllText(_filePath);
            Books = JsonConvert.DeserializeObject<List<Book>>(json);
        }
        #endregion

        #region CRUD

        //Добавить книгу
        public void AddBook(Book book)
        {
            /*_books.Insert(0, book);*/
            _books.Add(book);
            Serialize();
        }

        //Удалить книгу
        public void DeleteBook(int id)
        {
            if (_books.Find(b => b.Id == id) != null)
                _books.Remove(_books?.First(b => b.Id == id));

            Serialize();
        }

        //Редактировать книгу
        public  void EditBook(Book book)
        {
            if (book == null)
                return;

            Book found = _books.First(b => b.Id == book.Id);

            //Изменение коллекции
            if (found != null)
                _books[_books.IndexOf(found)] = book;


            Serialize();
        }

        //Удалить файл
        public void DeleteFile()
        {
            if(File.Exists(_filePath))
                File.Delete(_filePath);
        }

        #endregion

        #region Сортировки

        //сортировка по фамилиям авторов (полуаем именно фамилию)
        public IEnumerable<Book> SortByAuthor()
        {

           return _books.OrderBy((b) => {

                int spaceIndex = b.Author.IndexOf(" ");

               //Если имя и фамилия заданы в корректном формате, тогда получаем только фамилию из строки
                return spaceIndex > 0 ? b.Author.Substring(spaceIndex) : b.Author;
               });
        }

        //сортировка упорядоченных по годам издания
        public IEnumerable<Book> SortByYear()
            => _books.OrderBy(b => b.Year);


        //сортировка упорядоченных по убыванию цены
        public IEnumerable<Book> SortByPrice()
            => _books.OrderByDescending(b => b.Price);

        #endregion

        #region Выборки

        //Выборка книг с максимальным количеством экземпляров
        public IEnumerable<Book> SelectByMaxQuantity()
        {
            if (_books.Count <= 0)
                return _books;

            int max = _books.Max(b => b.Quantity);
            return _books.Where(b => b.Quantity == max);
        }
        
        //Выборка книг с минимальным количеством экземпляров
        public IEnumerable<Book> SelectByMinQuantity()
        {
            if (_books.Count <= 0)
                return _books;

            int min = _books.Min(b => b.Quantity);
            return _books.Where(b => b.Quantity == min);
        }


        //Самые старые книги
        public IEnumerable<Book> SelectOldest()
        {
            if (_books.Count <= 0)
                return _books;

            int oldest = _books.Min(b => b.Year);
            return _books.Where(b => b.Year == oldest);
        }

        //Самые новые книги
        public IEnumerable<Book> SelectNewest()
        {
            if (_books.Count <= 0)
                return _books;

            int newest = _books.Max(b => b.Year);
            return _books.Where(b => b.Year == newest);

        } 

        //Самые дорогие книги
        public IEnumerable<Book> SelectMostExpensive()
        {
            if (_books.Count <= 0)
                return _books;

            int highest = _books.Max(b => b.Price);
            return _books.Where(b => b.Price == highest);

        } 

        //Самые дешевые книги
        public IEnumerable<Book> SelectCheapest()
        {
            if (_books.Count <= 0)
                return _books;

            int lowest = _books.Min(b => b.Price);
            return _books.Where(b => b.Price == lowest);

        } 
        #endregion


    }
}
