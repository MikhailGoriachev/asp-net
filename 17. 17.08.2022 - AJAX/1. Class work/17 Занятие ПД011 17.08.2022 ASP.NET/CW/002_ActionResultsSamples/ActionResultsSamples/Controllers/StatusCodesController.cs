using Microsoft.AspNetCore.Mvc;

namespace ActionResultsSamples.Controllers;

public class StatusCodesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Sample1()
    {
        return StatusCode(200); // возврат указанного статус кода
    }

    public IActionResult Sample2()
    {
        return Ok(); // 200
    }

    // код 201 - возвращается при создании сущности на сервере
    // 201, адрес по которому создан ресурс и сам ресурс
    public IActionResult Sample3() {
        // Request.Path + "/123" - путь к созданной сущности
        // new { prop1 = "A", prop2 = "B" } - данные создаваемой сущности
        return Created(Request.Path + "/123", new { prop1 = "A", prop2 = "B" }); 
    }

    public IActionResult Sample4() {
        return BadRequest(); // 400
    }

    public IActionResult Sample5() {
        return Unauthorized(); // 401
    }

    public IActionResult Sample6() {
        return NotFound(); // 404
    }
}