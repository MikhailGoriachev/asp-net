using System.Diagnostics;
using ASP_NET_15_FormAspNet.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ASP_NET_15_FormAspNet.Models.Figures;

namespace ASP_NET_15_FormAspNet.Controllers;

public class SecondTaskController : Controller
{
    // коллекция фигур
    private FigureList _figureList = new FigureList();
     
    // тип фигуры
    [BindProperty]
    public string? FigureType { get; set; }
   
    // GET: вывод всей коллекции в исходном порядке
    public IActionResult Index() {
        ViewBag.Title = "Вывод исходной коллекции";
        return View("FigureList", _figureList);
    }

    // добавление фигуры
    public IActionResult AddFigure() {
        ViewBag.Title = "Коллекция фигур после добавления фигуры";
        int id = _figureList.Figures.Count > 0 ? _figureList.Figures.Max(item => item.Id) + 1 : 1;
        _figureList.Figures.Insert(0, Utilities.GetFigure(id));
        _figureList.Serialize();
       
        return View("FigureList", _figureList); 
    }
 
    // удаление фигуры
    public IActionResult RemoveFigure(int id) {
        ViewBag.Title = "Коллекция фигур, после удаления фигуры";  
        _figureList.Figures.Remove(_figureList.Figures.FirstOrDefault(item=>item.Id == id)!);
        _figureList.Serialize();
        return View("FigureList", _figureList);
    }
    
    public IActionResult EditFigure(int id) { 
        IFigure figure = _figureList.Figures.First(item => item.Id == id);
        FigureModel figureModel;
            
        switch(figure.Name) {
            case "Квадрат": 
                figureModel= new FigureModel(id, figure.Name, figure.ParamAndValue["a"],null,null );
                break;
                
            case "Прямоугольник": 
                figureModel= new FigureModel(id, figure.Name, figure.ParamAndValue["a"], 
            figure.ParamAndValue["b"], null);
             break;
                
                
            case "Треугольник":
                figureModel= new FigureModel(id, figure.Name, figure.ParamAndValue["a"], 
            figure.ParamAndValue["b"], figure.ParamAndValue["c"]);
                break; 
            
            default:
                throw new Exception("Ошибка при передачи модели в форму"); 
        } 
        return View(figureModel);
    }
    
    // POST /Home/Register
    [HttpPost]
    public IActionResult EditFigure(FigureModel model) {
        // этот метод получает POST запрос, валидирует модель и выполняет бизнес
        // правило связанное с этой моделью.
        // в данном примере пропущена валидация и дальнейшее использование модели
        // (эти темы будут рассмотрены в следующих уроках)  
        
        switch(_figureList.Figures[model.IdFigure].Name){
            case "Квадрат":  
                Square sqar = (Square)_figureList.Figures.First(item => item.Id == model.IdFigure); 
                sqar.Side = model.SideA; 
                break;
                
            case "Прямоугольник": 
                Rectangle rectangle = (Rectangle)_figureList.Figures.First(item => item.Id == model.IdFigure); 
                rectangle.SideA = model.SideA;
                rectangle.SideB = model.SideB ?? 1d; 
                break;
                
                
            case "Треугольник":
                Triangle triangle = (Triangle)_figureList.Figures.First(item => item.Id == model.IdFigure); 
                triangle.Sides = (model.SideA, (double)model.SideB!, (double)model.SideC!); 
                break; 
            
            default:
                throw new Exception("Ошибка при получении данных из формы"); 
        }
 
        _figureList.Serialize();
        return View("FigureList", _figureList);
    } // Register
    
    // вывод всей коллекции, упорядоченной по убывнию площади
    public IActionResult OrderByDescArea() { 
        ViewBag.Title = "Вывод всей коллекции, упорядоченной по убывнию площади";
        _figureList.Figures = _figureList.OrderByDescArea();
        return View("FigureList", _figureList);
    }


    // вывод всей коллекции, упорядоченной по типу фигур
    public IActionResult OrderByName() {
        ViewBag.Title = "Вывод всей коллекции, упорядоченной по типу фигур";
        _figureList.Figures = _figureList.OrderByName();
        return View("FigureList", _figureList);
    }


    // выборка квадратов, сортировка по возростанию периметра
    public IActionResult SelectBySquare() {
        ViewBag.Title = "Выборка квадратов, сортировка по возростанию периметра";
        _figureList.Figures = _figureList.SelectBySquare();
        return View("FigureList", _figureList);
    }


    // выборка прямоугольников, сортировка по убыванию периметра
    public IActionResult SelectByRect() {
        ViewBag.Title = "Выборка прямоугольников, сортировка по убыванию периметра";
        _figureList.Figures = _figureList.SelectByRectangle();
        return View("FigureList", _figureList);
    } 

    // выборка треугольников, в обратном порядке по отношению к порядку в коллекции
    public IActionResult SelectByTriangle() {
        ViewBag.Title = "Выборка треугольников, в обратном порядке по отношению к порядку в коллекции";
        _figureList.Figures = _figureList.SelectByTriangle();
        return View("FigureList", _figureList);
    }
}