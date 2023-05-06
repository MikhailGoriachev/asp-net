namespace Homework.Models.Figures;

public interface IFiguresRepository
{
    public IEnumerable<IFigure> Figures { get; }
    
    public void Add(IFigure? figure = null);

    public IFigure Get(int id);
    
    void Update(IFigure figure);
    
    void Delete(int id);

    public Dictionary<string, int> FiguresCounts();
    
    public void SaveData();
}