using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models.ViewModels.Instructors;

public class SelectByCategoryViewModel
{
    // инструкторы
    public IEnumerable<Instructor> Instructors { get; set; } = null!;


    // категории
    [Required]
    [Display(Name = "Категория:")]
    public string Category { get; set; } = "";

    #region Конструкторы

    // конструктор по умолчанию
    public SelectByCategoryViewModel()
    {

    }


    // конструктор инициализирующий
    public SelectByCategoryViewModel(IEnumerable<Instructor> instructors, string category)
    {
        Instructors = instructors;
        Category = category;
    }

    #endregion
}
