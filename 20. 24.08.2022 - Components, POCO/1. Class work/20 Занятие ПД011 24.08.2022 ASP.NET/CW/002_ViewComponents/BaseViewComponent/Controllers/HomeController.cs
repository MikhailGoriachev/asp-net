using BaseViewComponent.Model;
using Microsoft.AspNetCore.Mvc;

namespace BaseViewComponent.Controllers;

public class HomeController : Controller
{
    private ProductRepository _productRepository;

    public HomeController(ProductRepository productRepository) {
        _productRepository = productRepository;
    } // HomeController

    public IActionResult Index() {
        return View(_productRepository);
    } // Index
}
