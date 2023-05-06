namespace HomeWork.Models.ViewModels;

public class PeopleListViewModel
{
    // записи
    public List<Person>? Data { get; set; }

    // статистика веса персонажа
    public (double? min, double? avg, double? max) WeightStat { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public PeopleListViewModel()
    {
    }

    // конструктор инициализирующий
    public PeopleListViewModel(List<Person>? data, (double? min, double? avg, double? max) weightStat)
    {
        Data = data;
        WeightStat = weightStat;
    }

    #endregion
}
