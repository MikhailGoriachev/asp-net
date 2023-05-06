using Microsoft.AspNetCore.Mvc;
using PocoComponentSample.Model;

namespace PocoComponentSample.Controllers;

public class HomeController : Controller
{
    private ProductRepository _productRepository;

    public HomeController(ProductRepository productRepository) {
        _productRepository = productRepository;
    } // HomeController

    public IActionResult Index() {
        return View(_productRepository);
    } // Index
} // HomeController
