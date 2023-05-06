using Microsoft.AspNetCore.Mvc;
using Home_work.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Home_work.Infrastructure;
using Newtonsoft.Json;
using System.IO;


namespace Home_work.Controllers;


public class TasksController : Controller
{
    //Представление запроса 1 
    public IActionResult Users()
    {
        return View();
    }
    //Представление запроса 2 
    public IActionResult UsersPosts()
    {
        return View();
    }

    //Представление запроса 3 
    public IActionResult UsersAlbum()
    {
        return View();
    }

    //Представление запроса 4 
    public IActionResult UsersTodo()
    {
        return View();
    }

}
