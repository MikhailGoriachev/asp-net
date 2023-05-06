namespace HomeWork.Models.ViewModels;

public class PlanetsViewModel
{
    // записи
    public List<Planet>? Data { get; set; }

    // статистика диаметра планеты
    public (double? min, double? avg, double? max) DiameterStat { get; set; }

    // статистика орбитального периода планеты
    public (double? min, double? avg, double? max) OrbitalPeriodStat { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public PlanetsViewModel()
    {
    }


    // конструктор инициализирующий
    public PlanetsViewModel(List<Planet>? data, (double? min, double? avg, double? max) diameterStat,
        (double? min, double? avg, double? max) orbitalPeriodStat)
    {
        Data = data;
        DiameterStat = diameterStat;
        OrbitalPeriodStat = orbitalPeriodStat;
    }

    #endregion
}
