using System.ComponentModel.DataAnnotations;
using Home_work.Infrastructure;

namespace Home_work.ViewModels;

public class RangeModel
{
    [Required(ErrorMessage = "Поле обязатено к заполнению!")]
    [UIHint("Number")]
    [Range(0, int.MaxValue, ErrorMessage = "Некорретный диапазон")]
    [Display(Name = "Минимальное значение")]
    public int Min { get; set; }


    [Required(ErrorMessage = "Поле обязатено к заполнению!")]
    [UIHint("Number")]
    [Range(0, int.MaxValue, ErrorMessage = "Некорретный диапазон")]
    [Display(Name = "Максимальное значение")]
    public int Max { get; set; }

    public string Controller { get; set; }

    public string Action { get; set; }

    public RangeModel(int min, int max, string controller,string action)
    {
        if (min >= max)
            throw new Exception("Мин. значение диапазона должно быть < максимального");

        Min = min;
        Max = max;
        Controller = controller;
        Action = action;

    }

    public RangeModel(): this(10,101,"default","default")
    {

    }
}
