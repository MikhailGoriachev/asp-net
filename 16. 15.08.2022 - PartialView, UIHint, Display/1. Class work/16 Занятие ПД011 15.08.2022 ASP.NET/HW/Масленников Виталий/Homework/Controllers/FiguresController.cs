using Homework.Common;
using Homework.Models.Figures;
using Homework.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers;

[Route("Figures")]
public class FiguresController : Controller
{
    private readonly IFiguresRepository _figuresRepository;
    
    public FiguresController(IFiguresRepository figuresRepository) => 
        _figuresRepository = figuresRepository;

    [HttpGet("Index")]
    public IActionResult Index() =>
        View( new FiguresIndexViewModel
        {
            Figures = _figuresRepository.Figures,
            FiguresCounts = _figuresRepository.FiguresCounts()
        });

    [HttpGet("AddFigure/")]
    public IActionResult AddFigure()
    {
        _figuresRepository.Add();
        _figuresRepository.SaveData();
        return View("Index", new FiguresIndexViewModel
        {
            Figures = _figuresRepository.Figures,
            FiguresCounts = _figuresRepository.FiguresCounts()
        });
    }

    [HttpGet("EditFigure/{id}")]
    public IActionResult EditFigure(int id)
    {
        var found = _figuresRepository.Get(id);
        return View("EditFigure", new FigureFormViewModel
        {
            FigureInput = new FigureInput(found)
        });
    }

    [HttpPost("EditFigure/{figureInput}")]
    public IActionResult EditFigure(FigureInput figureInput)
    {
        IFigure figure;
        try
        {
            figure = figureInput.Figure();
        }
        catch (Exception ex)
        {
            return View("EditFigure", new FigureFormViewModel
            {
                FigureInput = figureInput, ErrMsg = ex.Message
            });
        }
        
        _figuresRepository.Update(figure);
        _figuresRepository.SaveData();
        
        return View("Index", new FiguresIndexViewModel
        {
            Figures = _figuresRepository.Figures,
            FiguresCounts = _figuresRepository.FiguresCounts()
        });
    }

    // GET Figures/RemoveFigure
    [HttpGet("RemoveFigure/")]
    public IActionResult RemoveFigure(int id)
    {
        _figuresRepository.Delete(id);
        _figuresRepository.SaveData();
        return View("Index", new FiguresIndexViewModel
        {
            Figures = _figuresRepository.Figures,
            FiguresCounts = _figuresRepository.FiguresCounts()
        });
    }
    
    [HttpGet("Filter/{figure=All}/{property}/{sort?}")]
    public IActionResult Filter(string? property, string figure="All", string? sort = "Asc")
    {
        var figures = _figuresRepository.Figures;
        
        if (figure != "All")
            figures = _figuresRepository.Figures!.Where(f => f.Name == figure);

        figures = (property == "Reverse")
            ? figures!.Reverse()
            : figures.OrderBySort(property!, sort); 
        
        return View("Index", new FiguresIndexViewModel
        {
            Figures = figures,
            FiguresCounts = _figuresRepository.FiguresCounts()
        });
    }
}