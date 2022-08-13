namespace SimpleApp.Models;

// пример модели данных
/*
public class Driver
{
    public string Name { get; }
    public int Age { get; }

    public Driver(string name, int age) {
        Name = name;
        Age = age;
    }// Driver

    // деконструктор
    public void Deconstruct(out string name, out int age) => (name, age) = (Name, Age);

    // Пример использования деконструктора
    // Driver driver = new Driver("Tim", 45);
    // (string name, int age) = driver;
    // name <- driver.Name, age <- driver.Age

    public override string ToString() => $"Driver {Name} ({Age} лет)";
} // class Driver
*/

// пример использования синтаксиса C# 9 - позиционный record для сокращения 
// требуемого объема кода

// для Driver будет создаваться конструктор, который принимает два параметра
// и присваивает их значения соответственно свойствам Name и Age, и что также
// автоматически будет создаваться деконструктор, метод ToString()
public record Driver(string Name, int Age)
{
    // применим собственный вариант метода ToString()
    public override string ToString() => $"Водитель {Name} ({Age} лет)";
}


