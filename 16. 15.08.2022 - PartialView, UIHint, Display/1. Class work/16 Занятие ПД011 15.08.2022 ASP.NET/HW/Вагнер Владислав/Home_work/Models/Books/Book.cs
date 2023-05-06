namespace Home_work.Models
{
    public class Book
    {
        public int Id { get; set; }

        //Обложка
        public string Image { get; set; }

        //Автор
        public string Author { get; set; }

        //Название
        public string Title { get; set; }

        //Год публикации
        public int Year { get; set; }

        //Количество
        public int Quantity { get; set; }

        //Стоимость
        public int Price { get; set; }

        public Book(int id, string title, string author, string image, int year, int amount, int price)
        {
            Id = id;
            Title = title;
            Author = author;
            Image = image;
            Year = year;
            Quantity = amount;
            Price = price;
        }
        public Book():this(0,"По умолчанию","По умолчанию","Default",0,0,0)
        {

        }
    }
}
