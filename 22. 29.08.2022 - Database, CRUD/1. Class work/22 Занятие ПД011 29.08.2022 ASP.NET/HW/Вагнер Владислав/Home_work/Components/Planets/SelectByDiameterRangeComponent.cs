using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Home_work.Models;
using Home_work.ViewModels;
using Home_work.Models.Planets;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Home_work.Components.Planets;

public class SelectByDiameterRangeComponent : ViewComponent
{
    private GeneralModelService _generalModelService;

    //ctor с внедрением зависимостей
    public SelectByDiameterRangeComponent(GeneralModelService modelService)
    {
        _generalModelService = modelService;
    }

    //Асинхронный запуск компонента
    public async Task<IViewComponentResult> InvokeAsync(RangeModel range)
    {
        //Список пользователей
        PlanetsDeserializationModel deserializationModel = await _generalModelService.GetPlanetsAsync();

        //Выпадающий список поверхностй планет
        ViewBag.SelectPlanets = new SelectList(deserializationModel.PlanetsList.Select(p => {

            if (p.Terrain.Contains(','))
                return p.Terrain.Replace(p.Terrain.Substring(p.Terrain.IndexOf(',')), "");

            return p.Terrain;
        }).Distinct().OrderBy(t => t));

        //Выборка по диапазону
        deserializationModel.PlanetsList = deserializationModel.PlanetsList
            .Where(p => p.DiameterInt >= range.Min && p.DiameterInt <= range.Max).ToList();

        //Расчеты
        (double?, double?, double?) Diameters = (deserializationModel.PlanetsList.Min(p => p.DiameterInt),
            deserializationModel.PlanetsList.Average(p => p.DiameterInt),
            deserializationModel.PlanetsList.Max(p => p.DiameterInt));

        (double?, double?, double?) OrbitalPeriod = (deserializationModel.PlanetsList.Min(p => p.OrbitalPeriodInt),
            deserializationModel.PlanetsList.Average(p => p.OrbitalPeriodInt),
            deserializationModel.PlanetsList.Max(p => p.OrbitalPeriodInt));

        //Формируем view model
        PlanetsViewModel viewModel = new PlanetsViewModel(deserializationModel.PlanetsList,Diameters,OrbitalPeriod);

        return View(viewModel);
    }

}
