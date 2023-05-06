namespace Homework.Models.Task2;

// Модель данных о работнике
public class Worker
{
    //  фамилия и инициалы работника
    public string FullName { get; set; } = null!;
    
    //  дата поступления на работу
    public DateTime StartDate { get; set; }
    
    //  имя файла с фотографией работника
    public string Image { get; set; } = null!;
    
    //  величина оклада работника
    public int Salary { get; set; }
    
    //  метод вычисления стажа работника для текущей даты
    public int LengthOfService => new DateTime((DateTime.Now - StartDate).Ticks).Year - 1;
}