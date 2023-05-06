using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Models
{
    // Класс Книга
    public class Book
    {
        // название книги
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }


        // автор
        private string _author;
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }


        // год издания
        private int _year;
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }


        // файл изображения
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }


        #region Конструкторы

        // конструктор по умолчанию
        public Book() : this("", "", 1, "") { }

        // конструктор инициализирующий
        public Book(string author, string title, int year, string fileName)
        {
            // установка значений
            _author = author;
            _title = title;
            _year = year;
            _fileName = fileName;
        }

        #endregion 

        #region Методы

        #endregion
    }
}
