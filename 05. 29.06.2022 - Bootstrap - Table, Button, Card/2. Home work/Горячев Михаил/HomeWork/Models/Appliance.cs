using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Models
{
    // Класс Бытовая техника
    // наименование, артикул, цена, количество, изображение
    public class Appliance
    {
        // наименование
        private string? _name;
        public string? Name
        {
            get => _name;
            set => _name = value;
        }


        // тип техники
        private string? _type;
        public string? Type
        {
            get => _type;
            set => _type = value;
        }


        // артикул
        private string? _vendorCode;
        public string? VendorCode
        {
            get => _vendorCode;
            set => _vendorCode = value;
        }


        // цена
        private int? _price;
        public int? Price
        {
            get => _price;
            set => _price = value;
        }


        // количество
        private int? _amount;
        public int? Amount
        {
            get => _amount;
            set => _amount = value;
        }


        // изображение
        private string? _fileName;
        public string? FileName
        {
            get => _fileName;
            set => _fileName = value;
        }


        #region Конструкторы

        // конструктор по умолчанию
        public Appliance() { }

        // конструктор инициализирующий
        public Appliance(string name, string type, string vendorCode, int price, int amount, string fileName)
        {
            // установка значений
            _name = name;
            _type = type;
            _vendorCode = vendorCode;
            _price = price;
            _amount = amount;
            _fileName = fileName;
        }

        #endregion 

        #region Методы


        // вывод модели в разметку в виде плитки
        public string FullToHtml() => @$"
                <div class='tile'>
                    <div class='tile-normal'>
                        <img src='~/images/{_fileName}' />
                    </div>
                    <div>Наименование: <span>{_name}</span></div>
                    <div>Тип техники: <span>{_type}</span></div>
                    <div>Артикул: <span>{_vendorCode}</span></div>
                    <div>Цена: <span>{_price} &#8381;</span></div>
                    <div>Количество: <span>{_amount}</span></div>
                </div>";

        #endregion
    }
}
