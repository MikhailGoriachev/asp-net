namespace Homework.Models.DTO;

// Модель для передачи данных о товаре (Data Transfer Object)
public class GoodsDTO
{
    public int Id { get; set; }
    
    public string Goods { get; set; }

    public GoodsDTO(Goods goods)
    {
        Id = goods.Id;
        Goods = goods.Name;
    }
}