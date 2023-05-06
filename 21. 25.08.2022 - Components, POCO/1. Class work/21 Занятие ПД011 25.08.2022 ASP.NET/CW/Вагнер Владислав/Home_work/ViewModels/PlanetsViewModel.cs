using Home_work.Models;

namespace Home_work.ViewModels;

public class PlanetsViewModel
{
    //Коллекция постов пользователей
    public List<Planet> Posts { get; set; }

    //Общее кол-во постов
    public int PostsQuantity { get; set; }

    //Мин. длина текста
    public int MinTxtLenght { get; set; }

    //Средняя длина текста
    public int AvgTxtLenght { get; set; }

    //Макс. длина текста
    public int MaxTxtLenght { get; set; }

    public PlanetsViewModel(List<Planet> posts)
    {
        Posts = posts;

        //Вычисления и задания свойств
        
        PostsQuantity = posts.Count();

        IEnumerable<int> lengths = posts.Select(p => p.Body.Length)/*.ToArray()*/;

        MinTxtLenght = lengths.Min();
        AvgTxtLenght = (int)lengths.Average();
        MaxTxtLenght = lengths.Max();

        /*MinTxtLenght = posts.Select(p => p.Body.Length).Min();
        AvgTxtLenght = (int)posts.Select(p => p.Body.Length).Average();
        MaxTxtLenght = posts.Select(p => p.Body.Length).Max();*/

    }
}
