namespace Homework.Models.DTO;

// Модель для передачи данных о единице измерения (Data Transfer Object)
public class UnitDTO
{
    public int Id { get; set; }

    public string Short { get; set; } 
    
    public string Long { get; set; } 

    public UnitDTO(Unit unit)
    {
        Id = unit.Id;
        Short = unit.Short;
        Long = unit.Long;
    }
}