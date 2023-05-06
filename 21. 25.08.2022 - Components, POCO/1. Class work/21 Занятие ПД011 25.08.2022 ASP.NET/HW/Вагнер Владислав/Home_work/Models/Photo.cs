using System.ComponentModel.DataAnnotations;

namespace Home_work.Models;

public class Photo
{
    //Id альбома
    public int AlbumId { get; set; }

    //Id пользователя
    public int Id { get; set; }

    //Заголовок к фото
    public string Title { get; set; }

    //Ссылка на фото
    public string Url { get; set; }
    public string ThumbnailUrl { get; set; }
}
