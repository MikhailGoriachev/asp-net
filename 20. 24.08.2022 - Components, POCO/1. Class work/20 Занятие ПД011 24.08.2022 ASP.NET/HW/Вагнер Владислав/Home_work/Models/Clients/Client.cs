using System.ComponentModel.DataAnnotations;
using Home_work.Infrastructure;
using Newtonsoft.Json;

namespace Home_work.Models.Clients;

public class Client
    {

        public int Id { get; set; } //Id

        public string? PhotoFile { get; set; }

        [Required(ErrorMessage = "Поле обязатено к заполнению!")]
        [Display(Name = "Фамилия клиента")]
        public string Surname { get; set; } //Фамилия 

        [Required(ErrorMessage = "Поле обязатено к заполнению!")]
        [Display(Name = "Имя клиента")]
        public string Name { get; set; } //Имя


        [Required(ErrorMessage = "Поле обязатено к заполнению!")]
        [RegularExpression(@"^[А-ЯA-ZЁ][a-zа-яё ]+", ErrorMessage = "Возмоны только буквы и начало слова с заглавной буквы")]
        [Display(Name = "Отчество клиента")]
        public string Patronymic { get; set; } //Отчество


        [UIHint("Number")]
        [Range(18, 65, ErrorMessage = "Возраст должен быть в диапазоне от 18 до 65 лет")]
        [Required(ErrorMessage = "Поле обязатено к заполнению!")]
        [Display(Name = "Возраст клиента")]
        public int Age { get; set; } //Возраст


        [Required(ErrorMessage = "Поле обязатено к заполнению!")]
        [Phone(ErrorMessage = "Телефон введён некорректно")]    
        [Display(Name = "Номер телефона клиента")]
        public string PhoneNumber { get; set; } //Номер телефон

        [UIHint("email")]
        [Required(ErrorMessage = "Поле обязатено к заполнению!")]
        [EmailAddress(ErrorMessage = "E-mail введён некорректно")]
        [Display(Name = "Электронный адрес клиента")]
        public string Email { get; set; } //электронная

        private string _password;


        [UIHint("Password")]
        [Required(ErrorMessage = "Поле обязатено к заполнению!")]
        [StringLength(28,MinimumLength = 8, ErrorMessage = "Длина пароля длжна быть от 8 до 28 символов")]
        [Display(Name = "Пароль")]
        public string Password {

            get => _password;
            set => _password = value;

        } //Пароль

        [Display(Name = "Постоянный клиент")]
        public bool IsConstant { get; set; } //Приpнак постоянного клиента

        #region Конструкторы

            public Client(int id, string surname, string name, string patronymic, int age, string phoneNumber, string email, string password,string fileName, bool isConstant)
            {
                Id = id;
                Surname = surname;
                Name = name;
                Patronymic = patronymic;
                Age = age;
                PhoneNumber = phoneNumber;
                Email = email;
                Password = password;
                PhotoFile = fileName;
                IsConstant = isConstant;
                EncodePassword();
            }

            public Client(ClientBindingModel bindingModel)
            {
                Id = bindingModel.Id;
                Surname = bindingModel.Surname;
                Name = bindingModel.Name;
                Patronymic = bindingModel.Patronymic;
                Age = bindingModel.Age;
                PhoneNumber = bindingModel.PhoneNumber;
                Email = bindingModel.Email;
                Password = bindingModel.Password;
                PhotoFile = bindingModel.PhotoFile;
                IsConstant = bindingModel.IsConstant;
                EncodePassword();
            }

            public Client() : this(1, "", "", "", 21, "+38 071 000 0000", "", "","", false)
            {

            }
    #endregion

        //Кодирование/Декодирование пароля
        public void EncodePassword() => _password = Utils.EncodeString(_password);

}
