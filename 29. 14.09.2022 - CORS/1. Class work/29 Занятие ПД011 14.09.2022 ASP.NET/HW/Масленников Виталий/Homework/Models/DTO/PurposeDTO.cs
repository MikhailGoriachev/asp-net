namespace Homework.Models.DTO;

// Модель для передачи данных о цели поездки
public class PurposeDTO
{
    public int Id { get; set; }
    
    // Цель поездки
    public string? Name { get; set; }

    public PurposeDTO(Purpose purpose)
    {
        Id = purpose.Id;
        Name = purpose.Name;
    }
}