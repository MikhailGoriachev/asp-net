using Newtonsoft.Json;
using Home_work.Models.Characters;
using Newtonsoft.Json.Serialization;
using Home_work.Infrastructure;
using Home_work.Models.Planets;

namespace Home_work.Models;
public class GeneralModelService
{


    private HttpClient httpClient;

    //Коллекция персонажей
    private CharactersDeserializationModel? _deserializationCharactesModel = null;


    //Коллекция планет
    private PlanetsDeserializationModel? _deserializationPlanetsModel = null;

    //Путь к файлу
    private string _charactersFile = "App_Data/characters.json";
    private string _planesFile = "App_Data/planets.json";

    public GeneralModelService()
    {
        httpClient = new HttpClient();
    }



    //Получение данных из http запроса
    public async Task<T> GetDataAsync<T>(string url)
    {
        //Получение данных от сервера
        HttpResponseMessage response = await httpClient.GetAsync(url);

        string json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(json);
    }

    #region Персонажи

    //Сформировать коллекциюю аснхронно
    private async Task<CharactersDeserializationModel> FormCharactersAsync(string characterUrl)
    {

        //Получаем модель-контейнер с коллекцией
        CharactersDeserializationModel charactersModel = await GetDataAsync<CharactersDeserializationModel>(characterUrl);

        //Временный список для заполнения основной коллекции записями со следующих странц запросов
        CharactersDeserializationModel tempCharactersModel = new CharactersDeserializationModel(1, new List<Character>(), charactersModel.Next);

        //Получение нужного количества записей
        while (charactersModel.Characters.Count < 42)
        {
            tempCharactersModel = GetDataAsync<CharactersDeserializationModel>(tempCharactersModel.Next).Result;
            charactersModel.Characters.AddRange(tempCharactersModel.Characters);
        }

        //Удаление элементов для сокращения длины списка до требуемого
        if (charactersModel.Characters.Count > 42)
            while (charactersModel.Characters.Count > 42)
                charactersModel.Characters.RemoveAt(charactersModel.Characters.Count - 1);

        int id = 0;
        double weight = 0;
        charactersModel.Characters.ForEach(c =>
        {
            c.Id = ++id;
            bool parsed = double.TryParse(c.Mass, out weight);
            c.WeightDouble = parsed ? weight : 0d;

            c.Homeworld = GetDataAsync<PlanetName>(c.Homeworld).Result.Name;
        });

        return charactersModel;
    }


    //Получение персонажей
    public async Task<CharactersDeserializationModel> GetCharactersAsync(string charactersUrl = "https://swapi.dev/api/people/?format=json")
    {
        //Получаем модель-контейнер с коллекцией
        if (!File.Exists(_charactersFile))
        {
            _deserializationCharactesModel = await FormCharactersAsync(charactersUrl);
            Utils.Serialize(_deserializationCharactesModel, _charactersFile, '/');
        }
        else
            _deserializationCharactesModel = Utils.Deserialization<CharactersDeserializationModel>(_charactersFile);


        return _deserializationCharactesModel;
    }


    //Получение конкретного перснажа
    public async Task<Character> GetCharacterAsync(string url)
        => await GetDataAsync<Character>(url); 


    #endregion

    //Сформировать коллекциюю аснхронно
    private async Task<PlanetsDeserializationModel> FormPlanetsAsync(string planetsUrl)
    {

        //Получаем модель-контейнер с коллекцией
        PlanetsDeserializationModel planetsModel = await GetDataAsync<PlanetsDeserializationModel>(planetsUrl);

        //Временная модель 
        PlanetsDeserializationModel tempPlanetsModel = new PlanetsDeserializationModel(1, new List<Planet>(), planetsModel.Next);

        //Получение нужного количества записей
        while (planetsModel.PlanetsList.Count < 33)
        {
            tempPlanetsModel = GetDataAsync<PlanetsDeserializationModel>(tempPlanetsModel.Next).Result;
            planetsModel.PlanetsList.AddRange(tempPlanetsModel.PlanetsList);
        }

        //Удаление элементов для сокращения длины списка до требуемого
        if (planetsModel.PlanetsList.Count > 33)
            while (planetsModel.PlanetsList.Count > 33)
                planetsModel.PlanetsList.RemoveAt(planetsModel.PlanetsList.Count - 1);

        int id = 0;
        int rotationPeriod = 0;
        int orbitalPeriod = 0;
        int diameter = 0;
        double gravity = 0d;

        List<string> residentsNames = new List<string>();

        planetsModel.PlanetsList.ForEach(p =>
        {
            residentsNames.Clear();
            p.Id = ++id;

            //Период вращения
            bool parsed = int.TryParse(p.RotationPeriod, out rotationPeriod);

            p.RotationPeriodInt = parsed ? rotationPeriod : 0;

            //Орбитальный период
            parsed = int.TryParse(p.OrbitalPeriod, out orbitalPeriod);

            p.OrbitalPeriodInt = parsed ? orbitalPeriod : 0;

            //Диаметр
            parsed = int.TryParse(p.Diameter, out diameter);

            p.DiameterInt = parsed ? diameter : 0;

            int ind = p.Gravity.IndexOf(" ");

            //Гравитация 
            //Условие: если пробел не найден, тогда проверка не состоит ли запись из слова, если нет, тогда парсим
            parsed = ind > 0 ? double.TryParse(p.Gravity.Remove(ind),out gravity) 
            : p.Gravity.Contains("unknown") || p.Gravity.ToLower().Contains("n/a") ? false : double.TryParse(p.Gravity, out gravity);

            p.GravityNumeric = parsed ? gravity : 0;

            //Резидены планеты

            p.Residents.ForEach(r => residentsNames.Add(GetCharacterAsync(r).Result.Name));

            p.Residents = residentsNames.ToList();
        });

        return planetsModel;
    }


    //Получение планет
    public async Task<PlanetsDeserializationModel> GetPlanetsAsync(string planetsUrl = "https://swapi.dev/api/planets/?format=json")
    {
        //Получаем модель-контейнер с коллекцией
        if (!File.Exists(_planesFile)){
            _deserializationPlanetsModel = await FormPlanetsAsync(planetsUrl);
            Utils.Serialize(_deserializationPlanetsModel, _planesFile, '/');
        }
        else
            _deserializationPlanetsModel = Utils.Deserialization<PlanetsDeserializationModel>(_planesFile);


        return _deserializationPlanetsModel;
    }


    //Получение конкретного перснажа
    public async Task<Planet> GetPlanetAsync(string url)
    { 
        Planet planet = await GetDataAsync<Planet>(url);

        //Если файл есть, тогда читаем ранее спарсенных перонажей
        if (File.Exists(_planesFile)){
            _deserializationPlanetsModel = Utils.Deserialization<PlanetsDeserializationModel>(_planesFile);
            planet.Residents = _deserializationPlanetsModel.PlanetsList.Find(p => p.Url == planet.Url).Residents;
        }
        //Если файла с планетами нет, то отправляем запросы по каждому персонажу
        else
        {
            List<string> residentsNames = new List<string>();
            planet.Residents.ForEach(r => residentsNames.Add(GetCharacterAsync(r).Result.Name));

            planet.Residents = residentsNames.ToList();
        }


        return planet;

    }


}

public record PlanetName(string Name);