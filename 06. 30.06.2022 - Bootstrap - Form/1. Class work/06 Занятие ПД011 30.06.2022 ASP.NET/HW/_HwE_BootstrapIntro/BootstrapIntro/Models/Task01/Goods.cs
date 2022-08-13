namespace BootstrapIntro.Models.Task01
{
    // Товар для обработки по заданию - бытовая техника
    // тип техники, наименование, артикул, цена, количество, изображение
    public class Goods
    {

        // тип техники
        private string _type;
        public string Type {
            get => _type;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Недопустимое значение в поле типа техники");
                _type = value;
            } // set
        } // Type

        // наименование
        private string _name;
        public string Name {
            get => _name;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Недопустимое значение в поле наименования");
                _name = value;
            } // set
        } // Name


        // артикул
        private string _vendorCode;
        public string VendorCode {
            get => _vendorCode;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Недопустимое значение в поле артикула");
                _vendorCode = value;
            } // set
        } // VendorCode

        // цена
        private int _price;
        public int Price {
            get => _price;
            set {
                if (value < 0)
                    throw new InvalidDataException("Недопустимое значение цены книги");
                _price = value;
            } // set
        } // Price

        // количество
        private int _quantity;
        public int Quantity {
            get => _quantity;
            set {
                if (value < 0)
                    throw new InvalidDataException("Недопустимое значение цены книги");
                _quantity = value;
            } // set
        } // Price

        // изображение
        private string _image;
        public string Image {
            get => _image;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Недопустимое значение в поле имени файла изображения товара");
                _image = value;
            } // set
        } // Image


        #region Конструкторы
        public Goods() : this("Микроволновая печь", "DEXP MS-70", "4726172", 6379, 12, "mwoven.jpg")
        {

        } // Goods

        public Goods(string type, string name, string vendorCode, int price, int quantity, string image)
        {
            Type = type;
            Name = name;
            VendorCode = vendorCode;
            Price = price;
            Quantity = quantity;
            Image = image;
        } // Goods
        #endregion
    } // class Goods
}
