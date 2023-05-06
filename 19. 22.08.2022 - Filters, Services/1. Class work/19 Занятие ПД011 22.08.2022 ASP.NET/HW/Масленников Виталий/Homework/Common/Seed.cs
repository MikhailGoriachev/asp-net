using Homework.Models.TravelCompany;

namespace Homework.Common;

public static class Seed
{
    // Данные об инструкторах
    public static List<Instructor> Instructors() => new List<Instructor>
    {
        new () { Id =  1, Surname = "Юрковский",   Name = "Марк",      Patronymic = "Максимилианович",
            DoB = new DateTime(1965, 5, 12), Category = "A"}, 
        new () { Id =  2, Surname = "Якубовская",  Name = "Диана",     Patronymic = "Павловна",
            DoB = new DateTime(1965, 5, 12),   Category = "B"}, 
        new () { Id =  3, Surname = "Шапиро",      Name = "Федор",     Patronymic = "Федорович",
            DoB = new DateTime(1974, 4, 1),  Category = "C"}, 
        new () { Id =  4, Surname = "Вожжаев",     Name = "Сергей",    Patronymic = "Денисович",
            DoB = new DateTime(1984, 8, 20), Category = "A"}, 
        new () { Id =  5, Surname = "Хроменко",    Name = "Игорь",     Patronymic = "Владимирович",
            DoB = new DateTime(1979, 7, 17), Category = "B"}, 
    };

    // Данные о маршрутах
    public static List<Models.TravelCompany.Route> Routes() => new List<Models.TravelCompany.Route>()
    {
        new()
        {
            Id = 1, Length = Random.Shared.Next(2, 21), Difficulty = "A-",
            StartPoint = "Начальный пункт 1", MiddlePoint = "Промежуточный пункт 1",
            FinishPoint = "Конечный пункт 1", InstructorId = 1
        },
        new()
        {
            Id = 2, Length = Random.Shared.Next(2, 21), Difficulty = "B",
            StartPoint = "Начальный пункт 2", MiddlePoint = "Промежуточный пункт 2",
            FinishPoint = "Конечный пункт 2", InstructorId = 2
        },
        new()
        {
            Id = 3, Length = Random.Shared.Next(2, 21), Difficulty = "A+",
            StartPoint = "Начальный пункт 3", MiddlePoint = "Промежуточный пункт 3",
            FinishPoint = "Конечный пункт 3", InstructorId = 1
        },
        new()
        {
            Id = 4, Length = Random.Shared.Next(2, 21), Difficulty = "C+",
            StartPoint = "Начальный пункт 4", MiddlePoint = "Промежуточный пункт 4",
            FinishPoint = "Конечный пункт 4", InstructorId = 3
        },
        new()
        {
            Id = 5, Length = Random.Shared.Next(2, 21), Difficulty = "B+",
            StartPoint = "Начальный пункт 5", MiddlePoint = "Промежуточный пункт 5",
            FinishPoint = "Конечный пункт 5", InstructorId = 2
        },
        new()
        {
            Id = 6, Length = Random.Shared.Next(2, 21), Difficulty = "A+",
            StartPoint = "Начальный пункт 6", MiddlePoint = "Промежуточный пункт 6",
            FinishPoint = "Конечный пункт 6", InstructorId = 4
        },
        new()
        {
            Id = 7, Length = Random.Shared.Next(2, 21), Difficulty = "B-",
            StartPoint = "Начальный пункт 7", MiddlePoint = "Промежуточный пункт 7",
            FinishPoint = "Конечный пункт 7", InstructorId = 5
        },
        new()
        {
            Id = 8, Length = Random.Shared.Next(2, 21), Difficulty = "C-",
            StartPoint = "Начальный пункт 8", MiddlePoint = "Промежуточный пункт 8",
            FinishPoint = "Конечный пункт 8", InstructorId = 3
        },
        new()
        {
            Id = 9, Length = Random.Shared.Next(2, 21), Difficulty = "A",
            StartPoint = "Начальный пункт 9", MiddlePoint = "Промежуточный пункт 9",
            FinishPoint = "Конечный пункт 9", InstructorId = 1
        },
        new()
        {
            Id = 10, Length = Random.Shared.Next(2, 21), Difficulty = "A-",
            StartPoint = "Начальный пункт 10", MiddlePoint = "Промежуточный пункт 10",
            FinishPoint = "Конечный пункт 10", InstructorId = 1
        },
        new()
        {
            Id = 11, Length = Random.Shared.Next(2, 21), Difficulty = "B-",
            StartPoint = "Начальный пункт 11", MiddlePoint = "Промежуточный пункт 11",
            FinishPoint = "Конечный пункт 11", InstructorId = 2
        },
        new()
        {
            Id = 12, Length = Random.Shared.Next(2, 21), Difficulty = "C",
            StartPoint = "Начальный пункт 12", MiddlePoint = "Промежуточный пункт 12",
            FinishPoint = "Конечный пункт 12", InstructorId = 3
        },
    };


     public static List<Client> Clients() => new List<Client>()
    {
        new() { Id = 1,  Age = 42, Email = "email1@gmail.com",  Password = "^OY^zKYY]EXN", Photo = "1.jpg",  PhoneNumber = "+79491234511", IsRegular = true,  Surname = "Фадеев",     Name =  "Харитон",   Patronymic = "Агафонович"},
        new() { Id = 2,  Age = 54, Email = "email2@gmail.com",  Password = "^OY^zKYY]EXN", Photo = "2.jpg",  PhoneNumber = "+79491234512", IsRegular = false, Surname = "Кулаков",    Name =  "Варлаам",   Patronymic = "Георгьевич"},
        new() { Id = 3,  Age = 43, Email = "email3@gmail.com",  Password = "^OY^zKYY]EXN", Photo = "3.jpg",  PhoneNumber = "+79491234513", IsRegular = true,  Surname = "Якушев",     Name =  "Матвей",    Patronymic = "Аркадьевич"},
        new() { Id = 4,  Age = 21, Email = "email4@gmail.com",  Password = "^OY^zKYY]EXN", Photo = "4.jpg",  PhoneNumber = "+79491234514", IsRegular = false, Surname = "Мамонтов",   Name =  "Алексей",   Patronymic = "Геннадьевич"},
        new() { Id = 5,  Age = 24, Email = "email5@gmail.com",  Password = "^OY^zKYY]EXN", Photo = "5.jpg",  PhoneNumber = "+79491234515", IsRegular = true,  Surname = "Волков",     Name =  "Вадим",     Patronymic = "Якунович"},
        new() { Id = 6,  Age = 49, Email = "email6@gmail.com",  Password = "^OY^zKYY]EXN", Photo = "6.jpg",  PhoneNumber = "+79491234516", IsRegular = false, Surname = "Архипов",    Name =  "Севастьян", Patronymic = "Эдуардович"},
        new() { Id = 7,  Age = 35, Email = "email7@gmail.com",  Password = "^OY^zKYY]EXN", Photo = "7.jpg",  PhoneNumber = "+79491234517", IsRegular = true,  Surname = "Лаврентьев", Name =  "Бенедикт",  Patronymic = "Игоревич"},
        new() { Id = 8,  Age = 41, Email = "email8@gmail.com",  Password = "^OY^zKYY]EXN", Photo = "8.jpg",  PhoneNumber = "+79491234518", IsRegular = false, Surname = "Калашников", Name =  "Леонард",   Patronymic = "Ростиславович"},
        new() { Id = 9,  Age = 47, Email = "email9@gmail.com",  Password = "^OY^zKYY]EXN", Photo = "9.jpg",  PhoneNumber = "+79491234519", IsRegular = true,  Surname = "Горбачёва", Name = "Вилора", Patronymic = "Михайловна"},
        new() { Id = 10, Age = 22, Email = "email10@gmail.com", Password = "^OY^zKYY]EXN", Photo = "10.jpg", PhoneNumber = "+79491234520", IsRegular = false, Surname = "Белозёрова", Name = "Снежана", Patronymic = "Вадимовна"},
        new() { Id = 11, Age = 27, Email = "email11@gmail.com", Password = "^OY^zKYY]EXN", Photo = "11.jpg", PhoneNumber = "+79491234521", IsRegular = true,  Surname = "Журавлёва", Name = "Гражина", Patronymic = "Наумовна"},
        new() { Id = 12, Age = 36, Email = "email12@gmail.com", Password = "^OY^zKYY]EXN", Photo = "12.jpg", PhoneNumber = "+79491234522", IsRegular = false, Surname = "Павлова", Name = "Милда", Patronymic = "Валерьевна"},
        new() { Id = 13, Age = 20, Email = "email13@gmail.com", Password = "^OY^zKYY]EXN", Photo = "13.jpg", PhoneNumber = "+79491234523", IsRegular = true,  Surname = "Орехова", Name = "Эрика", Patronymic = "Агафоновна"},
        new() { Id = 14, Age = 34, Email = "email14@gmail.com", Password = "^OY^zKYY]EXN", Photo = "14.jpg", PhoneNumber = "+79491234524", IsRegular = false, Surname = "Данилова", Name = "Северина", Patronymic = "Львовна"},
        new() { Id = 15, Age = 50, Email = "email15@gmail.com", Password = "^OY^zKYY]EXN", Photo = "15.jpg", PhoneNumber = "+79491234525", IsRegular = true,  Surname = "Брагина", Name = "Анжелика", Patronymic = "Кирилловна"},
    };
    
    
    // Список категорий инструкторов
    public static readonly List<string> Categories =
        new () { "A", "B", "C" };
    
    // Список категорий сложности туристических маршрутов
    public static readonly List<string> Difficulties = 
        new () { "A-", "A", "A+", "B-", "B", "B+", "C-", "C", "C+" };
}