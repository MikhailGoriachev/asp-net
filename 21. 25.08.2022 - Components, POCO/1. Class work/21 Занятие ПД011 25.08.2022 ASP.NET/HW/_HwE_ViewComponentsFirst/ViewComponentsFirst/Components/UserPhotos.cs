using Microsoft.AspNetCore.Mvc;
using ViewComponentsFirst.Model;

namespace ViewComponentsFirst.Components;

// Асинхронный компонент для работы с коллекцией фотографий пользователя 
public class UserPhotos: ViewComponent
{
    private readonly UserService _service;

    // получить связь с данными через внедрение зависимостей 
    public UserPhotos(UserService service) {
        _service = service;
    } // UserPosts


    // Реализация логики компонента
    public async Task<IViewComponentResult> InvokeAsync(int id) {
        // получение данных должно быть асинхронным
        List<Photo> photos = await _service.GetUserPhotosAsync(id);

        // Количество фотографий в альбоме - обработка по заданию
        ViewBag.Count = photos.Count;
        // Идентификатор пользователя - для бОльшей информативности
        ViewBag.UserId = id;

        return View(photos);
    } // InvokeAsync
} // class UserPhotos

