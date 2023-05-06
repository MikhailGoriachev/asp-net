using EntityFrameworkInAspNetCoreMvcIntro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EfAspCoreMvciltersPaginations.Models.ViewModels;

// модель для вывода в представление коллекции пользователей,
// и параметров фильтрации - список компаний, имя пользлвателя 
public class UserListViewModel
{
    // для выволда в представление коллекции пользователй
    public IEnumerable<User> Users { get; set; } = new List<User>();

    // параметр фильтрации - полное имя (Фамииля И.О.) пользователя или его часть
    public string? Name { get; set; }

    // параметр фильтрации - компания, выбираемая из выпадающего списка 
    public SelectList Companies { get; set; } = new(new List<Company>(), "Id", "Name");

}

