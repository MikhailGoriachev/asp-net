namespace HomeWork.Models;

// фамилия и инициалы работника
// дата поступления на работу
// имя файла с фотографией работника
// величина оклада работника
// метод вычисления стажа работника для текущей даты

// Класс Работник
public record Worker(string FullName, DateTime DateOfEmployment, string ImageFile, int Salary)
{
    // вычисление стажа работника
    public int GetWorkExp() => DateTime.Now.Year - DateOfEmployment.Year;
};
