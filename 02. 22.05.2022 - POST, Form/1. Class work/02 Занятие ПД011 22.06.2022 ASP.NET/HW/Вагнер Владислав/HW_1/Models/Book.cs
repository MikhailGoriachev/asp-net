using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_1.Models
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
    }
}
