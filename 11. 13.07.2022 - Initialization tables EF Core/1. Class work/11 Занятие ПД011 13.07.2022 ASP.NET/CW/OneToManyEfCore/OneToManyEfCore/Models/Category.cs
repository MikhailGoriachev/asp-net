using OneToManyEfCore.Models;

namespace OneToManyFfCore.Models
{
    // Вид издания (газета, журнал, альманах, каталог, …)
    // связан отношением "многие к одному" с сущностью Publication
    public class Category
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }

        // связное свойство для стороны "много"
        public List<Publication> Publications { get; set; } = new();
    }
}
