using HomeWork.Models.Task1;
using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models.ViewModels;

public class SelectByMass
{
    // персонажи
    public IEnumerable<Person> Persons { get; set; } = new List<Person>();

    // минимальный вес
    [Required(ErrorMessage = "Введите минимальный вес")]
    [Range(2, 300, ErrorMessage = "Минимальный вес должен быть в диапазоне от 2 до 300 кг")]
    public string MinMass { get; set; }

    // максимальный вес
    [Required(ErrorMessage = "Введите максимальный вес")]
    [Range(2, 300, ErrorMessage = "Максимальный вес должен быть в диапазоне от 2 до 300 кг")]
    public string MaxMass { get; set; }


    // конструкторы
    public SelectByMass()
    {
    }


    public SelectByMass(IEnumerable<Person> persons, string minMass = "", string maxMass = "")
    {
        Persons = persons;

        MinMass = minMass;
        MaxMass = maxMass;
    }
} // class SelectByMass
