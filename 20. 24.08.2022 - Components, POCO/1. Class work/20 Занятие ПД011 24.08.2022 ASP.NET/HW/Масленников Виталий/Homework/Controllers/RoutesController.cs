﻿using Homework.Common;
using Homework.Models.TravelCompany;
using Homework.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Route = Homework.Models.TravelCompany.Route;

namespace Homework.Controllers;

[Route("Routes")]
public class RoutesController : Controller
{
    private TravelCompany _travelCompany;

    // Конструктор
    public RoutesController(TravelCompany travelCompany) =>
        _travelCompany = travelCompany;

    public IActionResult ExceptionTest() =>
        throw new Exception($"{ControllerContext.RouteData.Values["controller"]} error");
    
    //	Вывод сведений о маршруте в порядке хранения в файле
    [HttpGet("Index")]
    public IActionResult Index() => View(new RoutesIndexViewModel
    {
        Routes = _travelCompany.Routes,
        Instructors = _travelCompany.Instructors,
        PointsSelectLists = _travelCompany.GetPointsSelectLists()
    });

    // Переход на форму добавления нового маршрута
    [HttpGet("AddRoute")]
    public IActionResult AddRoute()
    {
        Route route = new() { Id = 0 };
        return View("RouteForm", new RouteInputForm(route, _travelCompany.Instructors, Seed.Difficulties));
    }

    // Переход на форму редактирования данных маршрута
    [HttpGet("EditRoute/{id:int}")]
    public IActionResult EditRoute(int id)
    {
        var route = _travelCompany.Routes.First(b => b.Id == id);
        return View("RouteForm", new RouteInputForm(route, _travelCompany.Instructors, Seed.Difficulties));
    }

    // Удаление маршрута из коллекции
    [HttpGet("RemoveRoute/{id:int}")]
    public IActionResult RemoveRoute(int id)
    {
        _travelCompany.RemoveRoute(id);
        _travelCompany.SaveData();
        return View("Index", new RoutesIndexViewModel
        {
            Routes = _travelCompany.Routes,
            Instructors = _travelCompany.Instructors,
            PointsSelectLists = _travelCompany.GetPointsSelectLists()
        });
    }

    // Создание/обновление данных в зависимости от существования Id
    [HttpPost("UpdateRoute")]
    public IActionResult UpdateRoute(Route route)
    {
        if (!ModelState.IsValid)
            return View("RouteForm", new RouteInputForm(route, _travelCompany.Instructors, Seed.Difficulties));

        _travelCompany.AddOrUpdateRoute(route);
        return View("Index", new RoutesIndexViewModel
        {
            Routes = _travelCompany.Routes,
            Instructors = _travelCompany.Instructors,
            PointsSelectLists = _travelCompany.GetPointsSelectLists()
        });
    }

    // Упорядочивание коллекции маршрутов по параметрам
    [HttpGet("OrderBy/{property}/{sort}")]
    public IActionResult OrderBy(string property, string sort)
    {
        var routes = _travelCompany.Routes;

        routes = (property == "Difficulty")
            ? routes.OrderBy(r => Seed.Difficulties.IndexOf(r.Difficulty))
            : _travelCompany.Routes.OrderBySort(property, sort);
        ;

        return View("Index", new RoutesIndexViewModel
        {
            Routes = routes,
            Instructors = _travelCompany.Instructors,
            PointsSelectLists = _travelCompany.GetPointsSelectLists()
        });
    }

    // Выборка из коллекции маршрутов по заданному пункту маршрута
    [HttpPost("WherePoint")]
    public IActionResult WherePoint(string value, string point)
    {
        var routes = point switch
        {
            "Start" => _travelCompany.Routes.Where(r => r.StartPoint == value),
            "Middle" => _travelCompany.Routes.Where(r => r.MiddlePoint == value),
            _ => throw new ArgumentOutOfRangeException(nameof(point), point, null)
        };

        return View("Index", new RoutesIndexViewModel
        {
            Routes = routes,
            Instructors = _travelCompany.Instructors,
            PointsSelectLists = _travelCompany.GetPointsSelectLists()
        });
    }

    // Выборка из коллекции маршрутов по заданному диапазону протяженности
    [HttpPost("WhereLength")]
    public IActionResult WhereLength(int RouteMaxLength, int RouteMinLength)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", new RoutesIndexViewModel
            {
                Routes = _travelCompany.Routes,
                Instructors = _travelCompany.Instructors,
                PointsSelectLists = _travelCompany.GetPointsSelectLists()
            });
        }

        var test = _travelCompany.Routes.Where(r => r.Length >= RouteMinLength && r.Length <= RouteMaxLength);

        return View("Index", new RoutesIndexViewModel
        {
            Routes = _travelCompany.Routes.Where(r => r.Length >= RouteMinLength && r.Length <= RouteMaxLength),
            Instructors = _travelCompany.Instructors,
            PointsSelectLists = _travelCompany.GetPointsSelectLists()
        });
    }

    // Получить данные о маршруте в JSON-формате
    [HttpGet("RouteInfo/{id:int}")]
    public IActionResult RouteInfo(int id)
    {
        var route = _travelCompany.Routes.FirstOrDefault(r => r.Id == id);
        var instructor = _travelCompany.Instructors.FirstOrDefault(i => i.Id == route!.InstructorId);
        return Json(route is not null
            ? new
            {
                route.Id,
                route.StartPoint,
                route.MiddlePoint,
                route.FinishPoint,
                route.Length,
                route.Difficulty,
                Instructor = instructor?.ShortName ?? "Not Found"
            }
            : new { error = "Not Found" });
    }
}