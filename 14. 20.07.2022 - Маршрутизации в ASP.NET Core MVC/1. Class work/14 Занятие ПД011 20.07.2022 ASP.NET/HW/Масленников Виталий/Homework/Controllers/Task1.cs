using Homework.Models.Task1;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers;

public class Task1 : Controller
{
    // Контрольные наборы данных
    private readonly (double x, double y, double z) _defArgs1 = (1, 1, 3); 
    private readonly (double x, double y, double z) _defArgs2 = (3, 4, 5); 
    
    // GET /Task1/Expression1 
    public IActionResult Expression1()
    {
        ExpressionResult expr1ExpressionResult = SolveExpression1(_defArgs1.x, _defArgs1.y, _defArgs1.z);
        return View(expr1ExpressionResult);
    }    
    
    // GET /Task1/Expression2 
    public IActionResult Expression2()
    {
        ExpressionResult expr2ExpressionResult = SolveExpression2(_defArgs2.x, _defArgs2.y, _defArgs2.z);
        return View(expr2ExpressionResult);
    }

    // Вычисление первого уравнения
    private ExpressionResult SolveExpression1(double x, double y, double z)
    {
        double temp = x * x + 4;
        double a = (1 + y) * ((x + y / temp) / (Math.Pow(Math.E, -x - 2) + 1d / temp));
        
        temp = Math.Sin(z);
        double b = (1 + Math.Cos(y - 2)) / (Math.Pow(x, 4) / 2 + temp * temp);

        return new ExpressionResult{ A = a, B = b };
    } 
    
    // Вычисление второго уравнения
    private ExpressionResult SolveExpression2(double x, double y, double z)
    {
        double temp = Math.Sin(x + y);
        double a = (1 + temp * temp) / (2 + Math.Abs(x - 2 * x / (1 + x * x * y * y))) + x;
        
        double b = Math.Cos(Math.Atan(1 / z));
        b *= b;
        
        return new ExpressionResult { A = a, B = b };
    }
}