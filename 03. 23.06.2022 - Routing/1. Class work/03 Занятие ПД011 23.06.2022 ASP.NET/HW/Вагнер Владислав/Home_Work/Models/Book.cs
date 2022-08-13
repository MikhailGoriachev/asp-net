using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home_Work.Models
{
    public class Book
    {
        //Название книги
        public string BookName { get; set; }

        //Автор книги
        public string Author { get; set; }

        //Год издания
        public int PubYear { get; set; }

        public Book(string name, string autor, int year)
        {
            BookName = name;
            Author = autor;
            PubYear = year;
        }

        public Book():this("Python для детей. Самоучитель по программированию", "Бриггс Дж.", 2017)
        {

        }
    }
}
