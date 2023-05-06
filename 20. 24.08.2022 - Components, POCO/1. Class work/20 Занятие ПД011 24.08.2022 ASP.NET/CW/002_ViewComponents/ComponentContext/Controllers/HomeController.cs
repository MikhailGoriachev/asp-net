using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComponentContext.Model;
using Microsoft.AspNetCore.Mvc;

namespace ComponentContext.Controllers;

public class HomeController : Controller
{
    private ProductRepository _productRepository;

    public HomeController(ProductRepository productRepository) {
        _productRepository = productRepository;
    } // HomeController


    public IActionResult Index() {
        ViewBag.Products = _productRepository.Products;
        return View();
    } // Index
}
