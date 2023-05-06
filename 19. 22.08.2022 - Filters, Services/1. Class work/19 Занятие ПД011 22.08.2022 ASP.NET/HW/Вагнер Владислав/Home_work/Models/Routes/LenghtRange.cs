using System.ComponentModel.DataAnnotations;
using Home_work.Infrastructure;

namespace Home_work.Models.Routes;

public class LenghtRange
{
    [Required(ErrorMessage = "Поле обязатено к заполнению!")]
    [UIHint("Number")]
    [Range(5, 15000, ErrorMessage = "Введите число в диапазоне от 5 км до 15000 км")]
    [Display(Name = "Минимальная протяженность")]
    public int Min { get; set; }


    [Required(ErrorMessage = "Поле обязатено к заполнению!")]
    [UIHint("Number")]
    [Range(5, 15000, ErrorMessage = "Введите число в диапазоне от 5 км до 15000 км")]
    [Display(Name = "Максимальная протяженность")]
    public int Max { get; set; }

    public LenghtRange(int min, int max)
    {
        Min = min;
        Max = max;
    }

    public LenghtRange():this(10,100)
    {

    }
}
