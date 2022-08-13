namespace H_WASP_NET.Models
{
    /// <summary>
    /// Клиенты
    ///* Фамилия клиента
    ///* Имя клиента
    ///* Отчество клиента
    ///* Серия – номер паспорта клиента
    /// </summary>
    public class Client
    {
        public int Id { get; set; }

        // Фамилия клиента
        public string? Surname { get; set; }

        // Имя клиента
        public string? Name { get; set; }

        // Отчество клиента
        public string? Patronymic { get; set; }

        // Серия – номер паспорта клиента
        public string? Passport { get; set; }


        // связное свойство для стороны "много"
        public List<Travel> Travels { get; set; } = new();

    } // class Client
}
