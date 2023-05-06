using System.ComponentModel.DataAnnotations;

namespace Home_work.Models;

public class Post
{
    //Id пользователя
    public int UserId { get; set; }

    //Id поста
    public int Id { get; set; }

    //Заголовок поста
    public string Title { get; set; }

    //Тело поста
    public string Body { get; set; }
}
