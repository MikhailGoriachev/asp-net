using Microsoft.AspNetCore.Mvc;
using PartialViews02.Model;

namespace PartialViews02.Controllers;

public class HomeController : Controller
{
    // модель для работы с представлениями
    ContactsViewModel _viewModel;

    public HomeController() {
        _viewModel = new ContactsViewModel() {
            Email = "some@example.com",
            Phone = "+7 459 789 65 43",
            Address = "Снежное, ул. Благоева, 23, 2"
        };
    } // HomeController
    
    // передача модели в представление
    // для имитации данных, передаваемых в частичное представление
    public IActionResult Index() {
        return View(_viewModel);
    }

    public IActionResult Catalog() {
        return View();
    }

    // передача модели в представление
    // для имитации данных, передаваемых в частичное представление
    public IActionResult Contacts() {
        return View(_viewModel);
    }
} // class HomeController
