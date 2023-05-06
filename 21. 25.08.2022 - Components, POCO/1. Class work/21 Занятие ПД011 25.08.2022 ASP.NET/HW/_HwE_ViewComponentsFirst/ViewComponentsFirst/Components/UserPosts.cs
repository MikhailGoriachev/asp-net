using Microsoft.AspNetCore.Mvc;
using ViewComponentsFirst.Model;

namespace ViewComponentsFirst.Components;

// Асинхронный компонент для работы с коллекцией постов пользователя 
public class UserPosts : ViewComponent
{
    private readonly UserService _service;

    // получить связь с данными через внедрение зависимостей 
    public UserPosts(UserService service) {
        _service = service;
    } // UserPosts

    // Реализация логики компонента
    public async Task<IViewComponentResult> InvokeAsync(int id) {
        // получение данных должно быть асинхронным
        List<Post> posts = await _service.GetUserPostsAsync(id);

        // обработка по заданию: количество постов, минимальная, средняя
        // и максимальная длина текста (поле body) в символах
        ViewBag.Statistics = (
            posts.Count, 
            posts.Min(p => p.Body.Length), 
            posts.Average(p=> p.Body.Length),
            posts.Max(p => p.Title.Length));

        return View(posts);
    } // InvokeAsync
}

