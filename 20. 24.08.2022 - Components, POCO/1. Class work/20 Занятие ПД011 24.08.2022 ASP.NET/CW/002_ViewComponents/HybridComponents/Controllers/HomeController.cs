using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HybridComponents.Model;
using Microsoft.AspNetCore.Mvc;

namespace HybridComponents.Controllers;

public class HomeController : Controller
{
    // репозиторий товаров
    private ProductRepository _repository;

    // получить репозиторий - через внедрение зависимости
    public HomeController(ProductRepository repository) {
        _repository = repository;
    }

    public IActionResult Index() {
        return View(_repository.Products);
    }
}
