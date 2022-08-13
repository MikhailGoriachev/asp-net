using Homework.Common;
using Newtonsoft.Json;

namespace Homework.Models.Ships;

public class Fleet
{
    private const string DataFileName = "ship.json";
    private readonly string _dataFolder;
    private string DataFullPath => Path.Combine(_dataFolder, DataFileName);
		
    public List<Ship> Ships { get; private set; } = new ();

    public Fleet(string dataFolder)
    {
        _dataFolder = dataFolder;
        
        if (File.Exists(DataFullPath))
            Deserialize();
        else
        {
            Ships = new List<Ship>
            {
                new("Адмирал Трибуц", "большой противолодочный корабль", 163, 19, 7408, 1983, "tributz.jpg"),
                new("Бурный ", "эскадренный миноносец", 156.5, 17.2, 7904, 1986, "burniy.jpg"),
                new("Быстрый ", "эскадренный миноносец", 156.5, 17.2, 7904, 1987, "bystryy.jpg"),
                new("Адмирал Нахимов", "ракетный крейсер", 230, 250.1, 23750, 1986, "admnahim.jpg"),
                new("Варяг ", "ракетный крейсер", 186.4, 20.8, 11490, 1983, "varyag.jpg"),
                new("Адмирал Виноградов", "фрегат", 163, 19, 7408, 1987, "vinogradov.jpg"),
                new("Маршал Шапошников", "фрегат", 163, 19, 7408, 1984, "shaposhnikov.jpg"),
                new("Громкий ", "корвет", 104.5, 13, 2250, 2018, "gromkiy.jpg"),
                new("Гремящий ", "корвет", 106.3, 13, 2430, 2017, "gremyashchy.jpg"),
                new("Кореец", "малый противолодочный корабль", 71.12, 10.15, 1220, 1987, "koreetz.jpg"),
                new("Пересвет", "большой десантный корабль", 112.5, 15, 4080, 1991, "peresvet.jpg"),
                new("Иван Карцов", "десантный катер", 45, 8.6, 280, 2013, "kravtzov.jpg"),
                new("Вице-адмирал Захарьин", "морской тральщик", 61, 10.2, 852, 2006, "zaharyin.jpeg"),
            };
            Serialize();
        }
    }
    
    public void Add(Ship prod) => Ships.Insert(0, prod);

    public IEnumerable<Ship> GetOrdered(string prop, string order) =>
        order == "Asc" ? Ships.OrderBy(prop) : Ships.OrderByDescending(prop);

    public void Serialize() =>
        File.WriteAllText(DataFullPath, JsonConvert.SerializeObject(Ships, Formatting.Indented));
		
    public void Deserialize() =>
        Ships = JsonConvert.DeserializeObject<List<Ship>>(File.ReadAllText(DataFullPath))!;
}