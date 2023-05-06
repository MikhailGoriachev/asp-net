using Homework.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers;

[Route("Task1")]
public class Task1Controller : Controller
{
    // GET
    [HttpGet("Index")]
    public IActionResult Index()
    {
        return View(new Task1ViewModel());
    }
    
    // GET
    [HttpGet("Equation1")]
    public IActionResult Equation1()
    {
        var eq = new Equation
        {
            X = 1, Y = 1, Z = 3, ControlResultA = 9.608184, ControlResultB = 2.962605
        };
        
        double tmp = 1d / ((eq.X * eq.X) + 4);
        eq.A = (1 + eq.Y) * (eq.X + eq.Y * tmp) / 
               ( Math.Exp(-eq.X - 2) + tmp);

        eq.B = (1 + Math.Cos(eq.Y - 2)) / 
               (Math.Pow(eq.X, 4) / 2 + Math.Sin(eq.Z) * Math.Sin(eq.Z));
        
        
        return View("Index", new Task1ViewModel{ Equation1 = eq});
    }
    
    [HttpGet("Equation2")]
    public IActionResult Equation2()
    {
        var eq = new Equation
        {
            X = 3, Y = 4, Z = 5, ControlResultA = 3.288716, ControlResultB = 0.9615385
        };

        eq.A = (1 + Math.Sin(eq.X + eq.Y) * Math.Sin(eq.X + eq.Y))
               / (2 + Math.Abs(eq.X - 2 * eq.X / (1 + eq.X * eq.X * eq.Y * eq.Y)))
               + eq.X;

        eq.B = Math.Cos(Math.Atan(1 / eq.Z)) * Math.Cos(Math.Atan(1 / eq.Z));
        
        return View("Index", new Task1ViewModel{ Equation2 = eq});
    }
    
}