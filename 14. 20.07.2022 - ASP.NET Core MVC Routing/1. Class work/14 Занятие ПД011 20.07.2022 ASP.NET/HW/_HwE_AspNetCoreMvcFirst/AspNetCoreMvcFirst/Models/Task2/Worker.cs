namespace AspNetCoreMvcFirst.Models.Task2;

// Класс для задачи 2
//     • фамилия и инициалы работника
//     • дата поступления на работу
//     • имя файла с фотографией работника
//     • величина оклада работника
//     • метод вычисления стажа работника для текущей даты
public record Worker(int Id, string Fullname, DateTime JoinDate, string Photo, int Salary)
{
    // метод вычисления стажа работника для текущей даты
    public int Expirience() => DateTime.Now.Year - JoinDate.Year;
} // class Worker

