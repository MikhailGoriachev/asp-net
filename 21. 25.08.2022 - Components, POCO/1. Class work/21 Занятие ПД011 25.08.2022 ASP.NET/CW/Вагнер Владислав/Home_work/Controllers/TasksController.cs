using Microsoft.AspNetCore.Mvc;
using Home_work.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Home_work.Infrastructure;
using Newtonsoft.Json;
using System.IO;


namespace Home_work.Controllers;


public class TasksController : Controller
{
    //Коллекция людей
    public IActionResult Characters()
    {
        return View();
    }
    //Коллекция планет 
    public IActionResult Planet()
    {
        return View();
    }


}
