using Home_work.Models.Planets;

namespace Home_work.ViewModels;

public class PlanetsViewModel
{

    //Коллекция планет
    public List<Planet> Planets { get; set; }

    //Значения для расчета в компоненте
    public (double? min, double? avg, double? max) Diameter { get; set; } = (1d, 1d, 1d);
    public (double? min, double? avg, double? max) OrbitalPeriod { get; set; } = (1d, 1d, 1d);



    public PlanetsViewModel(List<Planet> planets, (double? min, double? avg, double? max) diameter,
        (double? min, double? avg, double? max) period)
    {
        Planets = planets;
        Diameter = diameter;
        OrbitalPeriod = period;
    }
}
