namespace Home_Work.Models
{
    public class Appliance
    {

        private string _name;
        public string Name {
            get{ 
                return _name;
            } 

            set{
                if (string.IsNullOrWhiteSpace(value))
                    return;
                _name = value; 
            }
        }

        private int _article;
        public int Article { 
            get { return _article; } 
            set { 
                _article = value;
            } 
        }
        private int _price;

        //Стоимость
        public int Price { 
            get { return _price; } 
            set {
                _price = value;
            } 
        }

        //Количество
        private int _quantity;

        public int Quantity { 
            get { return _quantity; } 
            set {
                _quantity = value;
            } 
        }

        //Изображение
        private string _image;

        public string Image { 
            get { return _image; } 
            set {
               /* _image = value;*/
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

                    /*case "пылесос":
                        _image = "vacuum_cleaner.jpg";
                        break;*/

                    default:
                        _image = "vacuum_cleaner.jpg";
                        break;
                }
            } 
        }

        public Appliance():this("-",0,0,0)
        {
        }

        public Appliance(string name,int article, int price,int quantity)
        {
            Name = name;
            Article = article;
            Price = price;
            Quantity = quantity;
            Image = "";
        }
    }
}
