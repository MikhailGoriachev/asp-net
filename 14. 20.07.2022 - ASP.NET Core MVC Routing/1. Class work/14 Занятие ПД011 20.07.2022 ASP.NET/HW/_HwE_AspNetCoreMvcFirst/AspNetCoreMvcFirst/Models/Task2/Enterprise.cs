using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace AspNetCoreMvcFirst.Models.Task2;

// класс, представляющий предприятие - фактически, контейнер для 
// коллекции работников, методов сериалиации и десериализации
public class Enterprise
{
    // идентификатор предприятия
    public int Id { get; set; }

    // нпзвание предприятия
    public string Name { get; set; } = "Рога и Копыта";

    // коллекция сотрудников
    public List<Worker> Workers { get; set; } = new();

    // конструктор
    public Enterprise() { } // Enterprise

    public void Generate() {
        Id = DateTime.Now.Millisecond + 1; // +1 для ухода от 0го значения
        Name = "Рога и Копыта";
        Workers = new List<Worker>(new[] {
            new Worker( 1, "Павловская Т.А.",    new DateTime(2012, 12, 22), "woman001.jpg", 36_000),
            new Worker( 2, "Златопольский Д.М.", new DateTime(2016,  3, 11), "man001.jpg",   32_000),
            new Worker( 3, "Чулюков В.А.",       new DateTime(2015,  6, 18), "man002.jpg",   34_000),
            new Worker( 4, "Васильев А.В.",      new DateTime(2015,  4,  6), "man003.jpg",   33_000),
            new Worker( 5, "Астраханская А.А.",  new DateTime(2014,  7, 10), "woman002.jpg", 32_000),
            new Worker( 6, "Фленова М.Е.",       new DateTime(2018, 11, 13), "woman003.jpg", 33_600),
            new Worker( 7, "Фаронов В.В.",       new DateTime(2012,  2, 28), "man004.jpg",   47_800),
            new Worker( 8, "Фролов А.В.",        new DateTime(2013,  9, 17), "man005.jpg",   38_200),
            new Worker( 9, "Евдокимова П.В.",    new DateTime(2019,  4, 12), "woman004.jpg", 42_420),
            new Worker(10, "Жаркова В.А.",       new DateTime(2019,  4, 26), "woman005.jpg", 40_800),
            new Worker(11, "Пугачев С.В.",       new DateTime(2019,  4, 17), "man006.jpg",   39_100),
            new Worker(12, "Подбельская В.В.",   new DateTime(2019,  4, 11), "woman006.jpg", 39_900),
        });
    } // Generate

} // Enterprise

