using AsyncComponents.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncComponents.Components;

// Асинхронный компонент
public class RegistredUsers : ViewComponent
{
    private readonly UserService _service;

    // получить связь с данными через внедрение зависимостей 
    public RegistredUsers(UserService service) {
        _service = service;
    } // UserService

    public async Task<IViewComponentResult> InvokeAsync() {
        // получение данных должно быть асинхронным
        List<User> users = await _service.GetUsersAsync();
        return View(users);
    } // InvokeAsync
} // class RegistredUsers

