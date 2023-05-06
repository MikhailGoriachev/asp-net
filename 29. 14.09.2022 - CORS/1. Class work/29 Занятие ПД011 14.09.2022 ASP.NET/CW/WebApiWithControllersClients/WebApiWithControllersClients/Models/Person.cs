namespace WebApiWithControllersClients.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = null!;
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}: {Fullname}; возраст, лет: {Age}";
        }
    }
}
