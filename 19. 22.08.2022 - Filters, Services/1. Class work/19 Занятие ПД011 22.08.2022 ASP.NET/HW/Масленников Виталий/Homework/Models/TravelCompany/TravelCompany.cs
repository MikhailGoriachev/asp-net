using Homework.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Homework.Models.TravelCompany;

// Класс для обработки данных туристической фирмы
public class TravelCompany
{
    private readonly string _dataPathInstructors = "App_Data/instructors.json";
    private readonly string _dataPathRoutes = "App_Data/routes.json";
    private readonly string _dataPathClients = "App_Data/clients.json";
    
    private List<Route> _routes;
    private List<Instructor> _instructors;
    private List<Client> _clients;

    public IEnumerable<Route> Routes => _routes;
    public IEnumerable<Instructor> Instructors => _instructors;
    public IEnumerable<Client> Clients => _clients;

    public TravelCompany()
    {
        if (File.Exists(_dataPathInstructors))
            _instructors = Utils.Deserialize<Instructor>(_dataPathInstructors);
        else
        {
            _instructors = Seed.Instructors();
            Utils.Serialize(_instructors, _dataPathInstructors);
        }
        
        if (File.Exists(_dataPathRoutes))
            _routes = Utils.Deserialize<Route>(_dataPathRoutes);
        else
        {
            _routes = Seed.Routes();
            Utils.Serialize(_routes, _dataPathRoutes);
        }
        
        if (File.Exists(_dataPathClients))
            _clients = Utils.Deserialize<Client>(_dataPathClients);
        else
        {
            _clients = Seed.Clients();
            Utils.Serialize(_clients, _dataPathClients);
        }
    }

    // Добавить или обновить данные о маршруте в зависимости от существования Id
    public void AddOrUpdateRoute(Route route)
    {
        int index = _routes.FindIndex(b => b.Id == route.Id);
        
        if (index < 0)
        {
            int id = _routes.Any() ? _routes.Max(r => r.Id + 1) : 1;
            route.Id = id;
            _routes.Insert(0, route);
        } else { 
            _routes[index] = route;
        } 
    }
    
    // Добавить или обновить данные об инструкторе в зависимости от существования Id
    public void AddOrUpdateInstructor(Instructor instructor)
    {
        int index = _instructors.FindIndex(b => b.Id == instructor.Id);
        
        if (index < 0)
        {
            int id = _instructors.Any() ? _instructors.Max(r => r.Id + 1) : 1;
            instructor.Id = id;
            _instructors.Insert(0, instructor);
        } else { 
            _instructors[index] = instructor;
        } 
    } 
    
    // Добавить или обновить данные о клиенте в зависимости от существования Id
    public void AddOrUpdateClient(Client client)
    {
        int index = _clients.FindIndex(c => c.Id == client.Id);
        
        if (index < 0)
        {
            int id = _clients.Any() ? _clients.Max(c => c.Id + 1) : 1;
            client.Id = id;
            _clients.Insert(0, client);
        } else { 
            _clients[index] = client;
        } 
    }
    
    // Удаление маршрута
    public void RemoveRoute(int id) => 
        _routes.Remove(_routes.First(r => r.Id == id));

    // Удаление клиента
    public bool RemoveClient(int id)
    {
        var client = _clients.FirstOrDefault(c => c.Id == id);
        if (client is null) return false;
        _clients.Remove(client);
        return true;
    }

    // Сохранение данных в файлы
    public void SaveData()
    {
        Utils.Serialize(_instructors, _dataPathInstructors);
        Utils.Serialize(_routes, _dataPathRoutes);
        Utils.Serialize(_clients, _dataPathClients);
    }
    
    // Получить списки уникальных значений пунктов маршрута в виде SelectList
    public PointsSelectLists GetPointsSelectLists() =>
        new ()
        {
            StartPoints = new SelectList(PopulateStartPoints()),
            FinishPoints = new SelectList(PopulateFinishPoints()),
            MiddlePoints = new SelectList(PopulateMiddlePoints()),
        };
    
    private IEnumerable<string> PopulateStartPoints() => _routes.Select(r => r.StartPoint).Distinct();
    private IEnumerable<string> PopulateMiddlePoints() => _routes.Select(r => r.MiddlePoint).Distinct();
    private IEnumerable<string> PopulateFinishPoints() => _routes.Select(r => r.FinishPoint).Distinct();
}