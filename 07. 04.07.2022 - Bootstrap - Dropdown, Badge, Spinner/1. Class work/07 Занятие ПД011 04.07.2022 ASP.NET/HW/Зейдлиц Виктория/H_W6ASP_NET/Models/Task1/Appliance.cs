namespace H_W6ASP_NET.Models.Task1
{
    /// <summary>
    /// Данные о бытовой технике на оптовом складе (
    ///  *наименование,
    ///  *тип техники,
    ///  *артикул,
    ///  *цена, 
    ///  *количество, 
    ///  *изображение
    /// </summary>
    public class Appliance
    {
        // наименование
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }


        // тип техники
        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


        // артикул
        private string _vendorCode;
        public string VendorCode
        {
            get { return _vendorCode; }
            set { _vendorCode = value; }
        }


        // цена
        private int _price;
        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        // количество
        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }


        // изображение
        private string _image;
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }


        #region Конструкторы

        // конструктор по умолчанию
        public Appliance() : this("", "", "", 1, 1, "") { }

        // конструктор инициализирующий
        public Appliance(string title, string type, string vendorCode, int price, int amount, string fileName)
        {
            // установка значений
            _vendorCode = vendorCode;
            _title = title;
            _type = type;
            _price = price;
            _amount = amount;
            _image = fileName;
        }

        #endregion 

    } // class Appliance
}
