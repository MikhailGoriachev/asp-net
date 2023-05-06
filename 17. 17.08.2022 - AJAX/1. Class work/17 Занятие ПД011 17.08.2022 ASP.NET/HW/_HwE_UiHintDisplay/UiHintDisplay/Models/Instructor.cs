using System.ComponentModel.DataAnnotations;

namespace UiHintDisplay.Models;

// Сведения об инструкторах содержат: идентификатор инструктора, фамилия,
// имя, отчество, дата рождения, категория (А, В, С; А – низшая категория,
// С – высшая категория).
public class Instructor
{
    // идентификатор инструктора
    [UIHint("HiddenInput")]
    public int Id { get; set; }

    // фамилия инструктора
    public string Surname { get; set; }

    // имя инструктора
    public string Name { get; set; }

    // отчество инструктора
    public string Patronymic { get; set; }

    // дата рождения инструктора
    public DateTime BornDate { get; set; }

    // категория инструктора (А, В, С; А – низшая категория, С – высшая категория)
    public string Category { get; set; }

    // фамилия и инициалы - свойство только для чтения
    public string ShortName => $"{Surname} {Name[0]}.{Surname[0]}.";


    #region Конструкторы
    public Instructor():this(1, "", "", "", new DateTime(1900, 1, 1), "A") {

    } // Instructor

    public Instructor(int id, string surname, string name, string patronymic,
        DateTime bornDate, string category) {
        Id = id;
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        BornDate = bornDate;
        Category = category;
    } // Instructor

    #endregion
} // class Instructor

