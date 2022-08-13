using Home_Work.Utilities;

namespace Home_Work.Models.Appliances
{
    public class Appliance
    {
        //Наименование прибора
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                _name = value;
            }
        }

        //Артикул
        private int _vendorCode;
        public int VendorCode
        {
            get { return _vendorCode; }
            set
            {
                _vendorCode = value;
            }
        }


        //Стоимость
        private int _price;

        public int Price
        {
            get { return _price; }
            set
            {
                _price = value;
            }
        }

        //Количество
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
            }
        }

        //Словарь с именами файлов (на данном этапе пришлось отказаться от использования.
        //Изображения присваивались некорректно)
        private Dictionary<string, string> _imgFiles = new (){
            ["стиральная машина"] = "washing-machine.jpg",
            ["микроволновка"] = "microwave.jpg",
            ["микроволновая печь"] = "microwave.jpg",
            ["холодильник"] = "fridge.jpg",
            ["пылесос"] = "vacuum_cleaner.jpg",
        };


        //Изображение
        private string _image;
        public string Image
        {
            get { return _image; }
            //Временный, неоптимизированный способ задния изображения
            set
            {
                /*_image = value;*/
                switch (_name.ToLower())
                {
                    case "стиральная машина":
                        _image = "washing-machine.jpg";
                        break;

                    case "микроволновка":
                        _image = "microwave.jpg";
                        break;

                    case "микроволновая печь":
                        _image = "microwave.jpg";
                        break;

                    case "холодильник":
                        _image = "fridge.jpg";
                        break;

                    case "пылесос":
                        _image = "vacuum_cleaner.jpg";
                        break;

                    default:
                        _image = "vacuum_cleaner.jpg";
                        break;
                }
            }
        }

        //Id 
        public int Id
        {
            get;
            private set;
        }


        public Appliance() : this("-", 0, 0, 0)
        {
        }

        public Appliance(string name, int vendorCode, int price, int quantity)
        {
            Name = name;
            VendorCode = vendorCode;
            Price = price;
            Quantity = quantity;
            /*_image = _imgFiles.ContainsKey(_name.ToLower()) ? _imgFiles[_name.ToLower()] : "vacuum_cleaner.jpg";*/
            Image = "";
            Id = Utils.LastApplianceId++;
        }
    }
}
