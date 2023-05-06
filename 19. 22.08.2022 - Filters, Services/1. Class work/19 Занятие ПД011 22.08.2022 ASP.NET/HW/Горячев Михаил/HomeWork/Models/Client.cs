using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HomeWork.Infrastructure;
using Newtonsoft.Json;

namespace HomeWork.Models;

// Класс Клиент
public class Client
{
    // текущий максимальный id
    private static int _currentMaxId = 0;

    public static int CurrentMaxId
    {
        get => _currentMaxId;
        set => _currentMaxId = value;
    }

    // id
    private int _id;

    [UIHint("HiddenInput")]
    public int Id
    {
        get => _id;
        set
        {
            _currentMaxId = value > _currentMaxId ? value : _currentMaxId;
            _id = value;
        }
    }


    // фамилию
    [Display(Name = "Фамилия:")]
    [Required(ErrorMessage = "Введите фамилию клиента!")]
    public string? Surname { get; set; }

    // имя
    [Display(Name = "Имя:")]
    [Required(ErrorMessage = "Введите имя клиента!")]
    public string? Name { get; set; }

    // отчество
    [Display(Name = "Отчество:")]
    [Required(ErrorMessage = "Введите отчество клиента!")]
    public string? Patronymic { get; set; }

    // возраст клиента в годах
    [Display(Name = "Возраст (лет):")]
    [Range(18, 65, ErrorMessage = "Возраст клиента должен быть в диапазоне от 18 до 65 лет")]
    [Required(ErrorMessage = "Введите возраст клиента!")]
    public int YearsOld { get; set; }

    // номер телефона
    [Display(Name = "Номер телефона")]
    [DataType(DataType.PhoneNumber)]
    [Required(ErrorMessage = "Введите номер телефона клиента!")]
    public string? TelNumber { get; set; }

    // адрес электронной почты
    [Required(ErrorMessage = "Введите адрес электронной почты клиента!")]
    [EmailAddress(ErrorMessage = "Неверный формат электронной почты!")]
    public string? Mail { get; set; }

    // пароль
    [JsonProperty] private string? _password;

    [Required(ErrorMessage = "Введите пароль клиента!")]
    [StringLength(28, MinimumLength = 8, ErrorMessage = "Длина пароля должна быть от 8 до 28 символов!")]
    [UIHint("Password")]
    [Newtonsoft.Json.JsonIgnore]
    public string? Password
    {
        get => Utils.Crypt(_password!);
        set => _password = Utils.Crypt(value!);
    }

    // признак постоянного клиента
    [Required(ErrorMessage = "Укажите значение признака постоянного клиента!")]
    public bool IsActive { get; set; }

    // фотографию пользователя
    public string? ImageFile { get; set; }

    #region Конструкторы

    // конструктор по умолчанию
    public Client()
    {
    }


    // конструктор инициализирующий
    public Client(string? surname, string? name, string? patronymic, int yearsOld, string? telNumber, string? mail,
        string? password, bool isActive, string? imageFile)
    {
        Id = ++_currentMaxId;
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        YearsOld = yearsOld;
        TelNumber = telNumber;
        Password = password;
        Mail = mail;
        IsActive = isActive;
        ImageFile = imageFile;
    }

    // конструктор инициализирующий
    public Client(string? surname, string? name, string? patronymic, int yearsOld, string? telNumber, string? mail,
        bool isActive, string? imageFile)
    {
        Id = ++_currentMaxId;
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        YearsOld = yearsOld;
        TelNumber = telNumber;
        Mail = mail;
        IsActive = isActive;
        ImageFile = imageFile;
    }

    // конструктор инициализирующий
    public Client(int id, string? surname, string? name, string? patronymic, int yearsOld, string? telNumber,
        string? mail, bool isActive, string? imageFile)
    {
        _id = id;
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        YearsOld = yearsOld;
        TelNumber = telNumber;
        Mail = mail;
        IsActive = isActive;
        ImageFile = imageFile;
    }

    #endregion
}
